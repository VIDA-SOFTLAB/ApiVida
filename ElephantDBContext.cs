using ApiVida.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiVida
{
    public partial class ElephantDBContext : DbContext 
    {
        public ElephantDBContext(){ 
        }
        public ElephantDBContext(DbContextOptions<ElephantDBContext> options) : base(options)        {
        }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<DoctorEntity> Doctors { get; set; }
        public virtual DbSet<PatientEntity> Patients { get; set; }
        public virtual DbSet<MedicalInsuranceEntity> MedicalInsuranceInformations { get; set; }

    }
}
