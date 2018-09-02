using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TestMakerFreeApp.ViewModels;

namespace TestMakerFreeApp.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        [Route("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var sampleQuestions = new List<QuestionViewModel>();
            sampleQuestions.Add(new QuestionViewModel
            {
                Id = 1,
                QuizId = quizId,
                Text = "What do you value most in your life?",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            for (int i = 2; i <= 5; i++)
            {
                sampleQuestions.Add(new QuestionViewModel
                {
                    Id = i,
                    QuizId = quizId,
                    Text = String.Format("Sample Question {0}", i),
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            return new JsonResult(sampleQuestions,
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
        
        }

    }
}