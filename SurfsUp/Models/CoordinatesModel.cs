using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurfsUp.Models
{
    public class CoordinatesModel
    {
        public List<List<double>> Coordinates { get; set; }

        public CoordinatesModel()
        {
            Coordinates = new List<List<double>>();
        }
    }
}