using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        //Reference To all the Repository
        public IProductRepository Product { get; private set; }
        public ICategoryRepository Category { get;private set; }
        //Inject All Repositories Here
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
        }
        //This is a Global Method, new need to implement in specific Repository
        #region SaveMethod
        public void Save()
        {
            _db.SaveChanges();
        }
        #endregion 
    }
}
