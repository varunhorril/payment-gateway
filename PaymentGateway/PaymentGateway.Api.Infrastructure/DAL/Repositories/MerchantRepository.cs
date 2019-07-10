using NLog;
using PaymentGateway.Api.Infrastructure.Interfaces;
using PaymentGateway.Core.Exceptions;
using PaymentGateway.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Infrastructure.DAL.Repositories
{
    public class MerchantRepository : IRepository<Merchant>
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IEnumerable<Merchant> GetAll()
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    return context.Merchants.ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"[MerchantRepository][GetAll][FAILED] : {ex.Message}");
            }

            return null;
        }

        public Merchant GetById(Guid id)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    return context.Merchants.Where(m => m.MerchantId == id)
                                            .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"[MerchantRepository][GetById][FAILED] : {ex.Message}");
            }

            return null;
        }

        public void Insert(Merchant entity)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    context.Merchants.Add(entity);
                    context.SaveChanges();
                }
            }
            catch (RepositoryException ex)
            {
                logger.Error(ex, $"[MerchantRepository][Insert][FAILED] : {ex.Message}");

                throw ex;
            }
        }

        public void Update(Merchant entity)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (RepositoryException ex)
            {
                logger.Error(ex, $"[MerchantRepository][Update][FAILED] : {ex.Message}");

                throw ex;
            }
        }
    }
}
