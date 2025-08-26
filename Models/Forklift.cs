namespace ForkliftControl.Models
{
    public class Forklift
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ModelNumber { get; set; } = string.Empty;
        public DateTime ManufacturingDate { get; set; } = DateTime.MinValue;
    }
}