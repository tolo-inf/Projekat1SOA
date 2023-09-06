using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GraphqlService.Repository
{
    public partial class SensorDbContext : DbContext
    {
        public SensorDbContext()
        {
        }

        public SensorDbContext(DbContextOptions<SensorDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SensorValue> SensorValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SensorValue>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("sensor_values");

                entity.Property(e => e.Id).HasColumnType("int(11)");
                entity.Property(e => e.Co2).HasColumnName("CO2");
                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
