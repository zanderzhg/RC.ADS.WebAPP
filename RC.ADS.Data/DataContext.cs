using Microsoft.EntityFrameworkCore;
using RC.ADS.Data.Entity.AD_Article;
using System;

namespace RC.ADS.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
             
        }
        //public DbSet<Article> Articles { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }

    }
}
