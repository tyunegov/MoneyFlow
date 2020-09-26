using Castle.Core.Internal;
using MoneyFllowControlLibrary.Context;
using MoneyFllowControlLibrary.Model;
using System.Collections.Generic;
using System.Linq;

namespace MoneyFllowControlLibrary.Repository
{
    public class GenerateData : IGenerateData
    {
        IApplicationContext db;
        public GenerateData()
        {
            db = new ApplicationContext();
        }
        public GenerateData(IApplicationContext db)
        {
            this.db = db;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// 1 - success
        /// -1 - error
        /// 2 - data exist
        /// </returns>
        public int Create()
        {
            int result;
            try
            {
                if (db.Categories.IsNullOrEmpty() &&
                db.Types.IsNullOrEmpty())
                {
                    AddTypes();
                    AddCategories();
                    AddTransactions();
                    db.SaveChanges();
                    result = 1;
                }
                else result = 2;
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        void AddTransactions()
        {
            var list = new List<Transaction>
            {
                new Transaction() {CategoryId=1, Date=new System.DateTime(), Summ=120},
            };
            db.Transactions.AddRange(list);
            db.SaveChanges();
        }

        void AddTypes()
        {
            var list = new List<Type>
            {
                new Type() {Title="Доход" },
                new Type() {Title="Расход" },
                new Type() {Title="Перевод" },
            };
            db.Types.AddRange(list);
            db.SaveChanges();
        }

        void AddCategories()
        {
            var list = new List<Category>
            {
                new Category() { Title = "Дивиденды", TypeId = 1 },
                new Category() { Title = "Зарплата", TypeId = 1 },
                new Category() { Title = "Премия", TypeId = 1 },
                new Category() { Title = "Подарок", TypeId = 1 },
                new Category() { Title = "Подработка", TypeId = 1 },
                new Category() { Title = "Проценты по вкладу", TypeId = 1 },

                new Category() { Title = "Автомобиль", TypeId = 2 } ,
                new Category() { Title = "Благотворительность", TypeId = 2 },
                new Category() { Title = "Подарки", TypeId = 2 },
                new Category() { Title = "Бытовая техника", TypeId = 2 },
                new Category() { Title = "Домашние животные", TypeId = 2 },
                new Category() { Title = "Здоровье и красота", TypeId = 2 },
                new Category() { Title = "Ипотекаб долги, кредиты", TypeId = 2 },
                new Category() { Title = "Квартира и связь", TypeId = 2 },
                new Category() { Title = "Налоги", TypeId = 2 },
                new Category() { Title = "Образование", TypeId = 2 },
                new Category() { Title = "Одежда и аксессуары", TypeId = 2 },
                new Category() { Title = "Отдых и развлечение", TypeId = 2 },
                new Category() { Title = "Питание", TypeId = 2 },
                new Category() { Title = "Разное", TypeId = 2 },
                new Category() { Title = "Ремонт", TypeId = 2 },
                new Category() { Title = "Товары для дома", TypeId = 2 },
                new Category() { Title = "Хобби", TypeId = 2 },
        };
            db.Categories.AddRange(list);
            db.SaveChanges();
        }
    }
}