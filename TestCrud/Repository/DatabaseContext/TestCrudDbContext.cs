using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TestCrud.Models.DBModels;

#nullable disable

namespace TestCrud.Repository.DatabaseContext
{
    public partial class TestCrudDbContext : DbContext
    {
        public TestCrudDbContext()
        {
        }

        public TestCrudDbContext(DbContextOptions<TestCrudDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbSkillType> TbSkillTypes { get; set; }
        public virtual DbSet<TbUser> TbUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-68PBUD8R;Database=Test-User;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<TbSkillType>(entity =>
            {
                entity.ToTable("tb_SkillTypes");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Display)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbUser>(entity =>
            {
                entity.ToTable("tb_Users");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Designation)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SkillId).HasColumnName("Skill_Id");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.TbUsers)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK__tb_Users__Skill___3C69FB99");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
