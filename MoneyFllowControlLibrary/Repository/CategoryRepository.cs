using Microsoft.EntityFrameworkCore;
using MoneyFllowControlLibrary.Context;
using MoneyFllowControlLibrary.Interface;
using MoneyFllowControlLibrary.Model;
using MoneyFllowControlLibrary.Repository;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;

namespace MoneyFllowControlLibrary.Controller
{
    public class CategoryRepository : ICategoryRepository
    {
        IApplicationContext db;
        public CategoryRepository()
        {
            new GenerateData().Create();
            db = new ApplicationContext();
        }

        public CategoryRepository(IApplicationContext db)
        {
            this.db = db;
        }

        public IQueryable<Category> GetAll()
        {
            IQueryable<Category> categories;
            try
            {
                categories = db.Categories.Include(t => t.Type); 
                return categories;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw (ex);
            }
        }

        public IQueryable<Category> GetByTypeId(int typeId)
        {
            return db.Categories.Where(x=>x.TypeId==typeId);
        }
    }
}
