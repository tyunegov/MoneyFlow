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
        /// Создает данные, если БД пустая
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
                new Transaction() {CategoryId=1, Date=new System.DateTime(), Summ=120, Description="Описание"},
            };
            db.Transactions.AddRange(list);
            db.SaveChanges();
        }

        void AddTypes()
        {
            var list = new List<Type>
            {
                new Type() {Id=1, Name="Доход" },
                new Type() {Id=2, Name="Расход" },
                new Type() {Id=3, Name="Перевод" },
            };
            db.Types.AddRange(list);
            db.SaveChanges();
        }

        void AddCategories()
        {
            var list = new List<Category>
            {
                new Category() { Name = "Дивиденды", TypeId = 1 },
                new Category() { Name = "Зарплата", TypeId = 1 },
                new Category() { Name = "Премия", TypeId = 1 },
                new Category() { Name = "Подарок", TypeId = 1 },
                new Category() { Name = "Подработка", TypeId = 1 },
                new Category() { Name = "Проценты по вкладу", TypeId = 1 },

                new Category() { Name = "Автомобиль", TypeId = 2 } ,
                new Category() { Name = "Благотворительность", TypeId = 2 },
                new Category() { Name = "Подарки", TypeId = 2 },
                new Category() { Name = "Бытовая техника", TypeId = 2 },
                new Category() { Name = "Домашние животные", TypeId = 2 },
                new Category() { Name = "Здоровье и красота", TypeId = 2 },
                new Category() { Name = "Ипотекаб долги, кредиты", TypeId = 2 },
                new Category() { Name = "Квартира и связь", TypeId = 2 },
                new Category() { Name = "Налоги", TypeId = 2 },
                new Category() { Name = "Образование", TypeId = 2 },
                new Category() { Name = "Одежда и аксессуары", TypeId = 2 },
                new Category() { Name = "Отдых и развлечение", TypeId = 2 },
                new Category() { Name = "Питание", TypeId = 2 },
                new Category() { Name = "Разное", TypeId = 2 },
                new Category() { Name = "Ремонт", TypeId = 2 },
                new Category() { Name = "Товары для дома", TypeId = 2 },
                new Category() { Name = "Хобби", TypeId = 2 },

                 new Category() { Name = "Семья", TypeId = 3 },
                  new Category() { Name = "Другое", TypeId = 3 },
        };
            db.Categories.AddRange(list);
            db.SaveChanges();
        }
    }
}