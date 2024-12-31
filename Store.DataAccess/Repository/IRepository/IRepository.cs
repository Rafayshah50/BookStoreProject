using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class//Here I am creating it generic because In this interface it will get multiple classes
    {
        IEnumerable<T> GetAll(string? includeProperties = null);//This will get all the elements because it will get in list it is IEnumerable
        T GetSingle(Expression<Func<T, bool>> filter, string? includeProperties = null);//Here I am passing a single value so here we are using lambda expression.
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
