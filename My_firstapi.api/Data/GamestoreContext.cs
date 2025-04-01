using System;
using Microsoft.EntityFrameworkCore;
using My_firstapi.api.Entities;

namespace My_firstapi.api.Data;

public class GamestoreContext(DbContextOptions < GamestoreContext > options ) 
    : DbContext(options)
{
   public DbSet<Game>  Games => Set<Game>();

   public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new {Id = 1 , Name = "Fighting"} ,
            new {Id = 2 , Name = "Roleplaying"} ,
            new {Id = 3 , Name = "Sports"} , 
            new {Id = 4 , Name = "Racing"} ,
            new {Id = 5 , Name = "Kids and Family"} 
        );
    }
}
