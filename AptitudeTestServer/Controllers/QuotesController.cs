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

    private decimal calculatePremium(decimal tiv, decimal rate){ 
    
        return  (tiv * rate) / 100;
    }
    
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
    [HttpPost]
    public async Task<IActionResult> AddQuote([FromBody] CreateQuote quote){ 
        var state = await _context.States.FindAsync(quote.StateId);

        //Error handling
        if(quote == null){ 
            return BadRequest("Quote");
        }

        if(quote.Tiv == 0){ 
            return BadRequest("No TIV Provided");
        }

        if(quote.StateId == 0){ 
            return BadRequest("No State Id Provided");
        }
        
        if(string.IsNullOrEmpty(quote.Name)){ 
            return BadRequest("No Name Provided");
        }

        var newQuote = new Quote{ 
            Name = quote.Name,
            Tiv = quote.Tiv,
            StateId = quote.StateId,
            Premium = calculatePremium(quote.Tiv, state.Rate)
        };
        
        await _context.Quotes.AddAsync(newQuote);
        await _context.SaveChangesAsync();

           
        var message = new { 
            Mesage = "Succesfully stored quote"
        };
        return Ok(message);
     
        
    }

    
    // Implement PUT
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditQute([FromBody] CreateQuote updatedQuote, [FromRoute] int id)
    {
        var existingQuote = await _context.Quotes.FindAsync(id);

        if (existingQuote == null)
        {
            return NotFound("Cannot find quote with id specified!");
        }

        // Fetch the state based on updated StateId
        var state = await _context.States.FindAsync(updatedQuote.StateId);

        if (state == null || state.Rate == 0)
        {
            return BadRequest("Invalid state or state rate.");
        }

        // Error handling for Tiv, StateId, and Name
        if (updatedQuote.Tiv <= 0)
        {
            return BadRequest("No valid Tiv provided");
        }

        if (updatedQuote.StateId == 0)
        {
            return BadRequest("No valid StateId provided");
        }

        if (string.IsNullOrEmpty(updatedQuote.Name))
        {
            return BadRequest("No Name provided");
        }

        // Update the fields
        existingQuote.Name = updatedQuote.Name;
        existingQuote.Tiv = updatedQuote.Tiv;
        existingQuote.StateId = updatedQuote.StateId;

        // Calculate and set the premium
        existingQuote.Premium = calculatePremium(updatedQuote.Tiv, state.Rate);

        // Save changes
        await _context.SaveChangesAsync();
        
        var message = new { 
            Mesage = "Quote updated succesfully"
        };

        return Ok(message);
    }

    
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