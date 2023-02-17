using Application.DTOs.Car;
using Application.DTOs.Offre;
using Application.Responses;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using persistence.Services;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class OffreController : ControllerBase
    {
        private readonly IOffreServices _offreServices;

        public OffreController(IOffreServices offreServices)
        {
            _offreServices = offreServices;
        }


        // GET: api/<UserDetailsController>
        [HttpGet("get")]
        public async Task<ActionResult<OffreDto>> GetOffreDetails(int id)
        {
            var offre = await _offreServices.GetOffreById(id);
            return Ok(offre);
        }

        [HttpGet("get-by-carId/{id}")]
        public async Task<ActionResult<OffreDto>> GetByCarId(int id)
        {
            var offres = await _offreServices.getOffresByCarId(id);
            return Ok(offres);
        }

        [HttpGet("get-by-userId/{id}")]
        public async Task<ActionResult<OffreDto>> GetByUserId(string id)
        {
            var offres = await _offreServices.GetOffresByUserId(id);
            return Ok(offres);
        }

        // Post: api/<CreateUserDetailsController>
        [HttpPost("add")]
        public async Task<ActionResult<BaseServicesResponse>> createOffre(CreateOffre createOffre)
        {
            var response = await _offreServices.CreateOffre(createOffre.offre, createOffre.car);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(int id)
        {

            var response = await _offreServices.DeleteOffre(id);
            return Ok(response);
        }

        [HttpPut("edit")]
        public async Task<ActionResult> Put([FromBody] OffreDto offreDto)
        {

            var response = await _offreServices.UpdateOffre(offreDto);
            return Ok(response);
        }

        [HttpPut("edit-availablite")]
        public async Task<ActionResult> ChangeOffreAvailablite([FromBody] int id)
        {
            try {
                var response = await _offreServices.ChangeOffreAvailablite(id);
                return Ok(response);
            } catch
            {
                return BadRequest("Failed to Update ");
            }           
        }



        [HttpGet("get-all-offres-Available")]
        public async Task<ActionResult<List<CarDto>>> GetAllOffre()
        {
            var offres = await _offreServices.GetAllOffresAvailable();
            return Ok(offres);
        }

        [HttpPut("edit-offre")]
        public async Task<ActionResult> UpdateOffreAndCar([FromBody] CreateOffre model)
        {
            var response = await _offreServices.UpdateOffreAndCar(model.offre,model.car);
            return Ok(response);
        }

    }


    public  class CreateOffre
    {
        public CarDto car { get; set; }
        public OffreDto offre { get; set; }
    }
}
