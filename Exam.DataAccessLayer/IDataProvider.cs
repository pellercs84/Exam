using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccessLayer
{
    /// <summary>
    /// It is a common abstract layer to make sure the unique design.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataProvider<T> where T : class
    {
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        void Add(T item);
        void Update(T item);
        void Remove(int itemId);
    }
}
