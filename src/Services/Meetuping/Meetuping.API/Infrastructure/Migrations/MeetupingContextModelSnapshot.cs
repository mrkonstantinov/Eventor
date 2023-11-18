﻿// <auto-generated />
using System;
using Eventor.Services.Meetuping.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Eventor.Services.Meetuping.API.Infrastructure.Migrations
{
    [DbContext(typeof(MeetupingContext))]
    partial class MeetupingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("mateseq", "meetuping")
                .IncrementsBy(10);

            modelBuilder.HasSequence("meetupseq", "meetuping")
                .IncrementsBy(10);

            modelBuilder.Entity("Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate.Meetup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "meetupseq", "meetuping");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("_age")
                        .HasColumnType("int")
                        .HasColumnName("Age");

                    b.Property<int?>("_gender")
                        .HasColumnType("int")
                        .HasColumnName("Gender");

                    b.Property<DateTime>("_meetupDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("MeetupDate");

                    b.Property<int>("_meetupStatusId")
                        .HasColumnType("int")
                        .HasColumnName("MeetupStatusId");

                    b.Property<Guid>("_ownerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("OwnerId");

                    b.Property<string>("_ownerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OwnerName");

                    b.HasKey("Id");

                    b.HasIndex("_meetupStatusId");

                    b.ToTable("meetups", "meetuping");
                });

            modelBuilder.Entity("Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate.MeetupStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("meetupstatus", "meetuping");
                });

            modelBuilder.Entity("Eventor.Services.Meetuping.Infrastructure.Idempotency.ClientRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("requests", "meetuping");
                });

            modelBuilder.Entity("Meetuping.Domain.AggregatesModel.MeetupAggregate.Mate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "mateseq", "meetuping");

                    b.Property<int>("MeetupId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("_age")
                        .HasColumnType("int")
                        .HasColumnName("Age");

                    b.Property<DateTime?>("_approvalAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("ApprovalAt");

                    b.Property<int?>("_gender")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("Gender");

                    b.Property<string>("_mateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MateName");

                    b.Property<string>("_pictureUrl")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PictureUrl");

                    b.HasKey("Id");

                    b.HasIndex("MeetupId");

                    b.ToTable("mates", "meetuping");
                });

            modelBuilder.Entity("Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate.Meetup", b =>
                {
                    b.HasOne("Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate.MeetupStatus", "MeetupStatus")
                        .WithMany()
                        .HasForeignKey("_meetupStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<int>("MeetupId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseHiLo(b1.Property<int>("MeetupId"), "meetupseq", "meetuping");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Region")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("MeetupId");

                            b1.ToTable("meetups", "meetuping");

                            b1.WithOwner()
                                .HasForeignKey("MeetupId");
                        });

                    b.OwnsOne("Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate.PostMetadata", "Metadata", b1 =>
                        {
                            b1.Property<int>("MeetupId")
                                .HasColumnType("int");

                            b1.HasKey("MeetupId");

                            b1.ToTable("meetups", "meetuping");

                            b1.ToJson("Metadata");

                            b1.WithOwner()
                                .HasForeignKey("MeetupId");

                            b1.OwnsMany("Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate.Tag", "Tags", b2 =>
                                {
                                    b2.Property<int>("PostMetadataMeetupId")
                                        .HasColumnType("int");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<string>("Value")
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("PostMetadataMeetupId", "Id");

                                    b2.ToTable("meetups", "meetuping");

                                    b2.WithOwner()
                                        .HasForeignKey("PostMetadataMeetupId");
                                });

                            b1.Navigation("Tags");
                        });

                    b.Navigation("Address");

                    b.Navigation("MeetupStatus");

                    b.Navigation("Metadata");
                });

            modelBuilder.Entity("Meetuping.Domain.AggregatesModel.MeetupAggregate.Mate", b =>
                {
                    b.HasOne("Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate.Meetup", null)
                        .WithMany("Mates")
                        .HasForeignKey("MeetupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate.Meetup", b =>
                {
                    b.Navigation("Mates");
                });
#pragma warning restore 612, 618
        }
    }
}