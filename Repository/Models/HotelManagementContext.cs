using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repository.Models;

public partial class HotelManagementContext : DbContext
{
    public HotelManagementContext()
    {
    }

    public HotelManagementContext(DbContextOptions<HotelManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booked> Bookeds { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomService> RoomServices { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
               => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsetting.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booked>(entity =>
        {
            entity.HasKey(e => e.BookedId).HasName("PK__Booked__3C92B7D669990CC3");

            entity.ToTable("Booked");

            entity.Property(e => e.BookedId)
                .ValueGeneratedNever()
                .HasColumnName("bookedID");
            entity.Property(e => e.Cost)
                .HasColumnType("money")
                .HasColumnName("cost");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.GuestName)
                .HasMaxLength(100)
                .HasColumnName("guestName");
            entity.Property(e => e.RoomNumber).HasColumnName("roomNumber");
            entity.Property(e => e.StartDate).HasColumnName("startDate");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.RoomNumberNavigation).WithMany(p => p.Bookeds)
                .HasForeignKey(d => d.RoomNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booked__roomNumb__4BAC3F29");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__6C3BF5DE5CE18F6B");

            entity.ToTable("Room");

            entity.Property(e => e.RoomId)
                .ValueGeneratedNever()
                .HasColumnName("roomID");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.RoomCapacity).HasColumnName("roomCapacity");
            entity.Property(e => e.RoomDetail)
                .HasMaxLength(220)
                .HasColumnName("roomDetail");
            entity.Property(e => e.RoomStatus).HasColumnName("roomStatus");
            entity.Property(e => e.RoomType)
                .HasMaxLength(100)
                .HasColumnName("roomType");
        });

        modelBuilder.Entity<RoomService>(entity =>
        {
            entity.HasKey(e => new { e.RoomId, e.ServiceId }).HasName("PK__roomServ__886EF2ED5D185DCD");

            entity.ToTable("roomService");

            entity.Property(e => e.RoomId).HasColumnName("roomID");
            entity.Property(e => e.ServiceId).HasColumnName("serviceID");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(100)
                .HasColumnName("serviceName");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Room).WithMany(p => p.RoomServices)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__roomServi__roomI__5070F446");

            entity.HasOne(d => d.Service).WithMany(p => p.RoomServices)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__roomServi__servi__5165187F");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__4550733FED647EFD");

            entity.Property(e => e.ServiceId)
                .ValueGeneratedNever()
                .HasColumnName("serviceID");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(100)
                .HasColumnName("serviceName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__user__CB9A1CDF28642371");

            entity.ToTable("user");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userID");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("fullName");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
