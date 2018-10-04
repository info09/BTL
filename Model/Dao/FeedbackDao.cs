﻿using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FeedbackDao
    {
        OnlineShopDbContext db = null;
        public FeedbackDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<Feedback> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Feedback> model = db.Feedbacks.OrderByDescending(x => x.CreatedDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
    }
}
