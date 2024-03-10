using BusinessObject.Model;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.OrderRepo
{
    public class OrderRepository : IOrderRepository
    {
        public void AddOrder(Order order)
        {
            OrderDAO.Instance.AddOrder(order);
        }

        public void DeleteOrder(Order order)
        {
            OrderDAO.Instance.DeleteOrder(order);
        }

        public List<Order> GetOrdersByCourseID(int id)
        {
            return OrderDAO.Instance.GetOrdersByCourseID(id);
        }

        public List<Order> GetOrdersByUserID(int id)
        {
            return OrderDAO.Instance.GetOrdersByUserID(id);
        }

        public void UpdateOrder(Order order)
        {
            OrderDAO.Instance.UpdateOrder(order);
        }
    }
}
