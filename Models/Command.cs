namespace ForkliftControl.Models
{
    public class ParseCommandRequest
    {
        public string CommandString { get; set; } = string.Empty;
    }

    public class ParsedCommand
    {
        public string Action { get; set; } = string.Empty;
        public int Value { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class ParseCommandResponse
    {
        public List<ParsedCommand> Commands { get; set; } = new();
        public bool IsValid { get; set; }
        public string? ErrorMessage { get; set; }
    }
}