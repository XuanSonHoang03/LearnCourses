using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
    {
        public project_prn211Context context = new project_prn211Context();
        
        private static OrderDAO instance;
        private static readonly object instanceLock = new object();
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }
        public void AddOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }
        public void DeleteOrder(Order order)
        {
            context.Orders.Remove(order);
            context.SaveChanges();
        }
        public List<Order> GetOrdersByUserID(int id)
        {
            return context.Orders.Where(o => o.UserId == id).ToList();
        }
        public List<Order> GetOrdersByCourseID(int id)
        {
            return context.Orders.Where(o => o.CourseId == id).ToList();
        }
        public void UpdateOrder(Order order)
        {
            context.Orders.Update(order);
            context.SaveChanges();
        }


    }

}
