using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TestMakerFreeApp.ViewModels;

namespace TestMakerFreeApp.Controllers
{
    [Route("api/[controller]")]
    public class AnswerController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Post(AnswerViewModel question)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]AnswerViewModel question)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        [Route("All/{questionId}")]
        public IActionResult All(int questionId)
        {
            var sampleAnswers = new List<AnswerViewModel>();
            sampleAnswers.Add(new AnswerViewModel()
            {
                Id = 1,
                QuestionId = questionId,
                Text = "Friends and family",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            for (int i = 2; i <= 5; i++)
            {
                sampleAnswers.Add(new AnswerViewModel
                {
                    Id = i,
                    QuestionId = questionId,
                    Text = String.Format("Sample Answer {0}", i),
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            return new JsonResult(sampleAnswers,
               new JsonSerializerSettings
               {
                   Formatting = Formatting.Indented
               });
        }
    }
}