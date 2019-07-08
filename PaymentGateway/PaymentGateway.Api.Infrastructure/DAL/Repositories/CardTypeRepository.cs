using NLog;
using PaymentGateway.Core.Exceptions;
using PaymentGateway.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Infrastructure.DAL.Repositories
{
    public class CardTypeRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public CardType GetById(Guid id)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    return context.CardTypes.Where(ct => ct.CardTypeId == id)
                                            .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"[CardTypeRepository][GetById][FAILED] : {ex.Message}");
            }

            return null;
        }
    }
}
