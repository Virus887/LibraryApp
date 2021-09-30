using System;
using LibraryAPI.Models.POCOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LibraryAPI.Database
{
    public partial class LibraryDbContext : DbContext
    {
        public LibraryDbContext()
        {
        }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuthorPOCO> Authors { get; set; }
        public virtual DbSet<BookPOCO> Books { get; set; }
        public virtual DbSet<BookAuthorPOCO> BookAuthors { get; set; }
        public virtual DbSet<StatusHistoryPOCO> StatusHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<AuthorPOCO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<BookPOCO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Language).HasMaxLength(50);
            });

            modelBuilder.Entity<BookAuthorPOCO>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.AuthorId});

                entity.ToTable("BookAuthor");

                entity.HasOne(d => d.Author)
                    .WithMany()
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookAutho__Autho__276EDEB3");

                entity.HasOne(d => d.Book)
                    .WithMany()
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookAutho__BookI__286302EC");
            });

            modelBuilder.Entity<StatusHistoryPOCO>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("StatusHistory");

                entity.Property(e => e.Status).IsRequired();

                entity.HasOne(d => d.Book)
                    .WithMany()
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StatusHis__BookI__2A4B4B5E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
