using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.RequestsAddon.Configuration;

/// <summary>
/// Plugin configuration.
/// </summary>
public class PluginConfiguration : BasePluginConfiguration
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PluginConfiguration"/> class.
    /// </summary>
    public PluginConfiguration()
    {
        RequestsUrl = "https://www.example.com"; // Set default URL here
    }

    /// <summary>
    /// Gets or sets the Requests URL.
    /// </summary>
    public string RequestsUrl { get; set; }
}
