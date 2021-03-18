using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Financiera_backend.Entity
{
    public partial class DBContexto : DbContext
    {
        public DBContexto()
        {
        }

        public DBContexto(DbContextOptions<DBContexto> options)
            : base(options)
        {
        }

        public virtual DbSet<CatUserType> CatUserType { get; set; }
        public virtual DbSet<TabAccount> TabAccount { get; set; }
        public virtual DbSet<TabRecord> TabRecord { get; set; }
        public virtual DbSet<TabUser> TabUser { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatUserType>(entity =>
            {
                entity.ToTable("cat_user_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_bin");

                entity.Property(e => e.NameTranslation)
                    .HasColumnName("nameTranslation")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_bin");

                entity.Property(e => e.Permission)
                    .HasColumnName("permission")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_bin");
            });

            modelBuilder.Entity<TabAccount>(entity =>
            {
                entity.ToTable("tab_account");

                entity.HasIndex(e => e.IdUser)
                    .HasName("user_account_fk");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(13,4)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("idUser")
                    .HasColumnType("int(255) unsigned");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_bin");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.TabAccount)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("user_account_fk");
            });

            modelBuilder.Entity<TabRecord>(entity =>
            {
                entity.ToTable("tab_record");

                entity.HasIndex(e => e.IdAccount)
                    .HasName("account_record_fk");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(13,4)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("current_timestamp()")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.IdAccount).HasColumnType("int(255) unsigned");

                entity.Property(e => e.Operation)
                    .HasColumnName("operation")
                    .HasColumnType("enum('deposit','withdrawal')")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_bin");

                entity.HasOne(d => d.IdAccountNavigation)
                    .WithMany(p => p.TabRecord)
                    .HasForeignKey(d => d.IdAccount)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("account_record_fk");
            });

            modelBuilder.Entity<TabUser>(entity =>
            {
                entity.ToTable("tab_user");

                entity.HasIndex(e => e.IdUserType)
                    .HasName("user_typet_fl");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.IdUserType)
                    .HasColumnName("idUserType")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_bin");

                entity.HasOne(d => d.IdUserTypeNavigation)
                    .WithMany(p => p.TabUser)
                    .HasForeignKey(d => d.IdUserType)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("user_typet_fl");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
