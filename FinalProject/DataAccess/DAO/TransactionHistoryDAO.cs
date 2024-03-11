using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.DAO
{
    public class TransactionHistoryDAO
    {
        private project_prn211Context context = new project_prn211Context();
        private static TransactionHistoryDAO instance;
        private static readonly object instanceLock = new object();
        
        public static TransactionHistoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TransactionHistoryDAO();
                    }
                    return instance;
                }
            }
        }
        private TransactionHistoryDAO() { }
        
        public List<TransactionsHistory> GetTransactionHistories()
        {
            return context.TransactionsHistories.ToList();
        }
        public void AddTransactionHistory(TransactionsHistory transactionHistory)
        {
            context.TransactionsHistories.Add(transactionHistory);
            context.SaveChanges();
        }
        public void UpdateTransactionHistory(TransactionsHistory transactionHistory)
        {
            context.TransactionsHistories.Update(transactionHistory);
            context.SaveChanges();
        }
        public void DeleteTransactionHistory(TransactionsHistory transactionHistory)
        {
            context.TransactionsHistories.Remove(transactionHistory);
            context.SaveChanges();
        }
        public List<TransactionsHistory> GetAllbyUserId(int userId)
        {
            return context.TransactionsHistories.Where(t => t.UserId == userId).ToList();
        }
    }
}
