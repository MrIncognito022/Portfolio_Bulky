using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            
            this.dbSet = _db.Set<T>();
            //This simply mean _db.Categories = dbSet
        }
        #region Add To DB
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        #endregion

        #region Get Element By ID

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                //Here split is use if there are more navigation properties in the model
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }

            }
            query = query.Where(filter);
            return query.FirstOrDefault();
            //Logic is just like 
            //_db.Categories.where(u=>u.id = Id).FirstOrDefault();
        }
        #endregion

        #region Get All Element 

        //Include here is to add Foreign key relationship to Table use Category with Capital C
        //Like Category, CoverType Etc

        IEnumerable<T> IRepository<T>.GetAll( string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(!string.IsNullOrEmpty(includeProperties))
            {
                //Here split is use if there are more navigation properties in the model
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp); 
                }

            }
            return query.ToList();
            //Logic is just like 
            //_db.Categories.ToList();
        }
        #endregion

        #region Remove From Db
        void IRepository<T>.Remove(T entity)
        {
            dbSet.Remove(entity);
        }
        #endregion

        #region Remove Range from Db
        void IRepository<T>.RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
        #endregion

        #region Update Logic is not here
        //Update Logic is implemented is specific REPO
        #endregion


    }
}
