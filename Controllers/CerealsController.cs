using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Databasetest.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Databasetest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CerealsController : Controller
    {
        private readonly BallingdatabaseContext _context;

        public CerealsController(BallingdatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("GetCereals")]
        public IQueryable<Cereal> AllCereals()
        {  
           return _context.Cereals;
        }
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Cannot Find ID")]
        [HttpGet("GetCereal")]
        public ActionResult<Cereal> CerealsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound("please give an Id " + id);
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
        // blablaba
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

        [HttpPost("PostRequestByparameter")]
        public ActionResult<Cereal> CreateCereal([Bind("Name,Mfr,Type,Calories,Protein,Fat,Sodium,Fiber,Carbo,Sugars,Potass,Vitamins,Shelf,Weight,Cups,Rating")] Cereal cereal)
        {
            if (cereal != null)
            {
                _context.Add(cereal);
                _context.SaveChanges(); 
                return cereal;
            }

            return NotFound();
        } 

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

            return NotFound();
        }


        [HttpDelete("Delete")]
        public Cereal? DeleteCereal(int id)
        {
              var cereals = _context.Cereals.Find(id);

            if (cereals == null)
            {
                return null;
            }

            _context.Cereals.Remove(cereals);
            _context.SaveChanges();

            return cereals;
        }
    }
}
