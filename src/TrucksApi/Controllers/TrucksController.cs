using System.Net;
using Microsoft.AspNetCore.Mvc;
using TrucksApi.Contracts.Requests;
using TrucksApi.Contracts.Responses;
using TrucksApi.Mappings;
using TrucksApi.Services.Abstract;

namespace TrucksApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TrucksController : Controller
{
    private const string Truck = "Truck";
    private readonly ITrucksService _trucksService;
    private readonly ILogger<TrucksController> _logger;

    public TrucksController(ITrucksService trucksService, ILogger<TrucksController> logger)
    {
        _trucksService = trucksService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetTrucksResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetTrucksFilter filter, [FromQuery] SortingParam sort)
    {
        try
        {
            var trucks = await _trucksService.GetFiltered(filter.ToTruckFilter(), sort.ToSorting());
            if (trucks.Count == 0)
            {
                return NotFound(GetError(Truck, "No trucks were found", HttpStatusCode.NotFound));
            }
            return Ok(trucks.ToTrucksResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong when retrieving trucks: {traceId} {ex}", HttpContext.TraceIdentifier, ex);
            return BadRequest(GetError("System", ex.Message, HttpStatusCode.BadRequest));
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(TruckResponse), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> DeleteById([FromBody] TruckRequest request)
    {
        try
        {
            var result = await _trucksService.Add(request.ToTruck());
            if (result.IsSuccess == false)
            {
                return BadRequest(GetError(Truck, result.ErrorMessage!, HttpStatusCode.BadRequest));
            }
            return Ok(result.Truck!.ToTruckResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong when adding truck: {traceId} {ex}", HttpContext.TraceIdentifier, ex);
            return BadRequest(GetError("System", ex.Message, HttpStatusCode.BadRequest));
        }
    }
    [HttpPost]
    [Route("{id}/status")]
    [ProducesResponseType(typeof(TruckResponse), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> DeleteById([FromRoute] string id, string status)
    {
        try
        {
            if (string.IsNullOrEmpty(status))
            {
                return BadRequest(GetError("Status", "No status was specified", HttpStatusCode.BadRequest));
            }
            var result = await _trucksService.SetStatus(id, status);
            if (result.IsSuccess == false)
            {
                return BadRequest(GetError(Truck, result.ErrorMessage!, HttpStatusCode.BadRequest));
            }
            return Ok(result.Truck!.ToTruckResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong when updating truck status: {traceId} {ex}", HttpContext.TraceIdentifier, ex);
            return BadRequest(GetError("System", ex.Message, HttpStatusCode.BadRequest));
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
                return BadRequest(GetError("Status", "No status was specified", HttpStatusCode.BadRequest));
            }
            var result = await _trucksService.Update(request.ToTruck(id));
            if (result.IsSuccess == false)
            {
                return BadRequest(GetError(Truck, result.ErrorMessage!, HttpStatusCode.BadRequest));
            }
            return Ok(result.Truck!.ToTruckResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong when updating truck: {traceId} {ex}", HttpContext.TraceIdentifier, ex);
            return BadRequest(GetError("System", ex.Message, HttpStatusCode.BadRequest));
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
                return NotFound(GetError(Truck, "Truck not found", HttpStatusCode.NotFound));
            }
            return Ok(truck.ToTruckResponse());
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong when retrieving truck: {traceId} {ex}", HttpContext.TraceIdentifier, ex);
            return BadRequest(GetError("System", ex.Message, HttpStatusCode.BadRequest));
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
                return BadRequest(GetError("Id", "Truck Id was not specified", HttpStatusCode.BadRequest));
            }
            await _trucksService.Delete(id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong when deleting truck: {traceId} {ex}", HttpContext.TraceIdentifier, ex);
            return BadRequest(GetError("System", ex.Message, HttpStatusCode.BadRequest));
        }
    }
    private ValidationProblemDetails GetError(string property, string message, HttpStatusCode statusCode)
    {
        var error = new ValidationProblemDetails
        {
            Status = (int)statusCode,
            Extensions =
            {
                ["traceId"] = HttpContext.TraceIdentifier
            }
        };
        error.Errors.Add(new(property, new[] { message }));
        return error;
    }
}
