using AptitudeTestServer.Data;
using AptitudeTestServer.Models;
using AptitudeTestServer.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AptitudeTestServer.Controllers;

[ApiController]
[Route("api/quotes")]
public class QuotesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<QuotesController> _logger;
    
    public QuotesController(ApplicationDbContext context, ILogger<QuotesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var quotes = await _context.Quotes
            .Select(q => new QuoteDto(
                q.Id,
                q.Name,
                q.Premium,
                q.State.Rate,
                q.Tiv,
                q.State.Abbreviation,
                q.State.Id))
            .ToListAsync();
        return Ok(quotes);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var quote = await _context.Quotes
                .Select(q => new QuoteDto(
                    q.Id,
                    q.Name,
                    q.Premium,
                    q.State.Rate,
                    q.Tiv,
                    q.State.Abbreviation,
                    q.State.Id))
                .FirstOrDefaultAsync(q => q.Id == id);
        return Ok(quote);
    }
    
    // Implement POST
    
    // Implement PUT
    
    // To supply a select / dropdown?
    [HttpGet("states")]
    public async Task<IActionResult> GetAllStates()
    {
        var states = await _context.States
            .OrderBy(state => state.Abbreviation)
            .Select(state => new StateDto(state.Id, state.Abbreviation))
            .ToListAsync();
        return Ok(states);
    }
}