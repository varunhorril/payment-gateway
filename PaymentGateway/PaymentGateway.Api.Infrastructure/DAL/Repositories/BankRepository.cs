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
    public class BankRepository : IRepository<Bank>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Bank GetBankByName(string name)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    return context.Banks.Where(b => b.Name
                                        .Equals(name, StringComparison.OrdinalIgnoreCase))
                                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"[BankRepository][GetBankByName]: FAIL {ex.Message}");
            }

            return null;
        }

        public IEnumerable<Bank> GetAll()
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    return context.Banks;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"[CardRepository][GetAll][FAILED] : {ex.Message}");
            }

            return null;
        }

        public Bank GetById(Guid id)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    return context.Banks.Where(b => b.BankId == id)
                                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"[BankRepository][GetById][FAILED] : {ex.Message}");
            }

            return null;
        }

        public void Insert(Bank entity)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    context.Banks.Add(entity);
                    context.SaveChanges();
                }
            }
            catch (RepositoryException ex)
            {
                logger.Error(ex, $"[BankRepository][Insert][FAILED] : {ex.Message}");

                throw ex;
            }
        }

        public void Update(Bank entity)
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
                logger.Error(ex, $"[BankRepository][Update][FAILED] : {ex.Message}");

                throw ex;
            }
        }
    }
}
