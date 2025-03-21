using ShmffPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ShmffPortal.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private NewPortalDBEntities _context = null;
        private DbSet<T> table = null;
        public GenericRepository()
        {
            this._context = new NewPortalDBEntities();
            table = _context.Set<T>();
        }
        public GenericRepository(NewPortalDBEntities _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = this._context.Set<T>().Where(predicate);
            return query;
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}