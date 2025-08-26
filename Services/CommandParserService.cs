using System.Text.RegularExpressions;
using ForkliftControl.Models;

namespace ForkliftControl.Services
{
    public class CommandParserService
    {
        private static readonly Regex CommandRegex = new(@"([FBLR])(\d+)", RegexOptions.Compiled);

        public ParseCommandResponse ParseCommands(string input)
        {
            var response = new ParseCommandResponse();
            
            try
            {
                var matches = CommandRegex.Matches(input.ToUpper());
                
                foreach (Match match in matches)
                {
                    var action = match.Groups[1].Value;
                    var value = int.Parse(match.Groups[2].Value);
                    
                    // Validate turn angles
                    if ((action == "L" || action == "R") && (value % 90 != 0 || value < 0 || value > 360))
                    {
                        response.IsValid = false;
                        response.ErrorMessage = $"Invalid turn angle: {value}. Must be multiple of 90 between 0-360.";
                        return response;
                    }
                    
                    var description = action switch
                    {
                        "F" => $"Move Forward by {value} metres.",
                        "B" => $"Move Backward by {value} metres.",
                        "L" => $"Turn Left by {value} degrees.",
                        "R" => $"Turn Right by {value} degrees.",
                        _ => $"Unknown command: {action}{value}"
                    };
                    
                    response.Commands.Add(new ParsedCommand
                    {
                        Action = action,
                        Value = value,
                        Description = description
                    });
                }
                
                response.IsValid = true;
                return response;
            }
            catch (Exception ex)
            {
                response.IsValid = false;
                response.ErrorMessage = $"Error parsing commands: {ex.Message}";
                return response;
            }
        }
    }
}