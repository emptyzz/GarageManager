using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManager.Models
{
    internal class GarageData
    {
        public List<Car> Cars { get; set; } = new List<Car>();
        public List<ServiceRecord> Records { get; set; } = new List<ServiceRecord>();
    }
}
