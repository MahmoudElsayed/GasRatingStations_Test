using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.DTO
{
    public class MainInfluenceDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public List<Guid> QuestionsIds { get; set; }
       
    }
    public class MainInfluenceTableDTO
    {
        public int TotalCount { get; set; }
        public Guid ID { get; set; }

        public string Name { get; set; }
        public string AddedDate { get; set; }

        public string QuestionsIds { get; set; }
        public string QuestionsNames { get; set; }


    }
}
