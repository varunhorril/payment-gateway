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
    public class CardRepository : IRepository<Card>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Card GetCardByIssuer(string issuerName)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    return context.Cards.Where(c => c.IssuerName
                                        .Equals(issuerName, StringComparison.OrdinalIgnoreCase))
                                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"[CardRepository][GetCardByIssuer] : {ex.Message}");
            }

            return null;
        }

        public IEnumerable<Card> GetAll()
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    return context.Cards.ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"[CardRepository][GetAll][FAILED] : {ex.Message}");
            }

            return null;
        }

        public Card GetById(Guid id)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    return context.Cards.Where(c => c.CardId == id)
                                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"[CardRepository][GetById][FAILED] : {ex.Message}");
            }

            return null;
        }

        public void Insert(Card entity)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    context.Cards.Add(entity);
                    context.SaveChanges();
                }
            }
            catch (RepositoryException ex)
            {
                logger.Error(ex, $"[CardRepository][Insert][FAILED] : {ex.Message}");

                throw ex;
            }
        }

        public void Update(Card entity)
        {
            try
            {
                using (var context = new PaymentGatewayContext())
                {
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException dbExp)
            {
                logger.Warn(dbExp, $"[DbUpdateConcurency][CardRepository] : {dbExp.Message}");

                throw dbExp;
            }
            catch (RepositoryException ex)
            {
                logger.Error(ex, $"[CardRepository][Update][FAILED] : {ex.Message}");

                throw ex;
            }
        }
    }
}
