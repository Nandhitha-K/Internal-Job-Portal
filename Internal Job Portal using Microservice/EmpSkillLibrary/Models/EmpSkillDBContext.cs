using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmpSkillLibrary.Models;

public partial class EmpSkillDBContext : DbContext
{
    public EmpSkillDBContext()
    {
    }

    public EmpSkillDBContext(DbContextOptions<EmpSkillDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmpSkill> EmpSkills { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=EmpSkillDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmpSkill>(entity =>
        {
            entity.HasKey(e => new { e.EmpId, e.SkillId }).HasName("PK__EmpSkill__C2D7B281F54603A6");

            entity.ToTable("EmpSkill");

            entity.Property(e => e.EmpId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SkillId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SkillExperience).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Emp).WithMany(p => p.EmpSkills)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmpSkill__EmpId__09A971A2");

            entity.HasOne(d => d.Skill).WithMany(p => p.EmpSkills)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmpSkill__SkillI__0A9D95DB");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB991E49D227");

            entity.ToTable("Employee");

            entity.Property(e => e.EmpId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.EmpName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("PK__Skill__DFA09187A6B4A210");

            entity.ToTable("Skill");

            entity.Property(e => e.SkillId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SkillName)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
