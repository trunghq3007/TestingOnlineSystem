using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Data.Repositories;
using TestingSystem.Models;

namespace TestingSystem.Sevice
{
    public class QuestionCategorySevice : IQuestionCategorySevice
    {
        public readonly IQuestionCategoryRepository questionCategory;
        public readonly IUnitOfWork unitOf;


        public QuestionCategorySevice(IQuestionCategoryRepository questionCategory, IUnitOfWork unitOf
        )
        {
            this.questionCategory = questionCategory;
            this.unitOf = unitOf;
        }

        public int AddCategoryQuestion(QuestionCategory category)
        {
            return questionCategory.AddCategoryQuestion(category);

        }

        public int Delete(int id)
        {
            return questionCategory.Delete(id);
        }

        public int DeleteQuestionCategory(int[] dsxoa)
        {
            return questionCategory.DeleteQuestionCategory(dsxoa);
        }

        public QuestionCategory FindCategoryByID(int? id)
        {
            return questionCategory.FindCategoryByID(id);
        }

        public IEnumerable<QuestionCategory> GetAll()
        {
            return questionCategory.GetAll();
        }

        public IEnumerable<QuestionCategory> GetAllQuestionCategories()
        {
            return questionCategory.GetAllQuestionCategories();
        }

        public IEnumerable<QuestionCategory> GetAllQuestionCategoriesActive()
        {
            return questionCategory.GetAllQuestionCategoriesActive();
        }

        public IEnumerable<User> GetAllUser()
        {
            return questionCategory.GetAllUser();
        }

        public QuestionCategory GetById(int id)
        {
            return questionCategory.GetById(id);
        }

        public bool QuestionCategoryID(int id)
        {
            return questionCategory.QuestionCategoryID(id);
        }

        public void SaveQuestionCategory()
        {
            unitOf.Commit();
        }

        public IEnumerable<QuestionCategory> Search(string txtSearch)
        {
            return questionCategory.SearchCategories(txtSearch);
        }

        public IEnumerable<QuestionCategory> SearchCategories(string txtSearch)
        {
            return questionCategory.SearchCategories(txtSearch);
        }

        public int UpdateCategoryQuestion(QuestionCategory category)
        {
            return questionCategory.UpdateCategoryQuestion(category);
        }
    }

    public interface IQuestionCategorySevice
    {
        IEnumerable<QuestionCategory> GetAll();
        int AddCategoryQuestion(QuestionCategory category);
        void SaveQuestionCategory();
        IEnumerable<User> GetAllUser();
        int UpdateCategoryQuestion(QuestionCategory questionCategory);

        IEnumerable<QuestionCategory> Search(string txtSearch);
        int DeleteQuestionCategory(int[] dsxoa);
        int Delete(int id);
        QuestionCategory GetById(int id);
        QuestionCategory FindCategoryByID(int? id);
        IEnumerable<QuestionCategory> GetAllQuestionCategories();
        bool QuestionCategoryID(int id);

        IEnumerable<QuestionCategory> SearchCategories(string txtSearch);
        IEnumerable<QuestionCategory> GetAllQuestionCategoriesActive();
    }
}
