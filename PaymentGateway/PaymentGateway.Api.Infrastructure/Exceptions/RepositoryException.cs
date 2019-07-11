using PaymentGateway.Api.Business.Interfaces;
using System;

namespace PaymentGateway.Core.Exceptions
{
    public class RepositoryException : Exception, IMonitoring
    {
        //Alert
        public void TriggerAlert()
        {
            throw new NotImplementedException();
        }
    }
}
