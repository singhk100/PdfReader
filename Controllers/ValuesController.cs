using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PdfReader.Interface;

namespace PdfReader.Controllers 
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IService _service;
        public ValuesController(IService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("ExcelReader")]
        public async Task<ActionResult> ConvertToJson()
        {
            try
            {
                var result = _service.Convert();
                return Ok(result);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
