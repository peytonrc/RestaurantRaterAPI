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
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext(); // Field

        //-- Create (POST)
        [HttpPost]
        public async Task<IHttpActionResult> PostRestaurant(Restaurant model)
        {
            if (model == null)
            {
                return BadRequest("Your request body cannot be empty.");
            }

            if (ModelState.IsValid)
            {
                _context.Restaurants.Add(model);
                await _context.SaveChangesAsync();

                return Ok("Good job, you added a restaurant!");
            }

            return BadRequest(ModelState);
        }




        //-- Read (GET)
        // Get by ID
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant != null)
            {
                return Ok(restaurant);
            }
            return NotFound(); // 404 not found

        }

        // Get All
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants); // 200 and returning whatever the list contains
        }




        //-- Update (PUT)
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri]int id, [FromBody]Restaurant updatedRestaurant)
        {
            // Check if our updated restaurant is valid
            if (ModelState.IsValid)
            {
                // Find and update the appropriate restaurant
                Restaurant restaurant = await _context.Restaurants.FindAsync(id);

                if (restaurant != null)
                {
                    // Update the restaurant now that we found it
                    restaurant.Name = updatedRestaurant.Name;
                    restaurant.Rating = updatedRestaurant.Rating;

                    await _context.SaveChangesAsync();

                    return Ok("Restaurant has been updated.");
                }
                // Did not find the restaurant
                return NotFound();
            }
            // Return a bad request
            return BadRequest(ModelState);
        }



        
        //-- Delete (DELETE)
       // [HttpDelete]
       // public async Task<IHttpActionResult> DeleteRestaurant(int id)
        
            





    }
}
