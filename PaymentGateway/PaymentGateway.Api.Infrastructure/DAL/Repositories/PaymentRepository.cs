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
    public class PaymentRepository : IRepository<Payment>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IEnumerable<Payment> GetAll()
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    return context.Payments;
                }
            }
            catch (RepositoryException ex)
            {
                logger.Error(ex, $"[PaymentRepository][GetAll][FAILED] : {ex.Message}");
            }

            return null;
        }

        public Payment GetById(Guid id)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    return context.Payments.Where(p => p.PaymentId == id)
                                            .FirstOrDefault();
                }
            }
            catch (RepositoryException ex)
            {
                logger.Error(ex, $"[PaymentRepository][GetById][FAILED] : {ex.Message}");
            }

            return null;
        }

        public void Insert(Payment entity)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    entity.CreatedOn = DateTime.UtcNow;
                    context.Payments.Add(entity);
                    context.SaveChanges();
                }
            }
            catch (RepositoryException ex)
            {
                logger.Error(ex, $"[PaymentRepository][Insert][FAILED] : {ex.Message}");

                throw ex;
            }
        }

        public void Update(Payment entity)
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
                logger.Warn(dbExp, $"[DbUpdateConcurency][PaymentRepository] : {dbExp.Message}");

                throw dbExp;
            }
            catch (RepositoryException ex)
            {
                logger.Error(ex, $"[PaymentRepository][Update][FAILED] : {ex.Message}");

                throw ex;
            }
        }
    }
}
