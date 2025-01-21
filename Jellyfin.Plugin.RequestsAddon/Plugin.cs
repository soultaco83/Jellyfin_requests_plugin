using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Jellyfin.Plugin.RequestsAddon.Configuration;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.RequestsAddon
{
    /// <summary>
    /// The main plugin.
    /// </summary>
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        private readonly ILogger<Plugin> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Plugin"/> class.
        /// </summary>
        /// <param name="applicationPaths">Instance of the <see cref="IApplicationPaths"/> interface.</param>
        /// <param name="xmlSerializer">Instance of the <see cref="IXmlSerializer"/> interface.</param>
        /// <param name="logger">Instance of the <see cref="ILogger{Plugin}"/> interface.</param>
        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer, ILogger<Plugin> logger)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
            _logger = logger;

            if (!string.IsNullOrWhiteSpace(applicationPaths.WebPath))
            {
                InjectFiles(applicationPaths.WebPath);
            }
        }

        /// <inheritdoc />
        public override string Name => "RequestsAddon";

        /// <inheritdoc />
        public override Guid Id => Guid.Parse("b0bef419-e5b8-439d-a6a5-9b96949cbe7e");

        /// <summary>
        /// Gets the current plugin instance.
        /// </summary>
        public static Plugin? Instance { get; private set; }

        private void InjectFiles(string webPath)
        {
            try
            {
                // 1. Inject the requests.js script
                string indexFile = Path.Combine(webPath, "index.html");
                if (File.Exists(indexFile))
                {
                    string indexContents = File.ReadAllText(indexFile);
                    string scriptElement = "<script src=\"/Plugins/RequestsAddon/Web/requests.js\" defer></script>";

                    if (!indexContents.Contains(scriptElement))
                    {
                        int bodyClosing = indexContents.LastIndexOf("</body>");
                        if (bodyClosing != -1)
                        {
                            indexContents = indexContents.Insert(bodyClosing, scriptElement);
                            File.WriteAllText(indexFile, indexContents);
                            _logger.LogInformation("Successfully injected requests.js script");
                        }
                    }
                }

                // 2. Modify the home-html chunk file
                var homeFiles = Directory.GetFiles(webPath, "home-html.*.chunk.js");
                foreach (var homeFile in homeFiles)
                {
                    string homeContents = File.ReadAllText(homeFile);
                    
                    // Look for the specific chunk we want to modify
                    if (homeContents.Contains("\"use strict\";(self.webpackChunk=self.webpackChunk||[]).push([[8372]"))
                    {
                        // Only modify if requests tab doesn't exist
                        if (!homeContents.Contains("requestsTab"))
                        {
                            // Find the closing div pattern
                            string oldPattern = "</div> </div> \\'";
                            string newContent = "</div> <div class=\\\"tabContent pageTabContent\\\" id=\\\"requestsTab\\\" data-index=\\\"2\\\"> <div class=\\\"sections\\\"></div> </div> \\'";
                            
                            homeContents = homeContents.Replace(oldPattern, newContent);
                            File.WriteAllText(homeFile, homeContents);
                            _logger.LogInformation($"Successfully modified {homeFile}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error injecting files");
            }
        }

        /// <inheritdoc />
        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = this.Name,
                    EmbeddedResourcePath = string.Format(CultureInfo.InvariantCulture, "{0}.Configuration.configPage.html", GetType().Namespace)
                }
            };
        }

        /// <summary>
        /// Registers services for the plugin.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        public void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(Instance!);
        }
    }
}