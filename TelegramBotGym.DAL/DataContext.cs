using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotGym.DAL
{
    public class DataContext : DbContext
    {
        #region Constructors

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region Properties

        public DbSet<SquatRound> SquatRounds { get; set; }



        #endregion
    }
}