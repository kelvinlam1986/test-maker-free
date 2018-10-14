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
    public class AnswerController : BaseApiController
    {
        public AnswerController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var answer = DbContext.Answers.FirstOrDefault(x => x.Id == id);
            if (answer == null)
            {
                return NotFound(new
                {
                    Error = $"Answer ID {id} has not been found."
                });
            }

            return new JsonResult(answer.Adapt<AnswerViewModel>(),
              new JsonSerializerSettings()
              {
                  Formatting = Formatting.Indented
              });
        }

        [HttpPost]
        public IActionResult Post(AnswerViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);
            var answer = model.Adapt<Answer>();
            answer.QuestionId = model.QuestionId;
            answer.Text = model.Text;
            answer.Notes = model.Notes;
            answer.CreatedDate = DateTime.Now;
            answer.LastModifiedDate = answer.CreatedDate;

            DbContext.Answers.Add(answer);
            DbContext.SaveChanges();

            return new JsonResult(answer.Adapt<AnswerViewModel>(),
                    new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented
                    });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]AnswerViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);
            var answer = DbContext.Answers.FirstOrDefault(x => x.Id == id);
            if (answer == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Answer ID {0} has not been found", id)
                });
            }

            answer.QuestionId = model.QuestionId;
            answer.Text = model.Text;
            answer.Value = model.Value;
            answer.Notes = model.Notes;
            answer.LastModifiedDate = answer.CreatedDate;
            DbContext.SaveChanges();

            return new JsonResult(answer.Adapt<AnswerViewModel>(),
              new JsonSerializerSettings()
              {
                  Formatting = Formatting.Indented
              });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var answer = DbContext.Answers.FirstOrDefault(x => x.Id == id);
            if (answer == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Answers ID {0} has not been found", id)
                });
            }

            DbContext.Answers.Remove(answer);
            DbContext.SaveChanges();

            return NoContent();
        }

        [Route("All/{questionId}")]
        public IActionResult All(int questionId)
        {
            var answers = DbContext.Answers.Where(x => x.QuestionId == questionId).ToArray();
            return new JsonResult(answers.Adapt<AnswerViewModel[]>(),
              new JsonSerializerSettings()
              {
                  Formatting = Formatting.Indented
              });
        }
    }
}