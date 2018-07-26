using Microsoft.EntityFrameworkCore;
using RC.ADS.Data.Entity.AD_Account;
using RC.ADS.Data.Entity.AD_Article;
using RC.ADS.Data.Entity.AD_Integral;
using RC.ADS.Data.Entity.AD_Menber;
using RC.ADS.Data.Entity.AD_Order;
using System;

namespace RC.ADS.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
             
        }
        public DbSet<AccountInfo> AccountInfos { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }
        public DbSet<IntegralInfo> IntegralInfos { get; set; }
        public DbSet<Menber> Menbers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderAudit> OrderAudits { get; set; }

        

    }
}
