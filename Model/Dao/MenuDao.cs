using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MenuDao
    {
        OnlineShopDbContext db = null;
        public MenuDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Menu> ListByGroupId(int groupId)
        {
            return db.Menus.Where(x => x.TypeID == groupId && x.Status==true).OrderBy(x=>x.DisplayOrder).ToList();
        }

        public IEnumerable<Menu> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Menu> model = db.Menus.OrderByDescending(x => x.Text);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Text.Contains(searchString));
            }
            return model.OrderByDescending(x => x.Text).ToPagedList(page, pageSize);
        }

        public long Insert(Menu menu)
        {
            db.Menus.Add(menu);
            db.SaveChanges();
            return menu.ID;
        }

        public bool Update(Menu entity)
        {
            try
            {
                var menu = db.Menus.Find(entity.ID);
                menu.Text = entity.Text;
                menu.Link = entity.Link;
                menu.DisplayOrder = entity.DisplayOrder;
                menu.TypeID = entity.TypeID;
                menu.Target = entity.Target;
                
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
                var menu = db.Menus.Find(id);
                db.Menus.Remove(menu);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Menu GetById(long id)
        {
            return db.Menus.Find(id);
        }
    }
}
