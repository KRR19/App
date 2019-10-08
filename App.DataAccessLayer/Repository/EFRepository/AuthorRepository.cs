using System;
using System.Collections.Generic;
using System.Text;
using App.DataAccessLayer.Repository.Interfaces;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Repository.Base;
using System.Linq;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class AuthorRepository : IAuthorRepository
    {
        private ApplicationContext DB;
        public AuthorRepository(ApplicationContext db)
        {
            DB = db;
        }
       public void Create (Author author)
        {
            DB.Authors.AddAsync(author);
            DB.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            Author item = DB.Authors.Find(id);
            if (item != null)
            {
                item.IsRemoved = true;
                DB.Authors.Update(item);
                DB.SaveChanges();
            }
        }

        public  Author Read(int id)
        {
            return DB.Authors.Find(id);
            
        }

        public void Update(Author item)
        {            
            if(item!=null)
            {
                DB.Authors.Update(item);
                DB.SaveChanges();
            }

        }
    }
}
