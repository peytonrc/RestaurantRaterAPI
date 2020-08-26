using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Rating
    {
        // Primary Key
        [Key]
        public int Id { get; set; }


        // Foreign Key (Restaurant Key) tells us which spot to go to 
        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }
        // Foreign Key Navigation Property
        public virtual Restaurant Restaurant { get; set; } //Virtual means this will let us tell the db that these two are connected. (connected to db)


        [Required]
        [Range(0, 10)]
        public double FoodScore { get; set; }

        [Required]
        [Range(0, 10)]
        public double EnvironmentScore { get; set; }

        [Required]
        [Range(0, 10)]
        public double CleanlinessScore { get; set; }

        public double AverageRating
        {
            get
            {
                double totalScore = FoodScore + EnvironmentScore + CleanlinessScore;
                return totalScore / 3;
            }
        }
         
            
  

    }
}