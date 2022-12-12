using ApiVida.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiVida.Contexts
{
    public partial class ElephantDBContext : DbContext
    {
        public ElephantDBContext()
        {
        }
        public ElephantDBContext(DbContextOptions<ElephantDBContext> options) : base(options)
        {
        }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<DoctorEntity> Doctors { get; set; }
        public virtual DbSet<PatientEntity> Patients { get; set; }
        public virtual DbSet<MedicalInsuranceEntity> MedicalInsuranceInformations { get; set; }
        public virtual DbSet<MedicalInsurancePlanEntity> MedicalInsurancePlans { get; set; }
        public virtual DbSet<MedicalSpecialityEntity> MedicalSpecialtyEntities { get; set; }

    }
}
