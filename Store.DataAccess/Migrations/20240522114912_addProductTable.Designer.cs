﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.DataAccess.Data;

#nullable disable

namespace Store.DataAccess.Migrations
{
    [DbContext(typeof(ApplicatonDbContext))]
    [Migration("20240522114912_addProductTable")]
    partial class addProductTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Store.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("ID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            DisplayOrder = 1,
                            Name = "Action"
                        },
                        new
                        {
                            ID = 2,
                            DisplayOrder = 2,
                            Name = "Sci-fi"
                        },
                        new
                        {
                            ID = 3,
                            DisplayOrder = 3,
                            Name = "History"
                        });
                });

            modelBuilder.Entity("Store.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price100")
                        .HasColumnType("float");

                    b.Property<double>("Price50")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Ryan Holiday",
                            Description = "The Ego is the Enemy draws on a vast array of stories and examples, from literature to philosophy to history. We meet fascinating figures like Howard Hughes, Katharine Graham, Bill Belichick, and Eleanor Roosevelt, all of whom reached the highest levels of power and success by conquering their own egos. Their strategies and tactics can be ours as well. ",
                            ISBN = "SWD9999001",
                            ListPrice = 99.0,
                            Price = 90.0,
                            Price100 = 80.0,
                            Price50 = 85.0,
                            Title = "Ego is the Enemy"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Sun Tzu",
                            Description = "Twenty-Five Hundred years ago, Sun Tzu wrote this classic book of military strategy based on Chinese warfare and military thought. Since that time, all levels of military have used the teaching on Sun Tzu to warfare and civilization have adapted these teachings for use in politics, business and everyday life. ",
                            ISBN = "CAW777777701",
                            ListPrice = 70.0,
                            Price = 80.0,
                            Price100 = 70.0,
                            Price50 = 75.0,
                            Title = "The Art of War"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Albert Camus",
                            Description = "Through this story of an ordinary man unwittingly drawn into a senseless murder on a sundrenched Algerian beach, Camus explores what he termed \"the nakedness of man faced with the absurd. ",
                            ISBN = "RITO5555501",
                            ListPrice = 60.0,
                            Price = 55.0,
                            Price100 = 40.0,
                            Price50 = 45.0,
                            Title = "The Stranger"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Fyodor Dostoevsky",
                            Description = "Raskolnikov, a destitute and desperate former student, wanders through the slums of St Petersburg and commits a random murder without remorse or regret. He imagines himself to be a great man, a Napoleon: acting for a higher purpose beyond conventional moral law. But as he embarks on a dangerous game of cat and mouse with a suspicious police investigator, Raskolnikov is pursued by the growing voice of his conscience and finds the noose of his own guilt tightening around his neck.",
                            ISBN = "WS3333333301",
                            ListPrice = 70.0,
                            Price = 65.0,
                            Price100 = 55.0,
                            Price50 = 60.0,
                            Title = "Crime and Punishment"
                        },
                        new
                        {
                            Id = 5,
                            Author = "Hamza Andreas",
                            Description = "In The Divine Reality, Hamza Andreas Tzortzis provides a compelling case for the rational and spiritual foundations of Islam, whilst intelligently and compassionately deconstructing atheism. Join him on an existential, spiritual and rational journey that articulates powerful arguments for the existence of God, the Qur'an, the Prophethood of Muhammad and why we must know, love and worship God.",
                            ISBN = "SOTJ1111111101",
                            ListPrice = 40.0,
                            Price = 35.0,
                            Price100 = 25.0,
                            Price50 = 30.0,
                            Title = "The Divine Reality: God, Islam and the Mirage of Atheism"
                        },
                        new
                        {
                            Id = 6,
                            Author = "Robert Greene",
                            Description = "Some laws teach the need for prudence (“Law 1: Never Outshine the Master”), others teach the value of confidence (“Law 28: Enter Action with Boldness”), and many recommend absolute self-preservation (“Law 15: Crush Your Enemy Totally”). Every law, though, has one thing in common: an interest in total domination. In a bold and arresting two-color package, The 48 Laws of Power is ideal whether your aim is conquest, self-defense, or simply to understand the rules of the game.",
                            ISBN = "FOT000000001",
                            ListPrice = 80.0,
                            Price = 70.0,
                            Price100 = 60.0,
                            Price50 = 65.0,
                            Title = "The 48 Laws of Power"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}