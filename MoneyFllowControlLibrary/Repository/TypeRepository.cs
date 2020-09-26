using MoneyFllowControlLibrary.Context;
using MoneyFllowControlLibrary.Interface;
using MoneyFllowControlLibrary.Repository;
using System;
using System.Linq;

namespace MoneyFllowControlLibrary.Model
{
    public class TypeRepository : ITypeTransactionRepository
    {
        IApplicationContext db;
        public TypeRepository()
        {
            db = new ApplicationContext();
        }

        public TypeRepository(IApplicationContext db)
        {
            this.db = db;
        }

        public IQueryable<Type> GetAll()
        {
            IQueryable<Type> result;
            int isSuccessGenerateTypeRepository=0;
            try
            {
                result = db.Types;
                if (result.Count() == 0)
                {
                    isSuccessGenerateTypeRepository = new GenerateData().Create();
                    if ((isSuccessGenerateTypeRepository&1)>0 || (isSuccessGenerateTypeRepository&2)>0) GetAll();
                }
                foreach (var v in result)
                {
                    v.Categories = (from p in db.Categories
                                    where v.Id == p.TypeId
                                    select p).ToList();
                }
                return result;
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex);
                throw(ex);
            }
        }
    }
}
