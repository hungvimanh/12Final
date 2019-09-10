using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TwelveFinal.Repositories.Models
{
    public partial class TFContext : DbContext
    {
        public virtual DbSet<DistrictDAO> District { get; set; }
        public virtual DbSet<FormDAO> Form { get; set; }
        public virtual DbSet<GraduationInformationDAO> GraduationInformation { get; set; }
        public virtual DbSet<HighSchoolDAO> HighSchool { get; set; }
        public virtual DbSet<HighSchoolReferenceDAO> HighSchoolReference { get; set; }
        public virtual DbSet<MajorsDAO> Majors { get; set; }
        public virtual DbSet<NaturalSciencesDAO> NaturalSciences { get; set; }
        public virtual DbSet<PersonalInformationDAO> PersonalInformation { get; set; }
        public virtual DbSet<ProvinceDAO> Province { get; set; }
        public virtual DbSet<RegisterInformationDAO> RegisterInformation { get; set; }
        public virtual DbSet<ReserveMarkDAO> ReserveMark { get; set; }
        public virtual DbSet<SocialScienceDAO> SocialScience { get; set; }
        public virtual DbSet<TestExamDAO> TestExam { get; set; }
        public virtual DbSet<TownDAO> Town { get; set; }
        public virtual DbSet<UniversityDAO> University { get; set; }
        public virtual DbSet<UniversityAdmissionDAO> UniversityAdmission { get; set; }
        public virtual DbSet<University_MajorsDAO> University_Majors { get; set; }
        public virtual DbSet<UserDAO> User { get; set; }
        public virtual DbSet<__MigrationLogDAO> __MigrationLog { get; set; }
        // Unable to generate entity type for table 'dbo.__SchemaSnapshot'. Please see the warning messages.


        public TFContext(DbContextOptions<TFContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=.;initial catalog=12Final;persist security info=True;user id=sa;password=123456a@;multipleactiveresultsets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<DistrictDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_Table_1")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_Province");
            });

            modelBuilder.Entity<FormDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_Form")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.DepartmentCode)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.NumberForm)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.GraduationInformation)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.GraduationInformationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Form_Graduation");

                entity.HasOne(d => d.PersonalInfomartion)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.PersonalInfomartionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Form_Student");

                entity.HasOne(d => d.RegisterInformation)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.RegisterInformationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Form_Register");

                entity.HasOne(d => d.UniversityAdmission)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.UniversityAdmissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Form_UniversityAdmission");
            });

            modelBuilder.Entity<GraduationInformationDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_Graduation")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.ExceptLanguages).HasMaxLength(500);
            });

            modelBuilder.Entity<HighSchoolDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_HighSchool")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.HighSchools)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HighSchool_Province");
            });

            modelBuilder.Entity<HighSchoolReferenceDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_HighSchoolReference")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Grade10)
                    .WithMany(p => p.HighSchoolReferenceGrade10s)
                    .HasForeignKey(d => d.Grade10Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HighSchoolReference_HighSchool10");

                entity.HasOne(d => d.Grade11)
                    .WithMany(p => p.HighSchoolReferenceGrade11s)
                    .HasForeignKey(d => d.Grade11Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HighSchoolReference_HighSchool11");

                entity.HasOne(d => d.Grade12)
                    .WithMany(p => p.HighSchoolReferenceGrade12s)
                    .HasForeignKey(d => d.Grade12Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HighSchoolReference_HighSchool12");
            });

            modelBuilder.Entity<MajorsDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_Majors")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<NaturalSciencesDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_NaturalSciences")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PersonalInformationDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_Student")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Identify)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Nation)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.PlaceOfBirth)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.HighSchool)
                    .WithMany(p => p.PersonalInformations)
                    .HasForeignKey(d => d.HighSchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_HighSchoolReference");

                entity.HasOne(d => d.Town)
                    .WithMany(p => p.PersonalInformations)
                    .HasForeignKey(d => d.TownId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Town");
            });

            modelBuilder.Entity<ProvinceDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_Province")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RegisterInformationDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_Register")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.HasOne(d => d.ContestGroup)
                    .WithMany(p => p.RegisterInformations)
                    .HasForeignKey(d => d.ContestGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Register_Province");

                entity.HasOne(d => d.ContestUnit)
                    .WithMany(p => p.RegisterInformations)
                    .HasForeignKey(d => d.ContestUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Register_HighSchool");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.RegisterInformations)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Register_Test");
            });

            modelBuilder.Entity<ReserveMarkDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_ReserveMark")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Graduation)
                    .WithMany(p => p.ReserveMarks)
                    .HasForeignKey(d => d.GraduationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReserveMark_Graduation");
            });

            modelBuilder.Entity<SocialScienceDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_SocialScience")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TestExamDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_Test")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.ForeignLanguage)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.HasOne(d => d.Science)
                    .WithMany(p => p.TestExams)
                    .HasForeignKey(d => d.ScienceId)
                    .HasConstraintName("FK_Test_NaturalSciences");

                entity.HasOne(d => d.ScienceNavigation)
                    .WithMany(p => p.TestExams)
                    .HasForeignKey(d => d.ScienceId)
                    .HasConstraintName("FK_Test_SocialScience");
            });

            modelBuilder.Entity<TownDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("IX_Town")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Towns)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Town_District");
            });

            modelBuilder.Entity<UniversityDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_University")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UniversityAdmissionDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_UniversityAdmission")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.GraduateYear)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.PriorityType)
                    .IsRequired()
                    .HasMaxLength(2);
            });

            modelBuilder.Entity<University_MajorsDAO>(entity =>
            {
                entity.HasIndex(e => e.CX)
                    .HasName("CX_University_Majors")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.HasIndex(e => new { e.MajorsId, e.UniversityId, e.SubjectGroupType })
                    .HasName("IX_University_Majors")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.SubjectGroupType).HasMaxLength(10);

                entity.HasOne(d => d.Majors)
                    .WithMany(p => p.University_Majors)
                    .HasForeignKey(d => d.MajorsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Majors_Majors");

                entity.HasOne(d => d.UniversityAdmission)
                    .WithMany(p => p.University_Majors)
                    .HasForeignKey(d => d.UniversityAdmissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Majors_UniversityAdmission");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.University_Majors)
                    .HasForeignKey(d => d.UniversityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Majors_University");
            });

            modelBuilder.Entity<UserDAO>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("CX_User")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Student");
            });

            modelBuilder.Entity<__MigrationLogDAO>(entity =>
            {
                entity.HasKey(e => new { e.migration_id, e.complete_dt, e.script_checksum });

                entity.HasIndex(e => e.complete_dt)
                    .HasName("IX___MigrationLog_CompleteDt");

                entity.HasIndex(e => e.sequence_no)
                    .HasName("UX___MigrationLog_SequenceNo")
                    .IsUnique();

                entity.HasIndex(e => e.version)
                    .HasName("IX___MigrationLog_Version");

                entity.Property(e => e.script_checksum).HasMaxLength(64);

                entity.Property(e => e.applied_by)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.deployed).HasDefaultValueSql("((1))");

                entity.Property(e => e.package_version)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.release_version)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.script_filename)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.sequence_no).ValueGeneratedOnAdd();

                entity.Property(e => e.version)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingExt(modelBuilder);
        }

        partial void OnModelCreatingExt(ModelBuilder modelBuilder);
    }
}
