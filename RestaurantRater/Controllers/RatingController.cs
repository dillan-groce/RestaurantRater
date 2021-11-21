using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();
        //create new ratings
        [HttpPost]
        public async Task<IHttpActionResult> CreateRating([FromBody]Rating model)
        {
            
            //check id model is null
            if (model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }
            //check id model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //find the restaurant by the model.restaurantid and see that it exists
            var restaurantEnitity = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurantEnitity is null)
                return BadRequest($"The target restaurant with the ID of {model.RestaurantId} does not exist.");
            //create the rating

            ////add to the rating table
            //_context.Ratings.Add(model);

            //add to the restaurant entity
            restaurantEnitity.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
                return Ok($"You rated restaurant {restaurantEnitity.Name} successfully!");

            return InternalServerError();
        }
        //get a rating by itsid

        //get all ratings

        //get all ratings for a spcific restaurant by the restaurant id

        //update a rating

        //delete a rating
    }
}
