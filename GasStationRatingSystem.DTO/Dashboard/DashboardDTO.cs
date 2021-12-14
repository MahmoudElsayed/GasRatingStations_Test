using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.DTO
{
  public  class DashboardGetTotalsDTO
    {
        public string CityName { get; set; }
        public int Total { get; set; }
        public int InProgress { get; set; }
        public int Ended { get; set; }
        public int NotStarted => Total-(InProgress + Ended);
        public Guid ID { get; set; }
    }
    public class DashboardChartsDTO
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public int Realized { get; set; }
        public int Unrealized { get; set; }
        public int Total => Realized + Unrealized;

    }
}
