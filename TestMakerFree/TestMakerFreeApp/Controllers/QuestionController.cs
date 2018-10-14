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
    public class QuestionController : BaseApiController
    {
        public QuestionController(ApplicationDbContext context) : base(context)
        {
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var question = DbContext.Questions.FirstOrDefault(x => x.Id == id);
            if (question == null)
            {
                return NotFound(new
                {
                    Error = $"Question ID {id} has not been found."
                });
            }

            return new JsonResult(question.Adapt<QuestionViewModel>(),
              new JsonSerializerSettings()
              {
                  Formatting = Formatting.Indented
              });
        }

        [HttpPost]
        public IActionResult Post([FromBody] QuestionViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);
            var question = model.Adapt<Question>();
            question.QuizId = model.QuizId;
            question.Text = model.Text;
            question.Notes = model.Notes;
            question.CreatedDate = DateTime.Now;
            question.LastModifiedDate = question.CreatedDate;

            DbContext.Questions.Add(question);
            DbContext.SaveChanges();

            return new JsonResult(question.Adapt<QuestionViewModel>(),
                    new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented
                    });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]QuestionViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);
            var question = DbContext.Questions.FirstOrDefault(x => x.Id == id);
            if (question == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Question ID {0} has not been found", id)
                });
            }

            question.QuizId = model.QuizId;
            question.Text = model.Text;
            question.Notes = model.Notes;
            question.LastModifiedDate = question.CreatedDate;
            DbContext.SaveChanges();

            return new JsonResult(question.Adapt<QuestionViewModel>(),
              new JsonSerializerSettings()
              {
                  Formatting = Formatting.Indented
              });
    }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var question = DbContext.Questions.FirstOrDefault(x => x.Id == id);
            if (question == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Question ID {0} has not been found", id)
                });
            }

            DbContext.Questions.Remove(question);
            DbContext.SaveChanges();

            return NoContent();
        }

        [Route("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var questions = DbContext.Questions.Where(x => x.QuizId == quizId).ToArray();
            return new JsonResult(questions.Adapt<QuestionViewModel[]>(),
              new JsonSerializerSettings()
              {
                  Formatting = Formatting.Indented
              });

        }

    }
}