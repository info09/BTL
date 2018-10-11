using Model.EF;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDetailDao
    {
        OnlineShopDbContext db = null;
        public OrderDetailDao()
        {
            db = new OnlineShopDbContext();
        }

        public bool Insert(OrderDetail orderDetail)
        {
            try
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public IEnumerable<OrderViewModel> ListOrder(DateTime? date, int page, int pageSize)
        {
            var data = (from a in db.OrderDetails
                        join b in db.Products
                        on a.ProductID equals b.ID
                        join c in db.Orders
                        on a.OrderID equals c.ID
                        select new OrderViewModel()
                        {
                            Name = b.Name,
                            Quantity = a.Quantity,
                            ShipName = c.ShipName,
                            ShipAddress = c.ShipAddress,
                            ShipEmail = c.ShipEmail,
                            ShipMobile = c.ShipMobile,
                            Price = a.Price,
                            CreatedDate=c.CreatedDate
                        });
            if (date!=null)
            {
                data = data.Where(x => x.CreatedDate.Equals(date));
            }
            return data.OrderByDescending(x => x.Name).ToPagedList(page, pageSize);
        }
    }
}
