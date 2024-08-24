using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    // [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        public readonly ILogging _logger;
        private readonly AppDbContext _appDbContext;
        public VillaAPIController(ILogging logging, AppDbContext dbContext)
        {
            _logger = logging;
            _appDbContext = dbContext;
        }
        [HttpGet]
        //public IEnumerable<VillaDTO> GetVillas()
        //{
        //    //return new List<Villa>
        //    //{
        //    //    new Villa { Id = 1, Name = "Pool View" },
        //    //    new Villa { Id = 2, Name = "Hill View" }
        //    //};
        //    return VillaStore.villaList;
        //}

        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            //return new List<Villa>
            //{
            //    new Villa { Id = 1, Name = "Pool View" },
            //    new Villa { Id = 2, Name = "Hill View" }
            //};

            _logger.Log("Getting All Villas List.", "");
            var villaList = await _appDbContext.Villas.ToListAsync();
            return Ok(villaList);
        }

        //[HttpGet("id")]
        [HttpGet("{id:int}",Name = "GetVilla")]

        // defining the response stauts type
        [ProducesResponseType(200)]

        // define the return type of success by another way also
        //[ProducesResponseType(200, Type=typeof(VillaDTO))]

        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        // defining the response stauts type with more clear way
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if(id <= 0)
            {
                _logger.Log("Invalid Id type", "error");
                return BadRequest();
            }
            var villa = _appDbContext.Villas.FirstOrDefault(x => x.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO) { 

            // custom validation 
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var IsVillaExist = _appDbContext.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower());
            if(IsVillaExist != null)
            {
                ModelState.AddModelError("CustomError", "Villa already Exits with name!");
                return BadRequest(ModelState);
            }


            if(villaDTO == null) return BadRequest(villaDTO);

            Villa model = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Sqft = villaDTO.Sqft,
                CreatedAt = DateTime.Now,
            };

            _appDbContext.Villas.Add(model);
            _appDbContext.SaveChanges();


            //return Ok(villaDTO);
            return CreatedAtRoute("GetVilla", new { Id =  villaDTO.Id }, villaDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        // ActionResult shows that is should have the reutn type 
        // IActionResult impl. doesnt need to reutrn while deleting
        public IActionResult DeleteVilla(int id)
        {
            if (id <= 0) return BadRequest();

            var villa = _appDbContext.Villas.FirstOrDefault(x => x.Id == id);
            if (villa == null) return NotFound();

            _appDbContext.Villas.Remove(villa);
            _appDbContext.SaveChanges();
            return NoContent();
        }


        [HttpPut("{id:int}",Name = "UpdateVila")]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            if(villaDTO.Id != id || villaDTO == null) return BadRequest();

            var villa = _appDbContext.Villas.FirstOrDefault(x => x.Id == id);
            if (villa == null) return NotFound(villaDTO);

            villa.Name = villaDTO.Name;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;
            villa.Details = villaDTO.Details;
            villa.Rate = villaDTO.Rate;
            villa.Amenity = villaDTO.Amenity;
            villa.ImageUrl = villaDTO.ImageUrl;

            _appDbContext.Update(villa);
            _appDbContext.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchVillaDTO)
        {
            if (id == 0 || patchVillaDTO == null) return BadRequest();

            //var villa = _appDbContext.Villas.FirstOrDefault(x => x.Id == id);  
            // this will cause erro beacuse ETF is tracking this entity with same id 
            // and we are trying the update the model in down where EF itself is tracking the entity with same id there too|
            // so EF will be confused of the tracking entity 
            // to resolve this issuse we need to fix the above query like this
            var villa = _appDbContext.Villas.AsNoTracking().FirstOrDefault(x => x.Id == id);  

            if (villa == null) return NotFound(patchVillaDTO);


            VillaDTO villaDTO = new VillaDTO()
            {
                Id = villa.Id,
                Name = villa.Name,
                Sqft = villa.Sqft,
                Occupancy = villa.Occupancy,
                Details = villa.Details,
                Rate = villa.Rate,
                Amenity = villa.Amenity,
                ImageUrl = villa.ImageUrl,
            };


            // json patch document wants the applyTO()
            // and restore in the model state
            patchVillaDTO.ApplyTo(villaDTO, ModelState);

            Villa model = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Sqft = villaDTO.Sqft,
                CreatedAt = DateTime.Now,
            };
            _appDbContext.Update(model);
            _appDbContext.SaveChanges();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NoContent();
        }
    }


    
}
