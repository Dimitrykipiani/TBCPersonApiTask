﻿// <auto-generated />
using System;
using DataContext.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataContext.Migrations
{
    [DbContext(typeof(PersonReferenceDbContext))]
    [Migration("20200813132722_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Models.PersonAggregate.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<int>("CityId");

                    b.Property<string>("FistName");

                    b.Property<string>("ImageURL");

                    b.Property<string>("LastName");

                    b.Property<string>("PrivateNumber");

                    b.Property<int>("Sex");

                    b.HasKey("Id");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(1732, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityId = 2,
                            FistName = "George",
                            ImageURL = "",
                            LastName = "Washington",
                            PrivateNumber = "04001099344",
                            Sex = 1
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(1865, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityId = 2,
                            FistName = "Abraham",
                            ImageURL = "",
                            LastName = "Lincoln",
                            PrivateNumber = "04041099344",
                            Sex = 1
                        });
                });

            modelBuilder.Entity("Domain.Models.PersonAggregate.PhoneNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Number");

                    b.Property<int>("OwnerId");

                    b.Property<int>("PhoneType");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("PhoneNumbers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Number = "555111222",
                            OwnerId = 1,
                            PhoneType = 3
                        },
                        new
                        {
                            Id = 2,
                            Number = "555111223",
                            OwnerId = 1,
                            PhoneType = 2
                        },
                        new
                        {
                            Id = 3,
                            Number = "555111224",
                            OwnerId = 1,
                            PhoneType = 1
                        });
                });

            modelBuilder.Entity("Domain.Models.PersonAggregate.RelatedPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PersonId");

                    b.Property<int>("RelatedPersonId");

                    b.Property<int>("RelationType");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Relations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PersonId = 1,
                            RelatedPersonId = 2,
                            RelationType = 1
                        },
                        new
                        {
                            Id = 2,
                            PersonId = 2,
                            RelatedPersonId = 1,
                            RelationType = 1
                        });
                });

            modelBuilder.Entity("Domain.Models.PersonAggregate.PhoneNumber", b =>
                {
                    b.HasOne("Domain.Models.PersonAggregate.Person", "Owner")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Models.PersonAggregate.RelatedPerson", b =>
                {
                    b.HasOne("Domain.Models.PersonAggregate.Person", "Person")
                        .WithMany("RelatedPeople")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
