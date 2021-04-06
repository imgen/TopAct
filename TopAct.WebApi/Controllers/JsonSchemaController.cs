using Microsoft.AspNetCore.Mvc;
using NJsonSchema;
using TopAct.Domain.Entities;

namespace TopAct.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class JsonSchemaController : Controller
    {
        [HttpGet]
        public IActionResult GetJsonSchema()
        {
            var schema = JsonSchema.FromType<Contact>();
            var schemaJson = schema.ToJson();
            return Content(schemaJson, "application/json");
        }
    }
}
