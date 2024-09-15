using System;
using System.Net.Mime;
using Jellyfin.Plugin.RequestsAddon.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.RequestsAddon.Api
{
    /// <summary>
    /// Controller for the RequestsAddon API.
    /// </summary>
    [ApiController]
    [Route("Plugins/RequestsAddon")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly Plugin _plugin;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiController"/> class.
        /// </summary>
        /// <param name="logger">Instance of the <see cref="ILogger{ApiController}"/> interface.</param>
        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
            _plugin = Plugin.Instance!;
        }

        /// <summary>
        /// Get the RequestsUrl.
        /// </summary>
        /// <response code="200">RequestsUrl retrieved.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>The RequestsUrl.</returns>
        [HttpGet("PublicRequestsUrl")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public ActionResult<string> GetPublicRequestsUrl()
        {
            try
            {
                var requestsUrl = _plugin.Configuration.RequestsUrl;
                if (string.IsNullOrEmpty(requestsUrl))
                {
                    _logger.LogWarning("RequestsUrl is null or empty");
                    return Ok(string.Empty);
                }

                return Ok(requestsUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving RequestsUrl");
                return StatusCode(500, "An error occurred while retrieving the RequestsUrl");
            }
        }
    }
}
