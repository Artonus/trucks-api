using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TrucksApi.Contracts.Requests;
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

    [HttpPost]
    [ProducesResponseType(typeof(TruckResponse), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> DeleteById([FromBody] TruckRequest request)
    {
        try
        {
            if (request is null)
            {
                return BadRequest();
            }
            var result = await _trucksService.Add(request.ToTruck());
            if (result.IsSuccess == false)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Truck!.ToTruckResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong when retrieving trucks: {ex}", ex);
            return BadRequest();
        }
    }
    [HttpPost]
    [Route("{id}/status")]
    [ProducesResponseType(typeof(TruckResponse), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> DeleteById([FromRoute] string id, string status)
    {
        try
        {
            if (status is null)
            {
                return BadRequest("No status was specified");
            }
            var result = await _trucksService.SetStatus(id, status);
            if (result.IsSuccess == false)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Truck!.ToTruckResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong when retrieving trucks: {ex}", ex);
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(TruckResponse), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> Update([FromBody] UpdateTruckRequest request, [FromRoute] string id)
    {
        try
        {
            if (request is null)
            {
                return BadRequest();
            }
            var result = await _trucksService.Update(request.ToTruck(id));
            if (result.IsSuccess == false)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Truck!.ToTruckResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong when retrieving trucks: {ex}", ex);
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(TruckResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        try
        {
            var truck = await _trucksService.GetById(id);
            if (truck is null)
            {
                return NotFound();
            }
            return Ok(truck.ToTruckResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong when retrieving trucks: {ex}", ex);
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteById([FromRoute] string id)
    {
        try
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Truck Id was not specified");
            }
            await _trucksService.Delete(id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong when retrieving trucks: {ex}", ex);
            return BadRequest();
        }
    }
}
