using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContactDao
    {
        OnlineShopDbContext db = null;
        public ContactDao()
        {
            db = new OnlineShopDbContext();
        }

        public Contact GetActiveContact()
        {
            return db.Contacts.Single(x => x.Status == true);
        }

        public long InsertFeedback(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }

        

        public long Insert(Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();
            return contact.ID;
        }

        public bool Update(Contact entity)
        {
            try
            {
                var contact = db.Contacts.Find(entity.ID);
                contact.Content = entity.Content;
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
                var contact = db.Contacts.Find(id);
                db.Contacts.Remove(contact);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Contact GetById(long id)
        {
            return db.Contacts.Find(id);
        }
    }
}
