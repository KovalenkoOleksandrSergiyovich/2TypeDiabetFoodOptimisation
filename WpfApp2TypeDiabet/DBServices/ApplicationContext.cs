using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2TypeDiabet.Models;
using static WpfApp2TypeDiabet.Services.OptimizeService;

namespace WpfApp2TypeDiabet.DBServices
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GoodInShop> GoodInShop { get; set; }
        public DbSet<GoodShopState> GoodShopState { get; set; }
        public DbSet<GoodState> GoodState { get; set; }
        public DbSet<Restriction> Restriction { get; set; }
        public DbSet<GoodBasket> GoodBasket { get; set; }
        public DbSet<UserGoodList> UserGoodList { get; set; }
        public DbSet<UserRestrictionList> UserRestrictionList { get; set; }
        public DbSet<GoodInBasket> GoodInBasket { get; set; }
        //public DbSet<OptimizeModel> OptimizeModel { get; set; }
        //public DbSet<Result> Result { get; set; }
        //public DbSet<ProductBasket> ProductBasket { get; set; }
        //public DbSet<GoodToOptimize> GoodToOptimize { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString = string.Format("Host={0};" +
                "Port={1};" +
                "Database={2};" +
                "Username={3};" +
                "Password={4}",
                "localhost", 5432, "dbDiabet", "postgres", "audiomachine");
            optionsBuilder.UseNpgsql(connString);
        }
    }
}
