using Application.DTOs.Car;
using Application.Responses;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        private readonly ICarServices _carServices;
        public CarController(ICarServices carServices)
        {
            _carServices = carServices;
        }

        [HttpGet("getCarByUser/{id}")]
        public async Task<ActionResult<List<CarDto>>> Get(string id)
        {
            var cars = await _carServices.GetUserCars(id);
            return Ok(cars);
        }
        // GET: api/<CarDetailsController>
        [HttpGet("get/{id}")]
        public async Task<ActionResult<CarDto>> GetUserDetails(int id)
        {
            var car = await _carServices.GetCarById(id);
            return Ok(car);
        }

        // Post: api/<CreateCarDetailsController>
        [HttpPost("add")]
        public async Task<ActionResult<BaseServicesResponse>> createCertification(CarDto carDto)
        {
            var response = await _carServices.CreateCar(carDto);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(int id)
        {

            var response = await _carServices.DeleteCar(id);
            return Ok(response);
        }

        [HttpPut("edit")]
        public async Task<ActionResult> Put([FromBody] CarDto carDto)
        {

            var response = await _carServices.UpdateCar(carDto);
            return Ok(response);
        }





    }
}
