using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.Common
{
  public static  class AppDateTime
    {
        /// <summary>
        /// تاريخ ووقت المملكة العربية السعودية
        /// </summary>
        public static DateTime Now => DateTime.UtcNow.AddHours(3);
    }
}
