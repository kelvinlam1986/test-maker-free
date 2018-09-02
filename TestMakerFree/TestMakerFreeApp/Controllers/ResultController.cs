using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TestMakerFreeApp.ViewModels;

namespace TestMakerFreeApp.Controllers
{
    [Route("api/[controller]")]
    public class ResultController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Post(ResultViewModel question)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ResultViewModel question)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IActionResult All(int quizId)
        {
            var sampleResults = new List<ResultViewModel>();
            sampleResults.Add(new ResultViewModel
            {
                Id = 1,
                QuizId = quizId,
                Text = "What do you value most in your life?",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            for (int i = 2; i <= 5; i++)
            {
                sampleResults.Add(new ResultViewModel
                {
                    Id = i,
                    QuizId = quizId,
                    Text = String.Format("Sample Question {0}", i),
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            return new JsonResult(sampleResults,
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
        }
    }
}