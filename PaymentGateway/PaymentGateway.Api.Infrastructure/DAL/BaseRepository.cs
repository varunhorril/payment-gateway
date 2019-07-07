using PaymentGateway.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace PaymentGateway.Api.Infrastructure.DAL
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        public Logger Logger;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public BaseRepository()
        {
            Logger = _logger;
        }
        public void Insert(T obj)
        {
            Logger.Debug("test");
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
