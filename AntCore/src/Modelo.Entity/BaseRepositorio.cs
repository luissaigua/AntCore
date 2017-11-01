using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Modelo.Entity.Interfaces;



namespace Modelo.Entity
{
    public abstract class BaseRepositorio<T> : IRepository<T> where T : class
    {
        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public List<T> Filter(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<T> Filter(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {

            throw new NotImplementedException();

        }

        public List<T> GetAll(List<Expression<Func<T, object>>> includes)
        {
            throw new NotImplementedException();
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T Single(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
