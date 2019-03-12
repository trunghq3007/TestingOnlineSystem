//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using TestingSystem.Models;

//namespace TestingSystem.Data
//{
//    public class SeedData : DropCreateDatabaseIfModelChanges<TestingSystemEntities>
//    {
//        protected override void Seed(TestingSystemEntities context)
//        {
//            //GetQuestionCategories().ForEach(c => context.QuestionCategories.Add(c));
//            //context.Commit();
//            //GetQuestions().ForEach(c => context.Questions.Add(c));
//            //context.Commit();
//            //GetAnswers().ForEach(c => context.Answers.Add(c));
//            //context.Commit();
//            //GetExamPapers().ForEach(c => context.ExamPapers.Add(c));
//            //context.Commit();
//            //GetExamPaperQuesions().ForEach(c => context.ExamPaperQuesions.Add(c));
//            //context.Commit();
//        }
//        //private static List<QuestionCategory> GetQuestionCategories()
//        //{
//        //    var list = new List<QuestionCategory>
//        //    {
//        //        //new QuestionCategory{Name = "C#",IsActive = true,CreatedBy = "NHDai",CreatedDate = new DateTime(2018,05,20),ModifiedBy = "NHViet",ModifiebDate = DateTime.Now},
//        //        //new QuestionCategory{Name = "Java",IsActive = true,CreatedBy = "NHDai",CreatedDate = new DateTime(2018,05,20),ModifiedBy = "NHViet",ModifiebDate = DateTime.Now},
//        //        //new QuestionCategory{Name = "Python",IsActive = true,CreatedBy = "NHDai",CreatedDate = new DateTime(2018,05,20),ModifiedBy = "NHViet",ModifiebDate = DateTime.Now},
//        //        //new QuestionCategory{Name = "HTML",IsActive = true,CreatedBy = "NHDai",CreatedDate = new DateTime(2018,05,20),ModifiedBy = "NHViet",ModifiebDate = DateTime.Now},
//        //        //new QuestionCategory{Name = "Angular",IsActive = false,CreatedBy = "NHDai",CreatedDate = new DateTime(2018,05,20),ModifiedBy = "NHViet",ModifiebDate = DateTime.Now},
//        //    };
//        //    return list;
//        //}
//        ////NHDai
//        //private static List<Question> GetQuestions()
//        //{

//        //    var list = new List<Question>
//        //    {
//        //    //    new Question{Content ="What is this ?",Image = null,Level = 1,CategoryID = 1,IsActive = true,CreatedBy = "NHDai",CreatedDate = new DateTime(2018,05,20),ModifiedBy = null,ModifiebDate = DateTime.Now},
//        //    //    new Question{Content ="What is this a?",Image = null,Level = 2,CategoryID = 1,IsActive = true,CreatedBy = "NHDai",CreatedDate = new DateTime(2018,05,20),ModifiedBy = null,ModifiebDate = DateTime.Now},
//        //    //    new Question{Content ="What is this b?",Image = null,Level = 3,CategoryID = 1,IsActive = true,CreatedBy = "NHDai",CreatedDate = new DateTime(2018,05,20),ModifiedBy = null,ModifiebDate = DateTime.Now},
//        //    //    new Question{Content ="What is this c?",Image = null,Level = 1,CategoryID = 1,IsActive = false,CreatedBy = "NHDai",CreatedDate = new DateTime(2018,05,20),ModifiedBy = null,ModifiebDate = DateTime.Now},
//        //    //    new Question{Content ="What is this d?",Image = null,Level = 2,CategoryID = 1,IsActive = true,CreatedBy = "NHDai",CreatedDate = new DateTime(2018,05,20),ModifiedBy = null,ModifiebDate = DateTime.Now},
//        //    };
//        //    return list;
//        //}
//        //private static List<Answer> GetAnswers()
//        //{

//        //    var list = new List<Answer>
//        //    {
//        //        new Answer{Content ="A",IsCorrect = true,QuestionID = 1},
//        //        new Answer{Content ="A",IsCorrect = false,QuestionID = 1},
//        //        new Answer{Content ="A",IsCorrect = false,QuestionID = 1},
//        //        new Answer{Content ="A",IsCorrect = false,QuestionID = 1},
//        //        new Answer{Content ="A",IsCorrect = true,QuestionID = 2},
//        //        new Answer{Content ="A",IsCorrect = false,QuestionID = 2},
//        //        new Answer{Content ="A",IsCorrect = false,QuestionID = 2},
//        //        new Answer{Content ="A",IsCorrect = false,QuestionID = 2},
//        //        new Answer{Content ="A",IsCorrect = true,QuestionID = 3},
//        //        new Answer{Content ="A",IsCorrect = false,QuestionID = 3},
//        //        new Answer{Content ="A",IsCorrect = false,QuestionID = 3},
//        //        new Answer{Content ="A",IsCorrect = false,QuestionID = 3},
//        //        new Answer{Content ="A",IsCorrect = true,QuestionID = 4},
//        //        new Answer{Content ="A",IsCorrect = false,QuestionID = 4},
//        //        new Answer{Content ="A",IsCorrect = false,QuestionID = 4},
//        //        new Answer{Content ="A",IsCorrect = false,QuestionID = 4},

//        //    };
//        //    return list;
//        //}
//        //private static List<ExamPaper> GetExamPapers()
//        //{

//        //    var list = new List<ExamPaper>
//        //    {
//        //    //    new ExamPaper{Title = "Exam C#",Time = 90,Status = true,NumberOfQuestion = 40,IsActive = true,CreatedBy = "NHDai",CreatedDate = new DateTime(2018,05,20),ModifiedBy = null,ModifiebDate = DateTime.Now},
//        //    //    new ExamPaper{Title = "Exam Java",Time = 90,Status = true,NumberOfQuestion = 40,IsActive = true,CreatedBy = "NHDai",CreatedDate = new DateTime(2018,05,20),ModifiedBy = null,ModifiebDate = DateTime.Now},
//        //    //    new ExamPaper{Title = "Exam Python",Time = 90,Status = true,NumberOfQuestion = 40,IsActive = true,CreatedBy = "NHDai",CreatedDate = new DateTime(2018,05,20),ModifiedBy = null,ModifiebDate = DateTime.Now},
//        //    //    new ExamPaper{Title = "Exam Angular",Time = 90,Status = true,NumberOfQuestion = 40,IsActive = true,CreatedBy = "NHDai",CreatedDate = new DateTime(2018,05,20),ModifiedBy = null,ModifiebDate = DateTime.Now},
//        //    //    new ExamPaper{Title = "Exam HTML",Time = 90,Status = true,NumberOfQuestion = 40,IsActive = true,CreatedBy = "NHDai",CreatedDate = new DateTime(2018,05,20),ModifiedBy = null,ModifiebDate = DateTime.Now},
//        //    };
//        //    return list;
//        //}
//        //private static List<ExamPaperQuesion> GetExamPaperQuesions()
//        //{

//        //    var list = new List<ExamPaperQuesion>
//        //    {
//        //        new ExamPaperQuesion{QuestionID = 1,ExamPaperID = 1},
//        //        new ExamPaperQuesion{QuestionID = 1,ExamPaperID = 2},
//        //        new ExamPaperQuesion{QuestionID = 1,ExamPaperID = 3},
//        //        new ExamPaperQuesion{QuestionID = 2,ExamPaperID = 4},
//        //        new ExamPaperQuesion{QuestionID = 3,ExamPaperID = 1},
//        //        new ExamPaperQuesion{QuestionID = 4,ExamPaperID =2},
//        //    };
//        //    return list;
//        //}
//    }
//}

