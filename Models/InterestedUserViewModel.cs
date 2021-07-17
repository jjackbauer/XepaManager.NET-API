using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfectedAPI.Models
{
    public class InterestedUserViewModel
    {
        public InterestedUserViewModel(long idNumber, string name, DateTime birthday, bool isComorbidity, double longitude, double latitude)
        {
            IdNumber = idNumber;
            Name = name;
            Birthday = birthday;
            IsComorbidity = isComorbidity;
            Longitude = longitude;
            Latitude = latitude;
        }
        public InterestedUserViewModel()
        {

        }

        public long IdNumber { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsComorbidity { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
