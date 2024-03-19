using BusinessObject.Model;
using LearnCourses.Models;

namespace LearnCourses.Service
{
    public interface IVnPayService
    {
        string CreatePayment(HttpContext context, TransactionsHistory model);

        OrderInfo PaymentExecute(IQueryCollection collection);
        string GetPaymentStatus(string orderInfo);
    }
}
