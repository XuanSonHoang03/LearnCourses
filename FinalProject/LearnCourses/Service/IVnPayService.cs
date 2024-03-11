using LearnCourses.Models;

namespace LearnCourses.Service
{
    public interface IVnPayService
    {
        string CreatePayment(HttpContext context, OrderInfo model);

        OrderInfo PaymentExecute(IQueryCollection collection);
        string GetPaymentStatus(string orderInfo);
    }
}
