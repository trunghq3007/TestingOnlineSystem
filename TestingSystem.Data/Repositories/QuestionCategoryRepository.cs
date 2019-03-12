using System;
using System.Collections.Generic;
using System.Linq;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Models;

namespace TestingSystem.Data.Repositories
{
    public class QuestionCategoryRepository : RepositoryBase<QuestionCategory>, IQuestionCategoryRepository
    {

        public QuestionCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public int AddCategoryQuestion(QuestionCategory questionCategory)
       {

          
            questionCategory.ModifiedDate = DateTime.Now;
            questionCategory.CreatedDate = DateTime.Now;
            DbContext.QuestionCategories.Add(questionCategory);
            DbContext.SaveChanges();
            return questionCategory.CategoryID;
           
        }

        public int Delete(int id)
        {
            try
            {
                QuestionCategory objExamPaper = DbContext.QuestionCategories.Find(id);
                if (objExamPaper != null)
                {
                    DbContext.QuestionCategories.Remove(objExamPaper);
                    return DbContext.SaveChanges();
                }
                return 0;
            }

            catch (Exception e)
            {
                Console.Write(e);
                throw;
            }
        }

        public int DeleteQuestionCategory(int[] dsxoa)
        {
            foreach (int i in dsxoa)
            {
                QuestionCategory category = DbContext.QuestionCategories.Find(i);
                DbContext.QuestionCategories.Remove(category);
            }
            return DbContext.SaveChanges();
        }

        public QuestionCategory FindCategoryByID(int? id)
        {
            var questionCategory = this.DbContext.QuestionCategories.SingleOrDefault(x => x.CategoryID == id);
            return questionCategory;
        }

        public IEnumerable<QuestionCategory> GetAllQuestionCategories()
        {
            var listCategory = this.DbContext.QuestionCategories.ToList();
            return listCategory;
        }

        public IEnumerable<QuestionCategory> GetAllQuestionCategoriesActive()
        {
            DbContext.Configuration.ProxyCreationEnabled = false;
            var listCategory = DbContext.QuestionCategories.Where(x => x.IsActive == true).ToList();
            return listCategory;
        }

        public IEnumerable<User> GetAllUser()
        {
            return DbContext.Users.ToList();
        }

        public bool QuestionCategoryID(int id)
        {
            var quesiton1 = DbContext.Questions.Where(x => x.CategoryID == id).Count();
            if (quesiton1 > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<QuestionCategory> SearchCategories(string txtSearch)
        {
            var listSearchCategory = DbContext.QuestionCategories.Where(x => x.Name.Contains(txtSearch)).ToList();
            return listSearchCategory;
        }
        public int UpdateCategoryQuestion(QuestionCategory questionCategory)
        {
            QuestionCategory listQuestionCategory = DbContext.QuestionCategories.Find(questionCategory.CategoryID);
            listQuestionCategory.Name = questionCategory.Name;
            listQuestionCategory.IsActive = questionCategory.IsActive;
            listQuestionCategory.CreatedBy = questionCategory.CreatedBy;
            listQuestionCategory.CreatedDate = DateTime.Now;
            listQuestionCategory.ModifiedBy = questionCategory.ModifiedBy;
            listQuestionCategory.ModifiedDate = DateTime.Now;

            return DbContext.SaveChanges();
        }

    }

    public interface IQuestionCategoryRepository : IRepository<QuestionCategory>
    {
        IEnumerable<User> GetAllUser();
        int AddCategoryQuestion(QuestionCategory questionCategory);
        int UpdateCategoryQuestion(QuestionCategory questionCategory);
        int DeleteQuestionCategory(int[] dsxoa);
        IEnumerable<QuestionCategory> SearchCategories(string txtSearch);
        int Delete(int id);
        QuestionCategory FindCategoryByID(int? id);
        IEnumerable<QuestionCategory> GetAllQuestionCategories();
        bool QuestionCategoryID(int id);
        IEnumerable<QuestionCategory> GetAllQuestionCategoriesActive();

    }
}
