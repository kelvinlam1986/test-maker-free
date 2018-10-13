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
    public class QuizController : Controller
    {
        private ApplicationDbContext dbContext;

        public QuizController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var quiz = dbContext.Quizzes.Where(x => x.Id == id).FirstOrDefault();

            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Quiz ID {0} has not been found", id)
                });
            }

            return new JsonResult(
                quiz.Adapt<QuizViewModel>(),
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
        }

        [HttpPost]
        public IActionResult Post([FromBody]QuizViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);
            var quiz = new Quiz();
            quiz.Title = model.Title;
            quiz.Description = model.Description;
            quiz.Text = model.Text;
            quiz.Notes = model.Notes;

            quiz.CreatedDate = DateTime.Now;
            quiz.LastModifiedDate = quiz.CreatedDate;
            quiz.UserId = dbContext.Users.Where(x => x.UserName == "Admin").FirstOrDefault().Id;
            dbContext.Quizzes.Add(quiz);
            dbContext.SaveChanges();

            return new JsonResult(quiz.Adapt<QuizViewModel>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]QuizViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);
            var quiz = dbContext.Quizzes.FirstOrDefault(x => x.Id == id);
            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Quiz ID {0} has not been found", id)
                });
            }

            quiz.Title = model.Title;
            quiz.Description = model.Description;
            quiz.Text = model.Text;
            quiz.Notes = model.Notes;
            quiz.LastModifiedDate = quiz.LastModifiedDate;
            quiz.UserId = dbContext.Users.Where(x => x.UserName == "Admin").FirstOrDefault().Id;
            dbContext.SaveChanges();

            return new JsonResult(quiz.Adapt<QuizViewModel>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var quiz = dbContext.Quizzes.FirstOrDefault(x => x.Id == id);
            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Quiz ID {0} has not been found", id)
                });
            }

            dbContext.Quizzes.Remove(quiz);
            dbContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("Latest/{num}")]
        public IActionResult Latest(int num = 10)
        {
            var latest = dbContext.Quizzes
                            .OrderByDescending(x => x.CreatedDate)
                            .Take(num)
                            .ToArray();
            return new JsonResult(
                latest.Adapt<QuizViewModel[]>(),
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
        }

        [Route("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10)
        {
            var byTitle = dbContext.Quizzes
                            .OrderBy(x => x.Title)
                            .Take(num)
                            .ToArray();
            return new JsonResult(
                byTitle.Adapt<QuizViewModel[]>(), 
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                }
            );
        }

        [Route("Random/{num:int?}")]
        public IActionResult ByRandom(int num = 10)
        {
            var random = dbContext.Quizzes
                            .OrderBy(x => Guid.NewGuid())
                            .Take(num)
                            .ToArray();
            return new JsonResult(
                random.Adapt<QuizViewModel[]>(),
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                }
            );
        }

    }
}