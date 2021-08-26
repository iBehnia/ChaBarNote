using System;
using Domain.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.DataModel
{
    public partial class ChaBarNoteContext : DbContext
    {
        public ChaBarNoteContext()
        {
        }

        public ChaBarNoteContext(DbContextOptions<ChaBarNoteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Folder> Folder { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserState> UserState { get; set; }
        public DbQuery<GetAllUser> GetAllUsers { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ChaBarNote;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.AdminId)
                    .HasColumnName("AdminID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Folder>(entity =>
            {
                entity.HasKey(e => new { e.FolderId, e.UserId });

                entity.Property(e => e.FolderId)
                    .HasColumnName("FolderID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.FolderName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Folder)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Folder_User");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => new { e.NoteId, e.FolderId, e.UserId });

                entity.Property(e => e.NoteId)
                    .HasColumnName("NoteID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FolderId).HasColumnName("FolderID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.NoteText).HasMaxLength(50);

                entity.Property(e => e.NoteTitle)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Note)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Note_User");

                entity.HasOne(d => d.Folder)
                    .WithMany(p => p.Note)
                    .HasForeignKey(d => new { d.FolderId, d.UserId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Note_Folder");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserState>(entity =>
            {
                entity.Property(e => e.UserStateId)
                    .HasColumnName("UserStateID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Type)
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });
        }
    }
}
