using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfectedAPI.Data.Collections
{
    public class InterestedUser
    {
        public InterestedUser(long IdNumber, string Name, DateTime Birthday, bool IsComorbidity, double longitude, double latitude)
        {
            this.IdNumber = IdNumber;
            this.Name = Name;
            this.Birthday = Birthday;
            this.IsComorbidity = IsComorbidity;
            this.Location = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }
        public long IdNumber { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsComorbidity { get; set; }
        public GeoJson2DGeographicCoordinates Location { get; set; }
    }
}
