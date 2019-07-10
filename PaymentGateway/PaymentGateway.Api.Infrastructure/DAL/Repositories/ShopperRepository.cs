using NLog;
using PaymentGateway.Api.Infrastructure.Interfaces;
using PaymentGateway.Core.Exceptions;
using PaymentGateway.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Infrastructure.DAL.Repositories
{
    public class ShopperRepository : IRepository<Shopper>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Shopper GetShopperByCardNumber(string cardNumber)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    return context.Shoppers.Where(s => s.CardNumber
                                           .Equals(cardNumber, StringComparison.OrdinalIgnoreCase))
                                           .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"[ShopperRepository][GetShopperByCardNumber][FAILED] : {ex.Message}");
            }

            return null;
        }

        public IEnumerable<Shopper> GetAll()
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    return context.Shoppers.ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"[ShopperRepository][GetAll][FAILED] : {ex.Message}");
            }

            return null;
        }

        public Shopper GetById(Guid id)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    return context.Shoppers.Where(s => s.ShopperId == id)
                                           .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"[ShopperRepository][GetById][FAILED] : {ex.Message}");
            }

            return null;
        }

        public void Insert(Shopper entity)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    entity.CreatedOn = DateTime.UtcNow;
                    context.Shoppers.Add(entity);
                    context.SaveChanges();
                }
            }
            catch (RepositoryException ex)
            {
                logger.Error(ex, $"[ShopperRepository][Insert][FAILED] : {ex.Message}");

                throw ex;
            }
        }

        public void Update(Shopper entity)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    entity.UpdatedOn = DateTime.UtcNow;
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException dbExp)
            {
                logger.Warn(dbExp, $"[DbUpdateConcurency][ShopperRepository] : {dbExp.Message}");

                throw dbExp;
            }
            catch (RepositoryException ex)
            {
                logger.Error(ex, $"[ShopperRepository][Update][FAILED] : {ex.Message}");

                throw ex;
            }
        }
    }
}
