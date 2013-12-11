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
    public class TestService : ITestService, ITestManagement
    {
        private RemoteProcessingError error = new RemoteProcessingError();

        public Dictionary<int, String> getCategories()
        {
            try
            {
                var db = new QuestionsContainer();
                Dictionary<int, String> categories = new Dictionary<int, string>();

                foreach (Category c in db.CategorySet)
                {
                    if (c.Category_id == null)
                    {
                        categories.Add(c.id, c.text);
                    }
                }

                return categories;
            }
            catch (Exception e)
            {
                error.Title = System.Reflection.MethodBase.GetCurrentMethod().Name + " exception";
                error.ExceptionMessage = e.Message;
                throw new FaultException<RemoteProcessingError>(error);
            }
        }

        public Dictionary<int, String> getSubcategories(int category)
        {
            try
            {
                var db = new QuestionsContainer();
                Dictionary<int, String> categories = new Dictionary<int, string>();

                var res = (from c in db.CategorySet where c.id == category select c).First();

                foreach (Category c in res.Subcategories)
                {
                    categories.Add(c.id, c.text);
                }

                return categories;
            }
            catch (Exception e)
            {
                error.Title = System.Reflection.MethodBase.GetCurrentMethod().Name + " exception";
                error.ExceptionMessage = e.Message;
                throw new FaultException<RemoteProcessingError>(error);
            }
        }

        public Dictionary<int, String> getQuestions(int category) 
        {
            try
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
            catch (Exception e)
            {
                error.Title = System.Reflection.MethodBase.GetCurrentMethod().Name + " exception";
                error.ExceptionMessage = e.Message;
                throw new FaultException<RemoteProcessingError>(error);
            }
        }

        public Dictionary<int, String> getQuestionAnswers(int question)
        {
            try
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
            catch (Exception e)
            {
                error.Title = System.Reflection.MethodBase.GetCurrentMethod().Name + " exception";
                error.ExceptionMessage = e.Message;
                throw new FaultException<RemoteProcessingError>(error);
            }
        }

        public bool validateAnswer(int question, int answer)
        {
            try
            {
                var db = new QuestionsContainer();
                var res = (from q in db.QuestionSet where q.id == question select q).First();
                return res.CorrectAnswer.id == answer;
            }
            catch (Exception e)
            {
                error.Title = System.Reflection.MethodBase.GetCurrentMethod().Name + " exception";
                error.ExceptionMessage = e.Message;
                throw new FaultException<RemoteProcessingError>(error);
            }
        }
        
        public void DoWork()
        {
            try
            {
                var db = new QuestionsContainer();

                Category c = new Category();
                c.text = "test category";

                Answer a = new Answer();
                a.text = "lol";

                Question q = new Question();
                q.text = "test";
                q.Category = c;
                c.Question.Add(q);
                q.Answers.Add(a);
                q.CorrectAnswer = a;

                db.CategorySet.AddObject(c);

                db.AnswerSet.AddObject(a);
                db.QuestionSet.AddObject(q);

                db.SaveChanges();
            }
            catch (Exception e)
            {
                error.Title = System.Reflection.MethodBase.GetCurrentMethod().Name + " exception";
                error.ExceptionMessage = e.Message;
                throw new FaultException<RemoteProcessingError>(error);
            }
        }

        public void DoMoreWork()
        {
            try
            {
                var db = new QuestionsContainer();

                Category cat = new Category();
                cat.text = "test sub category";
                var res = (from c in db.CategorySet where c.id == 1 select c).First();
                res.Subcategories.Add(cat);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                error.Title = System.Reflection.MethodBase.GetCurrentMethod().Name + " exception";
                error.ExceptionMessage = e.Message;
                throw new FaultException<RemoteProcessingError>(error);
            }
        }

        public int createCategory(String title, int category)
        {
            try
            {
                var db = new QuestionsContainer();
                Category new_category = new Category();
                new_category.text = title;

                var res = (from c in db.CategorySet where c.id == category select c);
                if (res.Count() != 0)
                {
                    res.First().Subcategories.Add(new_category);
                }

                db.CategorySet.AddObject(new_category);
                db.SaveChanges();

                return new_category.id;
            }
            catch (Exception e)
            {
                error.Title = System.Reflection.MethodBase.GetCurrentMethod().Name + " exception";
                error.ExceptionMessage = e.Message;
                throw new FaultException<RemoteProcessingError>(error);
            }
        }

        public int createQuestion(String text, int category, int correct_answer)
        {
            try
            {
                var db = new QuestionsContainer();
                var a = (from x in db.AnswerSet where x.id == correct_answer select x);
                var res = (from c in db.CategorySet where c.id == category select c);
                if (res.Count() == 0 || a.Count() == 0) return -1;
                Question new_question = new Question();
                new_question.text = text;
                new_question.Answers.Add(a.First());
                new_question.CorrectAnswer = a.First();
                res.First().Question.Add(new_question);
                db.SaveChanges();

                return new_question.id;
            }
            catch (Exception e)
            {
                error.Title = System.Reflection.MethodBase.GetCurrentMethod().Name + " exception";
                error.ExceptionMessage = e.Message;
                throw new FaultException<RemoteProcessingError>(error);
            }
        }

        public int createAnswer(String text)
        {
            try
            {
                var db = new QuestionsContainer();
                Answer answer = new Answer();
                answer.text = text;
                db.AnswerSet.AddObject(answer);
                db.SaveChanges();
                return answer.id;
            }
            catch (Exception e)
            {
                error.Title = System.Reflection.MethodBase.GetCurrentMethod().Name + " exception";
                error.ExceptionMessage = e.Message;
                throw new FaultException<RemoteProcessingError>(error);
            }
        }

        public bool addAnswerToQuestion(int answer, int question)
        {
            try
            {
                var db = new QuestionsContainer();
                var a = (from x in db.AnswerSet where x.id == answer select x);
                var q = (from x in db.QuestionSet where x.id == question select x);
                if (q.Count() == 0 || a.Count() == 0) return false;

                q.First().Answers.Add(a.First());
                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                error.Title = System.Reflection.MethodBase.GetCurrentMethod().Name + " exception";
                error.ExceptionMessage = e.Message;
                throw new FaultException<RemoteProcessingError>(error);
            }
        }


        public bool removeAnswerFromQuestion(int answer, int question)
        {
            try
            {
                var db = new QuestionsContainer();
                var a = (from x in db.AnswerSet where x.id == answer select x);
                var q = (from x in db.QuestionSet where x.id == question select x);
                if (q.Count() == 0 || a.Count() == 0) return false;

                q.First().Answers.Remove(a.First());

                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                error.Title = System.Reflection.MethodBase.GetCurrentMethod().Name + " exception";
                error.ExceptionMessage = e.Message;
                throw new FaultException<RemoteProcessingError>(error);
            }
        }

        public bool deleteCategory(int category)
        {
            try
            {
                var db = new QuestionsContainer();
                var res = (from c in db.CategorySet where c.id == category select c);
                if (res.Count() == 0) return false;

                if (res.First().Question.Count != 0) return false;
                if (res.First().Subcategories.Count != 0) return false;

                db.CategorySet.DeleteObject(res.First());

                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                error.Title = System.Reflection.MethodBase.GetCurrentMethod().Name + " exception";
                error.ExceptionMessage = e.Message;
                throw new FaultException<RemoteProcessingError>(error);
            }
        }

        public bool deleteQuestion(int question)
        {
            try
            {
                var db = new QuestionsContainer();
                var res = (from q in db.QuestionSet where q.id == question select q);
                if (res.Count() == 0) return false;

                if (res.First().Answers.Count != 0) return false;

                db.QuestionSet.DeleteObject(res.First());

                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                error.Title = System.Reflection.MethodBase.GetCurrentMethod().Name + " exception";
                error.ExceptionMessage = e.Message;
                throw new FaultException<RemoteProcessingError>(error);
            }
        }
    }
}
