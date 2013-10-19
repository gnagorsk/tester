using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Objects;

namespace Questionnaire
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TestManagement" in code, svc and config file together.
    public class TestManagement : ITestManagement
    {
        public Dictionary<int, String> getCategories()
        {
            var db = new QuestionsContainer();
            Dictionary<int, String> categories = new Dictionary<int, string>();

            foreach (Category c in db.CategorySet)
            {
                categories.Add(c.id, c.text);
            }

            return categories;
        }

        public Dictionary<int, String> getQuestions(int category) 
        {
            var db = new QuestionsContainer();
            Dictionary<int, String> questions = new Dictionary<int, string>();

            var res = (from q in db.QuestionSet where q.Category.id == category select q);

            foreach (Question q in res)
            {
                questions.Add(q.id, q.text);
            }

            return questions;
        }

        public Dictionary<int, String> getQuestionAnswers(int question)
        {
            var db = new QuestionsContainer();
            Dictionary<int, String> answers = new Dictionary<int, string>();

            var res = (from q in db.QuestionSet where q.id == question select q).First();

            foreach (Answer a in res.Answers)
            {
                answers.Add(a.id, a.text);
            }

            return answers;
        }

        public bool validateAnswer(int question, int answer)
        {
            var db = new QuestionsContainer();
            var res = (from q in db.QuestionSet where q.id == question select q).First();
            return res.CorrectAnswer.id == answer;
        }
        
        public void DoWork()
        {
            var db = new QuestionsContainer();
            Category c = new Category();
            c.text = "test category";

            Answer a = new Answer();
            a.text = "lol";

            Question q = new Question();
            q.text = "test";
            q.title = "lol";
            q.Category = c;
            c.Question.Add(q);
            q.Answers.Add(a);
            q.CorrectAnswer = a;


            db.CategorySet.AddObject(c);
            db.AnswerSet.AddObject(a);
            db.QuestionSet.AddObject(q);

            db.SaveChanges();
            
        }
    }
}
