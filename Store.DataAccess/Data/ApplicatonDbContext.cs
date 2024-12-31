using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Data
{
    public class ApplicatonDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicatonDbContext(DbContextOptions<ApplicatonDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { ID = 1, Name = "Action", DisplayOrder = 1 },
                new Category { ID = 2, Name = "Sci-fi", DisplayOrder = 2 },
                new Category { ID = 3, Name = "History", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Ego is the Enemy",
                    Author = "Ryan Holiday",
                    Description = "The Ego is the Enemy draws on a vast array of stories and examples, from literature to philosophy to history. We meet fascinating figures like Howard Hughes, Katharine Graham, Bill Belichick, and Eleanor Roosevelt, all of whom reached the highest levels of power and success by conquering their own egos. Their strategies and tactics can be ours as well. ",
                    ISBN = "SWD9999001",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryID = 1,
                    ImageURL=""
                },
                new Product
                {
                    Id = 2,
                    Title = "The Art of War",
                    Author = "Sun Tzu",
                    Description = "Twenty-Five Hundred years ago, Sun Tzu wrote this classic book of military strategy based on Chinese warfare and military thought. Since that time, all levels of military have used the teaching on Sun Tzu to warfare and civilization have adapted these teachings for use in politics, business and everyday life. ",
                    ISBN = "CAW777777701",
                    ListPrice = 70,
                    Price = 80,
                    Price50 = 75,
                    Price100 = 70,
                    CategoryID = 2,
                    ImageURL = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "The Stranger",
                    Author = "Albert Camus",
                    Description = "Through this story of an ordinary man unwittingly drawn into a senseless murder on a sundrenched Algerian beach, Camus explores what he termed \"the nakedness of man faced with the absurd. ",
                    ISBN = "RITO5555501",
                    ListPrice = 60,
                    Price = 55,
                    Price50 = 45,
                    Price100 = 40,
                    CategoryID = 3,
                    ImageURL = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "Crime and Punishment",
                    Author = "Fyodor Dostoevsky",
                    Description = "Raskolnikov, a destitute and desperate former student, wanders through the slums of St Petersburg and commits a random murder without remorse or regret. He imagines himself to be a great man, a Napoleon: acting for a higher purpose beyond conventional moral law. But as he embarks on a dangerous game of cat and mouse with a suspicious police investigator, Raskolnikov is pursued by the growing voice of his conscience and finds the noose of his own guilt tightening around his neck.",
                    ISBN = "WS3333333301",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55, 
                    CategoryID = 3,
                    ImageURL = ""

                },
                new Product
                {
                    Id = 5,
                    Title = "The Divine Reality: God, Islam and the Mirage of Atheism",
                    Author = "Hamza Andreas",
                    Description = "In The Divine Reality, Hamza Andreas Tzortzis provides a compelling case for the rational and spiritual foundations of Islam, whilst intelligently and compassionately deconstructing atheism. Join him on an existential, spiritual and rational journey that articulates powerful arguments for the existence of God, the Qur'an, the Prophethood of Muhammad and why we must know, love and worship God.",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 40,
                    Price = 35,
                    Price50 = 30,
                    Price100 = 25,
                    CategoryID = 8,
                    ImageURL = ""
                },
                new Product
                {
                    Id = 6,
                    Title = "The 48 Laws of Power",
                    Author = "Robert Greene",
                    Description = "Some laws teach the need for prudence (“Law 1: Never Outshine the Master”), others teach the value of confidence (“Law 28: Enter Action with Boldness”), and many recommend absolute self-preservation (“Law 15: Crush Your Enemy Totally”). Every law, though, has one thing in common: an interest in total domination. In a bold and arresting two-color package, The 48 Laws of Power is ideal whether your aim is conquest, self-defense, or simply to understand the rules of the game.",
                    ISBN = "FOT000000001",
                    ListPrice = 80,
                    Price = 70,
                    Price50 = 65,
                    Price100 = 60,
                    CategoryID = 1,
                    ImageURL = ""
                }
                );
        }
    }
}
