using Microsoft.EntityFrameworkCore;

namespace apdb11.models
{
    public class DoctorDbContext : DbContext
    {

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

        public DoctorDbContext()
        {
            
        }

        public DoctorDbContext(DbContextOptions options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(m => m.IdMedicament);
                entity.Property(m => m.IdMedicament).ValueGeneratedOnAdd();
                entity.ToTable("Medicament");
                entity.HasMany(m => m.PrescriptionMedicaments)
                    .WithOne(p => p.Medicament)
                    .HasForeignKey(p => p.IdMedicament)
                    .IsRequired();
            });
                
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor);
                entity.Property(e => e.IdDoctor).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.ToTable("Doctor");
                entity.HasMany(d => d.Prescriptions)
                    .WithOne(p => p.Doctor)
                    .HasForeignKey((p => p.IdDoctor))
                    .IsRequired();
                //check name, connection doesn't work
            });

            modelBuilder.Entity<PrescriptionMedicament>(entity =>
            {
                entity.HasKey(p => new {p.IdMedicament, p.IdPrescription});
                entity.ToTable("Prescription_Medicament");
            });
            
            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription);
                entity.Property(p => p.IdPrescription).ValueGeneratedOnAdd();
                entity.ToTable("Prescription");
                entity.HasMany(e => e.PrescriptionMedicaments)
                    .WithOne(p => p.Prescription)
                    .HasForeignKey(p => p.IdPrescription)
                    .IsRequired();
            });
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.IdPatient);
                entity.Property(p => p.IdPatient).ValueGeneratedOnAdd();
                entity.Property(p => p.FirstName).IsRequired();
                entity.Property(p => p.LastName).IsRequired();
                entity.ToTable("Patient");
                entity.HasMany(p => p.Prescriptions)
                    .WithOne(p => p.Patient)
                    .HasForeignKey(p => p.IdPatient)
                    .IsRequired();
            });
        }
        
    }
}