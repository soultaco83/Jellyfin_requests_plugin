using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jellyfin.Plugin.RequestsAddon.Api
{
    /// <summary>
    /// Controller for serving the requests.js file.
    /// </summary>
    [ApiController]
    [Route("Plugins/RequestsAddon/Web")]  // Note: Different route to avoid conflicts
    public class RequestsController : ControllerBase
    {
        /// <summary>
        /// Gets the requests.js script file.
        /// </summary>
        /// <response code="200">Script file retrieved.</response>
        /// <response code="404">Script file not found.</response>
        /// <returns>The script file.</returns>
        [HttpGet("requests.js")]
        [Produces("application/javascript")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetRequestsScript()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var scriptPath = $"{typeof(Plugin).Namespace}.requests.js";

            Stream? scriptStream = assembly.GetManifestResourceStream(scriptPath);
            if (scriptStream != null)
            {
                return File(scriptStream, "application/javascript");
            }
            
            return NotFound();
        }
    }
}