using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<HolidayDateEntity> Tb_holiday { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HolidayDateEntity>(entity =>
            {
                entity.ToTable("data_date").HasKey(m => m.date);
                //entity.Property(e => e.date).HasColumnName("date").ValueGeneratedNever();
                //entity.Property(e => e.ID).HasColumnName("ID").ValueGeneratedOnAdd();
                //entity.Property(e => e.StartDate).HasColumnName("StartDate");
                //entity.Property(e => e.EndDate).HasColumnName("EndDate");
                //entity.Property(e => e.Days).HasColumnName("Days").HasDefaultValue(0);
            });
        }
    }
}
