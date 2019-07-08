﻿using PaymentGateway.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Infrastructure.DAL
{
    public class PaymentGatewayContext : DbContext
    {
        public PaymentGatewayContext() : base("Default")
        {

        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shopper> Shoppers { get; set; }
        public DbSet<Bank> Banks { get; set; }
    }
}
