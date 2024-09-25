using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Api.Presentation
{
    public class ApiResponse<TResult>  : ControllerBase
    {
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static implicit operator OkObjectResult(ApiResponse<TResult> response) {
            var a = new OkObjectResult(response)
            {
                 Value = response.HttpContext.Items[0]
            };
            return a;
        }
    }
}
