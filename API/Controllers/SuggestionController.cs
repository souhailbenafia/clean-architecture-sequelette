
using Application.DTOs.Suggestion;
using Application.Responses;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {

        private readonly ISuggestionServices _suggestionServices;
        public SuggestionController(ISuggestionServices suggestionServices)
        {
            _suggestionServices = suggestionServices;
        }

        [HttpGet("getsuggestionByUser")]
        public async Task<ActionResult<List<SuggestionDto>>> Get(string id)
        {
            var suggestions = await _suggestionServices.GetUserSuggestions(id);
            return Ok(suggestions);
        }

        // GET: api/<suggestionDetailsController>
        [HttpGet("get")]
        public async Task<ActionResult<SuggestionDto>> GetUserDetails(int id)
        {
            var suggestion = await _suggestionServices.GetSuggestionById(id);
            return Ok(suggestion);
        }

        // Post: api/<CreatesuggestionDetailsController>
        [HttpPost("add")]
        public async Task<ActionResult<BaseServicesResponse>> createCertification(SuggestionDto SuggestionDto)
        {
            var response = await _suggestionServices.CreateSuggestion(SuggestionDto);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(int id)
        {

            var response = await _suggestionServices.DeleteSuggestion(id);
            return Ok(response);
        }

        [HttpPut("edit")]
        public async Task<ActionResult> Put([FromBody] SuggestionDto SuggestionDto)
        {

            var response = await _suggestionServices.UpdateSuggestion(SuggestionDto);
            return Ok(response);
        }
    }
}
