using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TestMakerFreeApp.Data;
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
            return new JsonResult(
                quiz.Adapt<QuizViewModel>(),
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
        }

        [HttpPost]
        public IActionResult Post(QuizViewModel quiz)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]QuizViewModel quiz)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
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