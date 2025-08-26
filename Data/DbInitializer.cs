namespace ForkliftControl.Data
{
    using System.Text.Json;
    using ForkliftControl.Models;
    
    public static class DbInitializer
    {
        public static void Initialize(ForkliftContext context)
        {
            context.Database.EnsureCreated();

            // Read JSON file first
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "forklifts.json");
            if (!File.Exists(jsonPath))
            {
                throw new FileNotFoundException($"Seed file not found: {jsonPath}");
            }

            try
            {
                var jsonData = File.ReadAllText(jsonPath);
                var forklifts = JsonSerializer.Deserialize<List<Forklift>>(jsonData);

                // Use transaction for atomic database operations
                using var transaction = context.Database.BeginTransaction();
                
                // Clear existing data and add new data in single transaction
                context.Forklifts.RemoveRange(context.Forklifts);
                
                if (forklifts != null && forklifts.Any())
                {
                    context.Forklifts.AddRange(forklifts);
                }
                
                context.SaveChanges();
                transaction.Commit();
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Invalid JSON format in {jsonPath}: {ex.Message}", ex);
            }
        }
    }
}
