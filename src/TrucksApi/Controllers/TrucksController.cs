using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TrucksApi.Contracts.Responses;
using TrucksApi.Mappings;
using TrucksApi.Services.Abstract;

namespace TrucksApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TrucksController : Controller
{
    private readonly ITrucksService _trucksService;
    private readonly ILogger<TrucksController> _logger;

    public TrucksController(ITrucksService trucksService, ILogger<TrucksController> logger)
    {
        _trucksService = trucksService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetTrucksResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var trucks = await _trucksService.GetAll();
            return Ok(trucks.ToTrucksResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong when retrieving trucks: {ex}", ex);            
            return BadRequest();
        }
    }
}
