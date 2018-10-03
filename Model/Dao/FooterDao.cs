using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FooterDao
    {
        OnlineShopDbContext db = null;
        public FooterDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<Footer> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Footer> model = db.Footers.OrderByDescending(x => x.Name);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.Name).ToPagedList(page, pageSize);
        }

        public List<Footer> GetFooter()
        {
            return db.Footers.Where(x => x.Status==true).ToList();
        }

        public Footer GetById(string id)
        {
            return db.Footers.Find(id);
        }

        public string Insert(Footer footer)
        {
            db.Footers.Add(footer);
            db.SaveChanges();
            return footer.ID;
        }

        public bool Update(Footer entity)
        {
            try
            {
                var footer = db.Footers.Find(entity.ID);
                footer.Name = entity.Name;
                footer.Content = entity.Content;
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
                var footer = db.Footers.Find(id);
                db.Footers.Remove(footer);
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
