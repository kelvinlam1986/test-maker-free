using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMakerFreeApp.Data;

namespace TestMakerFreeApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
        public BaseApiController(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            JsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        protected ApplicationDbContext DbContext { get; private set; }
        protected JsonSerializerSettings JsonSettings { get; private set; }
    }
}