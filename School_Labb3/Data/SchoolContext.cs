﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using School_Labb3.Models;

namespace School_Labb3.Data
{
    public partial class SchoolContext : DbContext
    {
        public SchoolContext()
        {
        }

        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Exam> Exams { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<StaffAdmin> StaffAdmins { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        //public virtual DbSet<GuestArrival> GuestArrivals { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=ULLSTENLENOVO; Initial Catalog=School;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.City).HasMaxLength(25);

                entity.Property(e => e.Homeland).HasMaxLength(25);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress).HasMaxLength(50);
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.ClassId).ValueGeneratedNever();

                entity.Property(e => e.ClassName).HasMaxLength(50);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseName).HasMaxLength(50);
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.ToTable("Exam");

                entity.Property(e => e.DateOfGrade).HasColumnType("date");

                entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseId");

                entity.Property(e => e.FkGradeId).HasColumnName("FK_GradeId");

                entity.Property(e => e.FkStaffAdminId).HasColumnName("FK_StaffAdminId");

                entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentId");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("FK_Exam_Course");

                entity.HasOne(d => d.FkGrade)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.FkGradeId)
                    .HasConstraintName("FK__Exam__FK_GradeId__04E4BC85");

                entity.HasOne(d => d.FkStaffAdmin)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.FkStaffAdminId)
                    .HasConstraintName("FK_Exam_StaffAdmin");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.FkStudentId)
                    .HasConstraintName("FK__Exam__FK_Student__02FC7413");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Position");

                entity.Property(e => e.PositionId).ValueGeneratedNever();

                entity.Property(e => e.PositionName).HasMaxLength(20);
            });

            modelBuilder.Entity<StaffAdmin>(entity =>
            {
                entity.ToTable("StaffAdmin");

                entity.Property(e => e.FkAddressId).HasColumnName("FK_AddressId");

                entity.Property(e => e.FkPositionId).HasColumnName("FK_PositionId");

                entity.Property(e => e.FkStaffId).HasColumnName("FK_StaffId");

                entity.HasOne(d => d.FkAddress)
                    .WithMany(p => p.StaffAdmins)
                    .HasForeignKey(d => d.FkAddressId)
                    .HasConstraintName("FK__StaffAdmi__FK_Ad__3E1D39E1");

                entity.HasOne(d => d.FkPosition)
                    .WithMany(p => p.StaffAdmins)
                    .HasForeignKey(d => d.FkPositionId)
                    .HasConstraintName("FK__StaffAdmi__FK_Po__3D2915A8");

                entity.HasOne(d => d.FkStaff)
                    .WithMany(p => p.StaffAdmins)
                    .HasForeignKey(d => d.FkStaffId)
                    .HasConstraintName("FK__StaffAdmi__FK_St__3C34F16F");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.DayOfBirth)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.FkAddressId).HasColumnName("FK_AddressId");

                entity.Property(e => e.FkClassId).HasColumnName("FK_ClassId");

                entity.Property(e => e.Gender)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.SecurityNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkAddress)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.FkAddressId)
                    .HasConstraintName("FK_Student_Address");

                entity.HasOne(d => d.FkClass)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.FkClassId)
                    .HasConstraintName("FK_Student_Class");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.HireDate)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
