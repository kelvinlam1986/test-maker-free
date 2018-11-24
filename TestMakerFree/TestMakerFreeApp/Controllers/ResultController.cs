using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using TestMakerFreeApp.Data;
using TestMakerFreeApp.Data.Models;
using TestMakerFreeApp.ViewModels;

namespace TestMakerFreeApp.Controllers
{
    [Route("api/[controller]")]
    public class ResultController : BaseApiController
    {
        public ResultController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = DbContext.Results.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound(new
                {
                    Error = $"Result ID {id} has not been found."
                });
            }

            return new JsonResult(result.Adapt<ResultViewModel>(),
              new JsonSerializerSettings()
              {
                  Formatting = Formatting.Indented
              });
        }

        [HttpPost]
        public IActionResult Post([FromBody] ResultViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);
            var result = model.Adapt<Result>();
            result.CreateDate = DateTime.Now;
            result.LastModifiedDate = result.CreateDate;

            DbContext.Results.Add(result);
            DbContext.SaveChanges();

            return new JsonResult(result.Adapt<ResultViewModel>(),
                    new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented
                    });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ResultViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);
            var result = DbContext.Results.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Result ID {0} has not been found", id)
                });
            }

            result.QuizId = model.QuizId;
            result.Text = model.Text;
            result.MinValue = model.MinValue;
            result.MaxValue = model.MaxValue;
            result.Notes = model.Notes;
            result.LastModifiedDate = result.CreateDate;
            DbContext.SaveChanges();

            return new JsonResult(result.Adapt<ResultViewModel>(),
              new JsonSerializerSettings()
              {
                  Formatting = Formatting.Indented
              });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = DbContext.Results.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Result ID {0} has not been found", id)
                });
            }

            DbContext.Results.Remove(result);
            DbContext.SaveChanges();

            return NoContent();
        }

        [Route("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var results = DbContext.Results.Where(x => x.QuizId == quizId).ToArray();
            return new JsonResult(results.Adapt<ResultViewModel[]>(),
              new JsonSerializerSettings()
              {
                  Formatting = Formatting.Indented
              });
        }
    }
}