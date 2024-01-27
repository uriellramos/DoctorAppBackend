using API.Errores;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("errores/{codigo}")]
    [ApiExplorerSettings(IgnoreApi=true)]
    public class ErrorController:BaseAPIController
    {
        public IActionResult Error(int codigo)
        {
            return new ObjectResult(new ApiErrorResponse(codigo));
        }

    }
}
