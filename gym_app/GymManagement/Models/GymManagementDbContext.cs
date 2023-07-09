using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GymManagement.Models
{
    public partial class GymManagementDbContext : DbContext
    {
        public GymManagementDbContext()
        {
        }

        public GymManagementDbContext(DbContextOptions<GymManagementDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Contract> Contracts { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Facility> Facilities { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<TypesOfCourse> TypesOfCourses { get; set; } = null!;
        public virtual DbSet<TypesOfFacility> TypesOfFacilities { get; set; } = null!;
        public virtual DbSet<Staff> Staffs { get; set; } = null!;


        private string ConnectionString = "Data Source=HIEUPC;Initial Catalog=GymManagementDb;Integrated Security=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_Accounts_Staff");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");


                entity.HasOne(d => d.Contract)
                   .WithMany(p => p.Bookings)
                   .HasForeignKey(d => d.CustomerId)
                   .HasConstraintName("FK_Booking_Contracts");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Booking_Customers");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_Booking_Staff");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.FinishDate).HasColumnType("date");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Contracts_Courses");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Contracts_Customers");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_Contracts_Staffs");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.Type)
                    .HasConstraintName("FK_Courses_TypesOfCourse");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.IdentityNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.Property(e => e.FacilityId).HasColumnName("FacilityID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Facilities)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Facilities_TypesOfFacility");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TypesOfCourse>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK_TypeOfCourse");

                entity.ToTable("TypesOfCourse");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TypesOfFacility>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("TypesOfFacility");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.IdentityNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Staff_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
