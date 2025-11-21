using Microsoft.EntityFrameworkCore;
using PruebaAppinit.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAppinit.Infrastructure
{
    public class AppinitDbContext : DbContext
    {
        public AppinitDbContext(DbContextOptions<AppinitDbContext> options) : base(options) { }
        public DbSet<GameEntity> Games { get; set; } = null!;
        public DbSet<RoundEntity> Rounds { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameEntity>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Player1Name).HasMaxLength(200).IsRequired();
                b.Property(x => x.Player2Name).HasMaxLength(200).IsRequired();
                b.HasMany(x => x.Rounds).WithOne(r => r.Game).HasForeignKey(r => r.GameId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RoundEntity>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.P1Move).HasMaxLength(50).IsRequired();
                b.Property(x => x.P2Move).HasMaxLength(50).IsRequired();
                b.Property(x => x.Outcome).HasMaxLength(50).IsRequired();
                b.Property(x => x.RoundNumber).IsRequired();
                b.HasIndex(r => new { r.GameId, r.RoundNumber }).IsUnique(false);
            });
        }
    }
}
