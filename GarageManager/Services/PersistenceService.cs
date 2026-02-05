using System;
using System.IO;
using System.Text.Json;
using GarageManager.Models;

namespace GarageManager.Services
{
    internal class PersistenceService
    {
        private readonly string _filePath;

        public PersistenceService(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(GarageData data)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(_filePath, json);
        }

        public GarageData Load()
        {
            if (!File.Exists(_filePath))
                return new GarageData();

            try
            {
                string json = File.ReadAllText(_filePath);
                var data = JsonSerializer.Deserialize<GarageData>(json);
                return data ?? new GarageData();
            }
            catch
            {
                return new GarageData();
            }
        }
    }
}
