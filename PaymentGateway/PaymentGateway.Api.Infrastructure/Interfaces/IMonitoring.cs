using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Business.Interfaces
{
    /// <summary>
    /// Monitoring process for custom exceptions
    /// </summary>
    interface IMonitoring
    {
        void TriggerAlert();
    }
}
