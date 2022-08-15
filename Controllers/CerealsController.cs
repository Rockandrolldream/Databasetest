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
            if (id == null)
            {
                return NotFound("Please give an Id " + id);
            }

            IQueryable<Cereal> output; 
            List<Cereal> cereals = new List<Cereal>(); 

            output = _context.Cereals.Where(m => m.Id == id);
            if (output == null)
            {
                return NotFound("ID was not Found" + id);
            }

            return Ok(output);
        }

        /// <summary>
        /// GetCereal by Parameters.
        /// </summary>
        /// <returns>returns number of cereals , taht was search after</returns> 
        // GET: GetCerealtypeByparameter  
        [HttpGet("GetCerealtypeByparameter")]
        public IQueryable<Cereal>? GetCerealByparameter(string? name, String? Mfr, String? type, String? calories, String? protein, string? fat, string? sodium, string? fiber, string? carbo, string? sugar, string? potass, string? vitamin, string? shelf, string? weight, string? cups, string? rating)
        {   
            List<Cereal> cereals = new List<Cereal>();

            var cereal = _context.Cereals.Where(c => (c.Name == name || name == null)
                                                    && (c.Mfr == Mfr || Mfr == null) 
                                                    &&  (c.Type == type || type == null)
                                                    && (c.Calories == calories || calories == null) 
                                                    && (c.Protein == protein || protein == null)
                                                    && (c.Fat == fat || fat == null)
                                                    && (c.Sodium == sodium || sodium == null)
                                                    && (c.Fiber == fiber || fiber == null)
                                                    && (c.Carbo == carbo || carbo == null)
                                                    && (c.Sugars == sugar || sugar == null)
                                                    && (c.Potass == potass || potass == null)
                                                    && (c.Vitamins == vitamin || vitamin == null)
                                                    && (c.Shelf == shelf || shelf == null)
                                                    && (c.Weight == weight || weight == null)
                                                    && (c.Cups == cups|| cups == null)
                                                    && (c.Rating == rating || rating == null)

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
        public ActionResult<Cereal> CreateCereal([Bind("Name,Mfr,Type,Calories,Protein,Fat,Sodium,Fiber,Carbo,Sugars,Potass,Vitamins,Shelf,Weight,Cups,Rating")] Cereal cereal)
        {
            if (cereal != null)
            {
                _context.Add(cereal);
                _context.SaveChanges(); 
                return cereal;
            }

            return NotFound("Please give input");
        }

        /// <summary>
        /// Update Cereal in database.
        /// </summary>
        /// <returns>returns object if ok, else return error</returns> 
        /// <response code="200">Returns Cereals, that has been Updated</response>
        /// <response code="404">If the input is empthy</response> 
        /// <response code="401">If that has not been created a JWT token</response> 
        // POST: UpdateRequestByparameter
        [Authorize]
        [HttpPost("UpdateRequestByparameter")] 
         public ActionResult<Cereal> UpdateCereal([Bind("Id,Name,Mfr,Type,Calories,Protein,Fat,Sodium,Fiber,Carbo,Sugars,Potass,Vitamins,Shelf,Weight,Cups,Rating")] Cereal cereal)
        {  
           var output = (_context.Cereals?.Where(e => e.Id == cereal.Id));

            if ( output != null)
            {
                _context.Update(cereal);
                _context.SaveChanges();
                return cereal;
            }

            return NotFound("Please give input, before updateing");
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
              var cereals = _context.Cereals.Find(id);

            if (cereals == null)
            {
                return NotFound(" The Id is not exsisitng");
            }

            _context.Cereals.Remove(cereals);
            _context.SaveChanges();

            return cereals;
        }

        /// <summary>
        /// CreateToken in database.
        /// </summary>
        /// <returns>returns object if Delete is completed, else return error</returns>
        // DELETE: Delete
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
