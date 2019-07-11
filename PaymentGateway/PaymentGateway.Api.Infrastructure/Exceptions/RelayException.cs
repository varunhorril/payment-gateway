using PaymentGateway.Api.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Core.Exceptions
{
    public class RelayException : Exception, IMonitoring
    {
        public void TriggerAlert()
        {
            throw new NotImplementedException();
        }
    }
}
