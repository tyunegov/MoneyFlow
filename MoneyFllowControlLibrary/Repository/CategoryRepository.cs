using MoneyFllowControlLibrary.Context;
using MoneyFllowControlLibrary.Interface;
using MoneyFllowControlLibrary.Model;
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
            db = new ApplicationContext();
        }

        public CategoryRepository(IApplicationContext db)
        {
            this.db = db;
        }

        public IQueryable<Category> GetAll()
        {
            var categories = db.Categories;
            foreach(var v in categories)
            {
                v.Type = (from p in db.Types
                                where v.TypeId == p.Id
                                select p).FirstOrDefault();
            }
            return categories;
        }

        public IQueryable<Category> GetByTypeId(int typeId)
        {
            return db.Categories.Where(x=>x.TypeId==typeId);
        }
    }
}
