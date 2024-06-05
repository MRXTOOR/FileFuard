using FileFuardSetup.DbContex;
using System;
using System.Data.Entity;

namespace FileFuardSetup
{
    public class ScanResultContext : DbContext
    {
        public DbSet<ScanResult> ScanResults { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ScanResult>().ToTable("ScanResults");
        }
    }
}