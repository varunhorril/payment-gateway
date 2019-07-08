using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Infrastructure.Interfaces
{
    interface IRepository<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
        T GetById(Guid id);
        IEnumerable<T> GetAll();
    }
}
