using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Business.Interfaces
{
    public interface IModuleBase
    {
        IResponseBase Process();
    }
}
