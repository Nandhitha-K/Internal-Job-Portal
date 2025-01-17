﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApplyJobLibrary.Models;

public partial class ApplyJobDBContext : DbContext
{
    public ApplyJobDBContext()
    {
    }

    public ApplyJobDBContext(DbContextOptions<ApplyJobDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplyJob> ApplyJobs { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<JobPost> JobPosts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=ApplyJobDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplyJob>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.EmpId }).HasName("PK__ApplyJob__20E0BBA1A4F4B2DA");

            entity.ToTable("ApplyJob");

            entity.Property(e => e.EmpId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ApplicationStatus)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.AppliedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Emp).WithMany(p => p.ApplyJobs)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ApplyJob__EmpId__4CA06362");

            entity.HasOne(d => d.Post).WithMany(p => p.ApplyJobs)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ApplyJob__PostId__4BAC3F29");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB995ADDA2D9");

            entity.ToTable("Employee");

            entity.Property(e => e.EmpId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.EmpName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<JobPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__JobPost__AA126018026F9FE4");

            entity.ToTable("JobPost");

            entity.Property(e => e.PostId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
