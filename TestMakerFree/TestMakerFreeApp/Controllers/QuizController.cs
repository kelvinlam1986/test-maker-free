using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TestMakerFreeApp.ViewModels;

namespace TestMakerFreeApp.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var v = new QuizViewModel
            {
                Id = id,
                Title = String.Format("Sample quiz with id {0}", id),
                Description = "Not a real quiz: it's just a sample!",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };

            return new JsonResult(v,
              new JsonSerializerSettings()
              {
                  Formatting = Formatting.Indented
              });
        }

        [Route("Latest/{num}")]
        public IActionResult Latest(int num = 10)
        {
            var sampleQuizzes = new List<QuizViewModel>();
            sampleQuizzes.Add(new QuizViewModel
            {
                Id = 1,
                Title = "Which Shingeki No Kyojin character are you?",
                Description = "Anime-related personality test",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            for (int i = 2; i <= num; i++)
            {
                sampleQuizzes.Add(new QuizViewModel
                {
                    Id = i,
                    Title = String.Format("Sample Quiz {0}", i),
                    Description = "This is a sample quiz",
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            return new JsonResult(sampleQuizzes,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        [Route("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10)
        {
            var sampleQuizzes = ((JsonResult)Latest(num)).Value as List<QuizViewModel>;
            return new JsonResult(sampleQuizzes.OrderBy(t => t.Title),
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
        }

        [Route("Random/{num:int?}")]
        public IActionResult ByRandom(int num = 10)
        {
            var sampleQuizzes = ((JsonResult)Latest(num)).Value as List<QuizViewModel>;
            return new JsonResult(sampleQuizzes.OrderBy(t => Guid.NewGuid()),
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
        }

    }
}