﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TournamentManagerAPI;

#nullable disable

namespace TournamentManagerAPI.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20220518140408_renamed_property_of_match")]
    partial class renamed_property_of_match
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("TournamentManagerAPI.Data.Entities.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<int?>("PlayerRequiringResultId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Score")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Start")
                        .HasColumnType("TEXT");

                    b.Property<int>("TournamentId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("WinnerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlayerRequiringResultId")
                        .IsUnique();

                    b.HasIndex("TournamentId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("TournamentManagerAPI.Data.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<int>("TournamentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("TournamentManagerAPI.Data.Entities.PlayerOrMatchResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEmpty")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPlayer")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MatchId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OriginalMatchId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OriginalMatchId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerOrMatchResults");
                });

            modelBuilder.Entity("TournamentManagerAPI.Data.Entities.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("TournamentManagerAPI.Data.Entities.Match", b =>
                {
                    b.HasOne("TournamentManagerAPI.Data.Entities.PlayerOrMatchResult", "PlayerRequiringResult")
                        .WithOne("Match")
                        .HasForeignKey("TournamentManagerAPI.Data.Entities.Match", "PlayerRequiringResultId");

                    b.HasOne("TournamentManagerAPI.Data.Entities.Tournament", null)
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TournamentManagerAPI.Data.Entities.Player", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId");

                    b.Navigation("PlayerRequiringResult");

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("TournamentManagerAPI.Data.Entities.Player", b =>
                {
                    b.HasOne("TournamentManagerAPI.Data.Entities.Tournament", "Tournament")
                        .WithMany("Players")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("TournamentManagerAPI.Data.Entities.PlayerOrMatchResult", b =>
                {
                    b.HasOne("TournamentManagerAPI.Data.Entities.Match", "OriginalMatch")
                        .WithMany("Players")
                        .HasForeignKey("OriginalMatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TournamentManagerAPI.Data.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.Navigation("OriginalMatch");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("TournamentManagerAPI.Data.Entities.Match", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("TournamentManagerAPI.Data.Entities.PlayerOrMatchResult", b =>
                {
                    b.Navigation("Match");
                });

            modelBuilder.Entity("TournamentManagerAPI.Data.Entities.Tournament", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
