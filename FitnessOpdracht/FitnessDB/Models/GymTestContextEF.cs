using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FitnessDB.Models;

public partial class GymTestContextEF : DbContext
{
    private string connectionString;
    public GymTestContextEF(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public GymTestContextEF(DbContextOptions<GymTestContextEF> options)
        : base(options)
    {
    }

    public virtual DbSet<CyclingsessionEF> Cyclingsessions { get; set; }

    public virtual DbSet<EquipmentEF> Equipment { get; set; }

    public virtual DbSet<MemberEF> Members { get; set; }

    public virtual DbSet<ProgramEF> Programs { get; set; }

    public virtual DbSet<ReservationEF> Reservations { get; set; }

    public virtual DbSet<ReservationTimeSlotEF> ReservationTimeSlots { get; set; }

    public virtual DbSet<RunningsessionDetailEF> RunningsessionDetails { get; set; }

    public virtual DbSet<RunningsessionMainEF> RunningsessionMains { get; set; }

    public virtual DbSet<TimeSlotEF> TimeSlots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CyclingsessionEF>(entity =>
        {
            entity.HasKey(e => e.CyclingsessionId).HasName("PK_cyclingsession_cyclingsession_id");

            entity.ToTable("cyclingsession");

            entity.Property(e => e.CyclingsessionId).HasColumnName("cyclingsession_id");
            entity.Property(e => e.AvgCadence).HasColumnName("avg_cadence");
            entity.Property(e => e.AvgWatt).HasColumnName("avg_watt");
            entity.Property(e => e.Comment)
                .HasMaxLength(500)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("comment");
            entity.Property(e => e.Date)
                .HasPrecision(0)
                .HasColumnName("date");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.MaxCadence).HasColumnName("max_cadence");
            entity.Property(e => e.MaxWatt).HasColumnName("max_watt");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Trainingtype)
                .HasMaxLength(45)
                .HasColumnName("trainingtype");

            entity.HasOne(d => d.Member).WithMany(p => p.Cyclingsessions)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cyclingsession$FK_cyclingtraining_members");
        });

        modelBuilder.Entity<EquipmentEF>(entity =>
        {
            entity.HasKey(e => e.EquipmentId).HasName("PK_equipment_equipment_id");

            entity.ToTable("equipment");

            entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");
            entity.Property(e => e.DeviceType)
                .HasMaxLength(45)
                .HasColumnName("device_type");
           /* modelBuilder.Entity<ReservationTimeSlot>()
            .HasOne(r => r.ReservationTimeSlotNavigation)
            .WithOne(r => r.ReservationTimeSlot)
            .HasForeignKey<ReservationEF>(r => r.ReservationTimeSlot.ReservationTimeSlotId);
           */
        });

        modelBuilder.Entity<MemberEF>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK_members_member_id");

            entity.ToTable("members");

            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasDefaultValue("Springfield")
                .HasColumnName("address");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .HasColumnName("first_name");
            entity.Property(e => e.Interests)
                .HasMaxLength(500)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("interests");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("last_name");
            entity.Property(e => e.Membertype)
                .HasMaxLength(20)
                .HasDefaultValue("BRONZE")
                .HasColumnName("membertype");
        });

        modelBuilder.Entity<ProgramEF>(entity =>
        {
            entity.HasKey(e => e.ProgramCode).HasName("PK_program_programCode");

            entity.ToTable("program");

            entity.Property(e => e.ProgramCode)
                .HasMaxLength(10)
                .HasColumnName("programCode");
            entity.Property(e => e.MaxMembers).HasColumnName("max_members");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Startdate)
                .HasPrecision(0)
                .HasColumnName("startdate");
            entity.Property(e => e.Target)
                .HasMaxLength(25)
                .HasColumnName("target");

            entity.HasMany(d => d.Members).WithMany(p => p.ProgramCodes)
                .UsingEntity<Dictionary<string, object>>(
                    "Programmember",
                    r => r.HasOne<MemberEF>().WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("programmembers$FK_programmembers_members"),
                    l => l.HasOne<ProgramEF>().WithMany()
                        .HasForeignKey("ProgramCode")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("programmembers$FK_programmembers_program"),
                    j =>
                    {
                        j.HasKey("ProgramCode", "MemberId").HasName("PK_programmembers_programCode");
                        j.ToTable("programmembers");
                        j.IndexerProperty<string>("ProgramCode")
                            .HasMaxLength(10)
                            .HasColumnName("programCode");
                        j.IndexerProperty<int>("MemberId").HasColumnName("member_id");
                    });
        });

        modelBuilder.Entity<ReservationEF>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PK_reservation_reservation_id");

            entity.ToTable("reservation");

            entity.Property(e => e.ReservationId).HasColumnName("reservation_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.MemberId).HasColumnName("member_id");

            entity.HasOne(d => d.Member).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reservation$FK_reservation_member");
        });

        modelBuilder.Entity<ReservationTimeSlotEF>(entity =>
        {
            entity.HasKey(e => e.ReservationTimeSlotId);
            entity.ToTable("ReservationTimeSlot");

            entity.HasOne(d => d.Equipment).WithMany(p => p.ReservationTimeSlots)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReservationTimeSlot_equipment");

            entity.HasOne(d => d.Reservation).WithMany(p => p.ReservationTimeSlots)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReservationTimeSlot_reservation");

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.ReservationTimeSlots)
                .HasForeignKey(d => d.TimeSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReservationTimeSlot_time_slot");
        });

        modelBuilder.Entity<RunningsessionDetailEF>(entity =>
        {
            entity.HasKey(e => new { e.RunningsessionId, e.SeqNr }).HasName("PK_runningsession_detail_runningsession_id");

            entity.ToTable("runningsession_detail");

            entity.Property(e => e.RunningsessionId).HasColumnName("runningsession_id");
            entity.Property(e => e.SeqNr).HasColumnName("seq_nr");
            entity.Property(e => e.IntervalSpeed).HasColumnName("interval_speed");
            entity.Property(e => e.IntervalTime).HasColumnName("interval_time");

            entity.HasOne(d => d.Runningsession).WithMany(p => p.RunningsessionDetails)
                .HasForeignKey(d => d.RunningsessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("runningsession_detail$FK_runningsession_detail_main");
        });

        modelBuilder.Entity<RunningsessionMainEF>(entity =>
        {
            entity.HasKey(e => e.RunningsessionId).HasName("PK_runningsession_main_runningsession_id");

            entity.ToTable("runningsession_main");

            entity.Property(e => e.RunningsessionId).HasColumnName("runningsession_id");
            entity.Property(e => e.AvgSpeed).HasColumnName("avg_speed");
            entity.Property(e => e.Date)
                .HasPrecision(0)
                .HasColumnName("date");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.MemberId).HasColumnName("member_id");

            entity.HasOne(d => d.Member).WithMany(p => p.RunningsessionMains)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("runningsession_main$FK_runningsession_main_members");
        });

        modelBuilder.Entity<TimeSlotEF>(entity =>
        {
            entity.HasKey(e => e.TimeSlotId).HasName("PK_time_slot_time_slot_id");

            entity.ToTable("time_slot");

            entity.Property(e => e.TimeSlotId).HasColumnName("time_slot_id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.PartOfDay)
                .HasMaxLength(20)
                .HasColumnName("part_of_day");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
