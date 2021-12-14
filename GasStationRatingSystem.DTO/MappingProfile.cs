using AutoMapper;
using GasStationRatingSystem.DTO.Guide;
using GasStationRatingSystem.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.DTO
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BranchDTO, Branch>().ReverseMap();
            CreateMap<RegionDTO, Region>().ReverseMap();
            CreateMap<CityDTO, City>().ReverseMap();
            CreateMap<DistrictDTO, District>().ReverseMap();
            CreateMap<UserTypeDTO, UserType>().ReverseMap();
            CreateMap<QuestionSaveDTO, Question>().ReverseMap();
            CreateMap<DashboardChartDTO, DashboardChart>().ReverseMap();
            CreateMap<MainInfluenceDTO, MainInfluence>().ReverseMap();






        }
    }
}
