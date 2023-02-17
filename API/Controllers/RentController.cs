
using Application.DTOs.Rent;
using Application.Responses;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly IRentServices _rentServices;
        public RentController(IRentServices rentServices)
        {
            _rentServices = rentServices;
        }

        [HttpGet("getrentByUser")]
        public async Task<ActionResult<List<RentDto>>> Get(int id)
        {
            var rents = await _rentServices.GetRentById(id);
            return Ok(rents);
        }

        // GET: api/<rentDetailsController>
        [HttpGet("get")]
        public async Task<ActionResult<RentDto>> GetUserDetails(int id)
        {
            var rent = await _rentServices.GetRentById(id);
            return Ok(rent);
        }

        // Post: api/<CreaterentDetailsController>
        [HttpPost("add")]
        public async Task<ActionResult<BaseServicesResponse>> createCertification(RentDto rentDto)
        {
            var response = await _rentServices.CreateRent(rentDto);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(int id)
        {

            var response = await _rentServices.DeleteRent(id);
            return Ok(response);
        }

        [HttpPut("edit")]
        public async Task<ActionResult> Put([FromBody] RentDto rentDto)
        {

            var response = await _rentServices.UpdateRent(rentDto);
            return Ok(response);
        }
    }
}
