using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PatientsAPI.DAL
{
    public class PatientsContext : DbContext
    {
        public PatientsContext() { }

        public PatientsContext(DbContextOptions<PatientsContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.Gender)
                .IsUnicode(false);
        }
    }
}
