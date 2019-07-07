using PaymentGateway.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Infrastructure.DAL
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        public BaseRepository()
        {

        }
        public void Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
