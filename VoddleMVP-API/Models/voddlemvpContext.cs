using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VoddleMVP_API
{
    public partial class voddlemvpContext : DbContext
    {
        public voddlemvpContext()
        {
        }

        public voddlemvpContext(DbContextOptions<voddlemvpContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tbluser> Tblusers { get; set; }
        public virtual DbSet<Tblvideo> Tblvideos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tbluser>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("tblusers_pk");

                entity.ToTable("tblusers", "voddle");

                entity.Property(e => e.Userid)
                    .ValueGeneratedNever()
                    .HasColumnName("userid");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Passwordhash)
                    .IsRequired()
                    .HasColumnName("passwordhash");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Tblvideo>(entity =>
            {
                entity.HasKey(e => e.Videoid)
                    .HasName("tblvideos_pk");

                entity.ToTable("tblvideos", "voddle");

                entity.HasIndex(e => e.Videoid, "tblvideos_videoid_uindex")
                    .IsUnique();

                entity.Property(e => e.Videoid)
                    .ValueGeneratedNever()
                    .HasColumnName("videoid");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Published)
                    .HasColumnType("date")
                    .HasColumnName("published");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Userpageurl)
                    .IsRequired()
                    .HasColumnName("userpageurl");

                entity.Property(e => e.Videourl)
                    .IsRequired()
                    .HasColumnName("videourl");

                entity.Property(e => e.Vidthumbnailurl)
                    .IsRequired()
                    .HasColumnName("vidthumbnailurl");

                entity.Property(e => e.Vidtitle)
                    .IsRequired()
                    .HasColumnName("vidtitle");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tblvideos)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblvideos_tblusers_userid_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
