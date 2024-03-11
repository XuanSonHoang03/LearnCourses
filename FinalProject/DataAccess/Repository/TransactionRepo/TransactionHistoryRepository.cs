using BusinessObject.Model;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.TransactionRepo
{
    public class TransactionHistoryRepository : ITransactionHistoryRepository
    {
        public void AddTransactionHistory(TransactionsHistory transactionsHistory)
        {
            TransactionHistoryDAO.Instance.AddTransactionHistory(transactionsHistory);
        }

        public void DeleteTransactionHistory(TransactionsHistory transactionsHistory)
        {
            TransactionHistoryDAO.Instance.DeleteTransactionHistory(transactionsHistory);
        }

        public List<TransactionsHistory> GetTransactionHistoryByUserId(int userId)
        {
            return TransactionHistoryDAO.Instance.GetAllbyUserId(userId);
        }
    }
}
