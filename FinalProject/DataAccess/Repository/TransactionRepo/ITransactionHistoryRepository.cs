using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.TransactionRepo
{
    public interface ITransactionHistoryRepository
    {
        void AddTransactionHistory(TransactionsHistory transactionsHistory);
        List<TransactionsHistory> GetTransactionHistoryByUserId(int userId);
        void DeleteTransactionHistory(TransactionsHistory transactionsHistory);

    }
}
