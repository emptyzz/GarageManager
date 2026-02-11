using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManager.Models
{
    public class ServiceRecord
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } = "";
        public decimal Cost { get; set; }
        public int MileageKm { get; set; }
        public int Id { get; set; }
        public int CarId { get; set; }
    }
}
