﻿// <auto-generated />
using BookingBotAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookingBotAPI.Migrations
{
    [DbContext(typeof(BookingBotDbContext))]
    [Migration("20250209193925_AddTelegramIdToMaster")]
    partial class AddTelegramIdToMaster
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("ClientMessenger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<int>("Messenger")
                        .HasColumnType("integer");

                    b.Property<string>("MessengerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("ClientMessenger");
                });

            modelBuilder.Entity("Master", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Masters");
                });

            modelBuilder.Entity("Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DurationMinutes")
                        .HasColumnType("integer");

                    b.Property<int>("MasterId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MasterId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("ClientMessenger", b =>
                {
                    b.HasOne("Client", "Client")
                        .WithMany("Messengers")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Service", b =>
                {
                    b.HasOne("Master", "Master")
                        .WithMany("Services")
                        .HasForeignKey("MasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Master");
                });

            modelBuilder.Entity("Client", b =>
                {
                    b.Navigation("Messengers");
                });

            modelBuilder.Entity("Master", b =>
                {
                    b.Navigation("Services");
                });
#pragma warning restore 612, 618
        }
    }
}
