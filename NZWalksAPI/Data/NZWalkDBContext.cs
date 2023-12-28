using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalkDBContext : DbContext
    {
        //Ctrl + R để tạo nhanh contructor
        public NZWalkDBContext(DbContextOptions<NZWalkDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; } 
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        //Seeding data 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Difficuties
            //Easy, Medium, Hard
            //View => Other Window => Csharp Interactive
            var difficuties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("6ed1c932-3a83-4155-b992-2777a9551335"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("10f7ebf0-f4d7-45e8-ad9f-b9144890495b"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("1a96c68c-56e1-479c-ba18-71f92617fcd9"),
                    Name = "Hard"
                },
            };

            //Seed difficuties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficuties);

            //Run seeding data migration
            //Tools => Nuget Package Manager => Package Manager Console
            // => Add-Migration "Name of Migration" => Update-Database (Insert data to the database table)
        }
    }
}
