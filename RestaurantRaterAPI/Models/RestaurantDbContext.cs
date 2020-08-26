using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class RestaurantDbContext : DbContext // Must use ctrl .
    {
        public RestaurantDbContext() : base("DefaultConnection") { } // Constructor calling base contsructor 

        public DbSet<Restaurant> Restaurants { get; set; } // Created restaurant property that is the whole set; stores info

        public DbSet<Rating> Ratings { get; set; }
    }
}