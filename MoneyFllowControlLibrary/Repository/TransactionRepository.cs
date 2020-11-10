using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using MoneyFllowControlLibrary.Context;
using MoneyFllowControlLibrary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Type = MoneyFllowControlLibrary.Model.Type;

namespace MoneyFllow.Model
{
    public class TransactionRepository : ITransactionRepository
    {
        IApplicationContext db;

        public DateTime Today { get; protected set; }

        public TransactionRepository()
        {
            db = new ApplicationContext();
        }
        public TransactionRepository(IApplicationContext db)
        {
            this.db = db;
        }
        /// <summary>
        /// Выборка всех транзакций
        /// </summary>
        /// <returns></returns>
        public IQueryable<Transaction> GetAll()
        {
            return db.Transactions.Include(c=>c.Category).ThenInclude(t=>t.Type);
        }
        /// <summary>
        /// Получение транзакций по Id
        /// </summary>
        /// <param name="id">Идентификатор транзакции</param>
        /// <returns></returns>
        public Transaction GetById(int id)
        {
            return db.Transactions.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Добавление транзакции
        /// </summary>
        public int Add(Transaction transaction)
        {
            //При добавлении db.Transactions.Add(transaction) пытается добавить Category и Type
            db.Transactions.Add(new Transaction()
            {
                CategoryId = transaction.Category.Id,
                Description = transaction.Description,
                Date = transaction.Date,
                Summ = transaction.Summ,
            });
            return db.SaveChanges();
        }

        /// <summary>
        /// Удаление транзакции
        /// </summary>
        /// <param name="transaction">Удаляемая транзакция</param>
        /// <returns>
        /// -1 - Вызвалось исключение при удалении
        /// 0 - Удаление не произошло
        /// 1 - Удаление произошло успешно
        /// </returns>
        public int Delete(Transaction transaction)
        {
            int result = 0;
            try
            {
                if (transaction.Id > 0)
                {
                    db.Transactions.Remove(transaction);
                    result = db.SaveChanges();
                }
            }
            catch 
            {
                result = -1;
            }
            return result;
        }

        /// <summary>
        /// Выборка всех транзакций по TypeId
        /// </summary>
        /// <param name="typeId">Идентификатор для Type в БД</param>
        /// <returns></returns>
        public IQueryable<Transaction> GetByTypeId(int typeId)
        {            
            var transactions = db.Transactions.Include(c => c.Category).ThenInclude(t => t.Type).Where(t=>t.Category.TypeId==typeId);
            return transactions;
        }

        public IQueryable<Transaction> Filter(int typeId, DateTime dateStart, DateTime dateEnd)
        {
            IQueryable<Transaction> transactions;
            if (typeId > 0) transactions = GetByTypeId(typeId);
            else transactions = GetAll();
            transactions = transactions.Where(tr=> tr.Date>=dateStart).Where(tr => tr.Date<=dateEnd);
            return transactions;
        }

        public int Change(Transaction transaction)
        {
            db.Transactions.Update(transaction);
            var _transaction = db.Transactions.Find(transaction.Id);
            return db.SaveChanges();
        }
    }
}
