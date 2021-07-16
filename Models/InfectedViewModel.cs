using System;

namespace Api_Mongo.Models
{
    public class InfectedViewModel
    {
        public DateTime Birthday { get; set; }
        public string Sex { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}