using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.DTO
{
    public class ResultViewModel
    {
        public bool Status { get; set; } 
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
