using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CategoryDao
    {
        OnlineShopDbContext db = null;
        public CategoryDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }

        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories.OrderByDescending(x => x.CreatedDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public long Insert(Category category)
        {
            category.CreatedDate = DateTime.Now;
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }

        public bool Update(Category entity)
        {
            try
            {
                var category = db.Categories.Find(entity.ID);
                category.Name = entity.Name;
                category.MetaTitle = entity.MetaTitle;
                category.MetaDescription = entity.MetaDescription;
                category.MetaKeyword = entity.MetaKeyword;
                category.ModifiedDate = DateTime.Now;
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
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Category GetById(long id)
        {
            return db.Categories.Find(id);
        }
    }
}
