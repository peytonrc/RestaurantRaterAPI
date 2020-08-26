﻿using System;
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
        // Primary Key
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Rating
        {
            get
            {
                //return FoodRating + EnvironmentRating + CleanlinessRating / 3;

                // Calculate a total average score based on Ratings
                double totalAverageRating = 0;

                // Add all Ratings together to get total Average Rating
                foreach (var rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }

                // Return Average of Total if the count is above 0
                return (Ratings.Count > 0) ? Math.Round(totalAverageRating / Ratings.Count, 2) : 0;
            }
        }

        // Average Food Rating
        public double FoodRating
        {
            get
            {
                double totalFoodScore = 0;

                foreach (var rating in Ratings)
                    totalFoodScore += rating.FoodScore;

                return (Ratings.Count > 0) ? Math.Round(totalFoodScore / Ratings.Count, 2) : 0;
            }
        }

        // Average Environment Rating
        public double EnvironmentRating
        {
            get
            {
                IEnumerable<double> scores = Ratings.Select(rating => rating.EnvironmentScore);

                double totalEnvironmentScore = scores.Sum();

                return (Ratings.Count > 0) ? Math.Round(totalEnvironmentScore / Ratings.Count, 2) : 0;
            }
        }

        // Average Cleanliness Rating
        public double CleanlinessRating
        {
            get
            {
                var totalScore = Ratings.Select(r => r.CleanlinessScore).Sum();
                return (Ratings.Count > 0) ? Math.Round(totalScore / Ratings.Count, 2) : 0;
                // return (Ratings.Count > 0) ? Ratings.Select(r => r.CleanlinessScore).Sum() / Ratings.Count : 0;
            }
        }

        // public bool IsRecommended => Rating > 8;
        public bool IsRecommended
        {
            get
            {
                return Rating > 8;
            }
        }

        // All of the associated Rating objects from the database
        // based on the Foreign Key relationship
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
    }
}