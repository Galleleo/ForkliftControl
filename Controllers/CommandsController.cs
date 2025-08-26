using ForkliftControl.Data;
using Microsoft.AspNetCore.Mvc;
using ForkliftControl.Models;
using ForkliftControl.Services;

namespace ForkliftControl.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly CommandParserService _commandParser;
        private readonly ForkliftContext _context;

        public CommandsController(CommandParserService commandParser, ForkliftContext context)
        {
            _commandParser = commandParser;
            _context = context;
        }

        [HttpPost("parse")]
        public ActionResult<ParseCommandResponse> ParseCommands([FromBody] ParseCommandRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CommandString))
            {
                return BadRequest(new ParseCommandResponse 
                { 
                    IsValid = false, 
                    ErrorMessage = "Command string cannot be empty" 
                });
            }

            var result = _commandParser.ParseCommands(request.CommandString);
            return Ok(result);
        }
    }
}