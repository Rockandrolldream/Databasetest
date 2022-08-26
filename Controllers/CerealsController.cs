using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Databasetest.Models;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace Databasetest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CerealsController : Controller
    {
        private readonly BallingdatabaseContext _context;
        private readonly IJWTManagerRepository _jWTManager; 
        

        public CerealsController(BallingdatabaseContext context, IJWTManagerRepository jWTManager)
        {
            _context = context;
            _jWTManager = jWTManager; 
        }


        /// <summary>
        /// Gets the list of all Cereals.
        /// </summary>
        /// <returns>The list of Cereals.</returns>
        // GET: GetCereals
        [HttpGet("GetCereals")]
        public IEnumerable<Cereal> AllCereals()
        {
            return _context.Cereals;
        }

        /// <summary>
        /// GetCereal by id.
        /// </summary>
        /// <returns>Search after specific Cereal</returns> 
        /// <response code="200">Returns Cereal</response>
        /// <response code="404">If the item is not found</response> 
        // GET: GetCereal
        [HttpGet("GetCereal")]
        public ActionResult<Cereal> CerealsDetails(int? id)
        {
            Cereal output = new Cereal();
            NotFoundObjectResult notFoundObjectResult = new NotFoundObjectResult(output);
            output = _context.Cereals.Where(m => m.Id == id).FirstOrDefault();
            if (output == null)
            {
                return notFoundObjectResult;
            }

            return Ok(output);
        }

        /// <summary>
        /// GetCereal by Parameters.
        /// </summary>
        /// <returns>returns number of cereals , taht was search after</returns> 
        // GET: GetCerealtypeByparameter  
        [HttpGet("GetCerealtypeByparameter")]
        public IQueryable<Cereal>? GetCerealByparameter(string? name, String? Mfr, string? notmfr ,String? type, string? nottype, int? calories,int? caloriegreatthan, int? caloriesmallerthan ,int? protein, int? proteingreatethan, int? proteinlessthan, int? fat , int? fatgreaterthan, int? fatlesserthan, int? sodium, int? sodiumgreaterthan, int? sodiumlessthan, int? fiber, int? fibergreaterthan, int? fiberlessthan, int? carbo, int? carbogreathan, int? carbolessthan , int? sugar, int? sugargreaterthan, int? sugarlessthan, int? potass, int? potassgreatherthan, int? potaslessthan, int? vitamin, int? vitamingreatthan, int? vitaminlessthan, int? shelf , int? shelfgreathan, int? shelflessthan, int? weight, int? weightgreatthan, int? weightlessthan, int? cups, int? cupsgreathan, int? cupslessthan, int? rating, int? ratinggreathan, int? ratinglessthan)
        {

            var cereal = _context.Cereals.Where(c => (c.Name == name|| name == null)
                                                    && (c.Mfr == Mfr || Mfr == null)
                                                    && (c.Mfr != notmfr || notmfr == null)
                                                    && (c.Type == type || type == null)
                                                    && (c.Type != nottype || nottype == null)
                                                    && (c.Calories == calories || calories == null)
                                                    && (c.Calories > caloriegreatthan || caloriegreatthan == null)
                                                    && (c.Calories < caloriesmallerthan || caloriesmallerthan == null)
                                                    && (c.Protein == protein || protein == null)
                                                    && (c.Protein > proteingreatethan || proteingreatethan == null)
                                                    && (c.Protein < proteinlessthan || proteinlessthan == null)
                                                    && (c.Fat == fat || fat == null)
                                                    && (c.Fat > fatgreaterthan || fatgreaterthan == null)
                                                    && (c.Fat < fatlesserthan || fatlesserthan == null)
                                                    && (c.Sodium == sodium || sodium == null)
                                                    && (c.Sodium > sodiumgreaterthan || sodiumgreaterthan == null)
                                                    && (c.Sodium < sodiumlessthan || sodiumlessthan == null)
                                                    && (c.Fiber == fiber || fiber == null)
                                                    && (c.Fiber > fibergreaterthan || fibergreaterthan == null)
                                                    && (c.Fiber < fiberlessthan || fiberlessthan == null)
                                                    && (c.Carbo == carbo || carbo == null)
                                                    && (c.Carbo > carbogreathan || carbogreathan == null)
                                                    && (c.Carbo < carbolessthan || carbolessthan == null)
                                                    && (c.Sugars == sugar || sugar == null)
                                                    && (c.Sugars > sugargreaterthan || sugargreaterthan == null)
                                                    && (c.Sugars < sugarlessthan || sugarlessthan == null)
                                                    && (c.Potass == potass || potass == null)
                                                    && (c.Potass > potassgreatherthan || potassgreatherthan == null)
                                                    && (c.Potass < potaslessthan || potaslessthan == null)
                                                    && (c.Vitamins == vitamin || vitamin == null)
                                                    && (c.Vitamins > vitamingreatthan || vitamingreatthan == null)
                                                    && (c.Vitamins < vitaminlessthan || vitaminlessthan == null)
                                                    && (c.Shelf == shelf || shelf == null)
                                                    && (c.Shelf > shelfgreathan || shelfgreathan == null)
                                                    && (c.Shelf < shelflessthan || shelflessthan == null)
                                                    && (c.Weight == weight || weight == null)
                                                    && (c.Weight > weightgreatthan || weightgreatthan == null)
                                                    && (c.Weight < weightlessthan || weightlessthan == null)
                                                    && (c.Cups == cups || cups == null)
                                                    && (c.Cups > cupsgreathan || cupsgreathan == null)
                                                    && (c.Cups < cupslessthan || cupslessthan == null)
                                                    && (c.Rating == rating || rating == null)
                                                    && (c.Rating > ratinggreathan || ratinggreathan == null)
                                                    && (c.Rating < ratinglessthan || ratinglessthan == null)

            );
            return cereal;
        }

        /// <summary>
        /// Add Cereal to database.
        /// </summary>
        /// <returns>returns object if ok, else return error</returns> 
        /// <response code="200">Returns Cereals, that has been posted</response>
        /// <response code="404">If the input is empthy</response> 
        /// <response code="401">If that has not been created a JWT token</response>
        // POST: PostRequestByparameter
        [Authorize]
        [HttpPost("PostRequestByparameter")]
        public ActionResult<Cereal> CreateCereal(Cereal cereal)
        {
            _context.Add(cereal);
            _context.SaveChanges();
            return cereal;

        }

        /// <summary>
        /// Update Cereal in database.
        /// </summary>
        /// <returns>returns object if ok, else invalidt id</returns> 
        /// <response code="200">Returns Cereals, that has been Updated</response>
        /// <response code="404">If the input is empthy</response> 
        /// <response code="401">If that has not been created a JWT token</response> 
        // POST: UpdateRequestByparameter
        [Authorize]
        [HttpPost("UpdateRequestByparameter")] 
         public ActionResult<Cereal> UpdateCereal(Cereal cereal)
        {
            var cereals = _context.Cereals.Where(m => m.Id == cereal.Id).FirstOrDefault();
            if (cereals == null)
            {
                return NotFound("Id not found");
            }

            _context.Update(cereal);
            _context.SaveChanges();
            return cereal;
        }

        /// <summary>
        /// Delete Cereal in database.
        /// </summary>
        /// <returns>returns object if Delete is completed, else return error</returns> 
        /// <response code="200">Returns Cereals, that has been Delete</response>
        /// <response code="404">If the input is empthy</response> 
        /// <response code="401">If that has not been created a JWT token</response> 
        // DELETE: Delete
        [Authorize]
        [HttpDelete("Delete")]
        public ActionResult<Cereal> DeleteCereal(int id)
        {
            var cereals = _context.Cereals.Where(m => m.Id == id).FirstOrDefault();
            if (cereals == null)
            {
                return NotFound("Id not found");
            }
            _context.Cereals.Remove(cereals);
            _context.SaveChanges();
            return Ok(cereals);
        }

        /// <summary>
        /// CreateToken in database.
        /// </summary>
        /// <returns>Create token, if user and password are validt, else return error</returns>
        // DELETE: POST
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(User usersdata)
        {
            var token = _jWTManager.Authenticate(usersdata);

            if (token == null)
            {
                return Unauthorized("Please give validt, name and password");
            }
            return Ok(token);
        }
    }
}
