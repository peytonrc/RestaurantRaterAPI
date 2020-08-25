using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    // Restaurant Entity (The class that gets stored in the database)
    public class Restaurant
    {
        [Key] // Key is automatically required
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public double Rating { get; set; }

        public bool IsRecommended => Rating > 3.5; // public bool IsRecommended is simplified completely 
    }
}