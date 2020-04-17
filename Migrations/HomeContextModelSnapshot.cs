﻿// <auto-generated />
using System;
using Dispatch.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dispatch.Migrations
{
    [DbContext(typeof(HomeContext))]
    partial class HomeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Dispatch.Models.Assignment", b =>
                {
                    b.Property<int>("AssignmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("RescuerId");

                    b.Property<int>("UnitId");

                    b.Property<DateTime>("UpdatdAt");

                    b.HasKey("AssignmentId");

                    b.HasIndex("RescuerId");

                    b.HasIndex("UnitId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("Dispatch.Models.Dispatchh", b =>
                {
                    b.Property<int>("DispatchhId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("IncidentId");

                    b.Property<int>("UnitId");

                    b.Property<DateTime>("UpdatdAt");

                    b.HasKey("DispatchhId");

                    b.HasIndex("IncidentId");

                    b.HasIndex("UnitId");

                    b.ToTable("Dispatches");
                });

            modelBuilder.Entity("Dispatch.Models.Incident", b =>
                {
                    b.Property<int>("IncidentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Location")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<DateTime>("UpdatdAt");

                    b.Property<int>("UserId");

                    b.HasKey("IncidentId");

                    b.HasIndex("UserId");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("Dispatch.Models.Rescuer", b =>
                {
                    b.Property<int>("RescuerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<bool>("IsAssigned");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Rank");

                    b.Property<DateTime>("UpdatdAt");

                    b.Property<int>("UserId");

                    b.HasKey("RescuerId");

                    b.HasIndex("UserId");

                    b.ToTable("Rescuers");
                });

            modelBuilder.Entity("Dispatch.Models.Unit", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("IsAvailable");

                    b.Property<int>("Number");

                    b.Property<string>("NumberType")
                        .IsRequired();

                    b.Property<string>("ResponseStatus");

                    b.Property<string>("UnitType")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("UnitId");

                    b.HasIndex("UserId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("Dispatch.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Dispatch.Models.Assignment", b =>
                {
                    b.HasOne("Dispatch.Models.Rescuer", "RiderAssigned")
                        .WithMany("AssignedUnit")
                        .HasForeignKey("RescuerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dispatch.Models.Unit", "UnitAssigned")
                        .WithMany("personnel")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dispatch.Models.Dispatchh", b =>
                {
                    b.HasOne("Dispatch.Models.Incident", "DispatchedIncident")
                        .WithMany("dispatchedUnits")
                        .HasForeignKey("IncidentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dispatch.Models.Unit", "UnitDispatched")
                        .WithMany("calls")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dispatch.Models.Incident", b =>
                {
                    b.HasOne("Dispatch.Models.User", "Dispatcher")
                        .WithMany("IncidentsDispatched")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dispatch.Models.Rescuer", b =>
                {
                    b.HasOne("Dispatch.Models.User", "creator")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dispatch.Models.Unit", b =>
                {
                    b.HasOne("Dispatch.Models.User", "creator")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
