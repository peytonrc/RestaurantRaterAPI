using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        // Create new ratings
        [HttpPost]
        public async Task<IHttpActionResult> CreateRating(Rating model)
        {
            if (model == null)
            {
                return BadRequest("Your request cannot be empty.");
            }

            // Check to see if the model is NOT valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the targeted restaurant
            var restaurant = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurant == null)
            {
                return BadRequest($"The target restaurant with the ID of {model.RestaurantId} does not exist.");
            }

            // The restaurant isn't null, so we can successfully rate it
            _context.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"You rated {restaurant.Name} successfully!");
            }

            return InternalServerError();
        }



        // Get ALL ratings for a specific restaurant by restaurant ID (GET)
        [HttpGet]
        public async Task<IHttpActionResult> GetAllById(int id)
        {
            Rating rating = await _context.Ratings.FindAsync();
            if (rating != null)
            {
                return Ok(rating.Restaurant);
            }

            return NotFound();

        }

        // Update Rating (PUT)
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRating([FromUri] int id, [FromBody] Rating updatedRating)
        {
            // Check if our updated restaurant is valid
            if (ModelState.IsValid)
            {
                // Find and update the appropriate rating
                Rating rating = await _context.Ratings.FindAsync(id);

                if (rating != null)
                {
                    // Update the rating now that we found it
                    rating.FoodScore = updatedRating.FoodScore;
                    rating.EnvironmentScore = updatedRating.EnvironmentScore;
                    rating.CleanlinessScore = updatedRating.CleanlinessScore;

                    await _context.SaveChangesAsync();

                    return Ok("Rating has been updated.");
                }
                // Did not find the restaurant
                return NotFound();
            }
            // Return a bad request
            return BadRequest(ModelState);
        }


        //Delete Rating (DELETE)
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRatingById(int id)
        {
            Rating entity = await _context.Ratings.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }
            _context.Ratings.Remove(entity);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("The rating was deleted.");
            }
            return InternalServerError();
        }
    }
}
