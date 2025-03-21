using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShmffPortal.Repository.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}
