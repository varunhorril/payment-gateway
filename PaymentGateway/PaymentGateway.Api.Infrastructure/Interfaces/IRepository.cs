using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Infrastructure.Interfaces
{
    interface IRepository<T> where T : class
    {
        void Insert(T obj);
        void Update(T obj);
    }
}
