using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }

        public Product GetById(long id)
        {
            return db.Products.Find(id);
        }

        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products.OrderByDescending(x => x.CreatedDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public long Insert(Product product)
        {
            product.CreatedDate = DateTime.Now;
            db.Products.Add(product);
            db.SaveChanges();
            return product.ID;
        }

        public bool Update(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);
                product.Name = entity.Name;
                product.Code = entity.Code;
                product.MetaTitle = entity.MetaTitle;
                product.Description = entity.Description;
                product.Image = entity.Image;
                product.MoreImage = entity.MoreImage;
                product.Price = entity.Price;
                product.PromotionPrice = entity.Warranty;
                product.Warranty = entity.Warranty;
                product.Quantity = entity.Quantity;
                product.MetaDescription = entity.MetaDescription;
                product.MetaKeywords = entity.MetaKeywords;
                product.ModifiedDate = DateTime.Now;
                product.Detail = entity.Detail;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
