using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Wsc2020Day2Session2Api.Models;

public partial class Wsc2020Day2Session2Context : DbContext
{
    public Wsc2020Day2Session2Context()
    {
    }

    public Wsc2020Day2Session2Context(DbContextOptions<Wsc2020Day2Session2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<CheckIn> CheckIns { get; set; }

    public virtual DbSet<Competitor> Competitors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Wsc2020Day2Session2;Trusted_Connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.ToTable("Announcement");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnnouncementDescription).HasColumnName("announcementDescription");
            entity.Property(e => e.AnnouncementTitle)
                .HasMaxLength(50)
                .HasColumnName("announcementTitle");
            entity.Property(e => e.Announcementdate)
                .HasColumnType("datetime")
                .HasColumnName("announcementdate");
        });

        modelBuilder.Entity<CheckIn>(entity =>
        {
            entity.ToTable("CheckIn");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompetitorId)
                .HasMaxLength(100)
                .HasColumnName("competitorId");

            entity.HasOne(d => d.Competitor).WithMany(p => p.CheckIns)
                .HasForeignKey(d => d.CompetitorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CheckIn_CheckIn");
        });

        modelBuilder.Entity<Competitor>(entity =>
        {
            entity.ToTable("Competitor");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("fullName");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
