using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.OrderRepo
{
    public interface IOrderRepository
    {
        public void AddOrder(Order order);
        public void DeleteOrder(Order order);
        public List<Order> GetOrdersByUserID(int id);
        public List<Order> GetOrdersByCourseID(int id);
        public void UpdateOrder(Order order);
    }
}
