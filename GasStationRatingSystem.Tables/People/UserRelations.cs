using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.Tables
{
    public partial class User
    {
        #region Relations


        public virtual ICollection<UserRegion> UserRegions { get; set; }

        public virtual UserType UserType { get; set; }
        public ICollection<ManualDistribution> ManualDistributions { get; set; }
        public ICollection<UserPermission> UserPermissionCreated { get; set; }
        public ICollection<UserPermission> UserPermissionModified { get; set; }
        public ICollection<UserPermission> UserPermissionDeleted { get; set; }

        #region Pages
        public ICollection<Area> AreaCreated { get; set; }
        public ICollection<Area> AreaModified { get; set; }
        public ICollection<Area> AreaDeleted { get; set; }

        public ICollection<Page> PageCreated { get; set; }
        public ICollection<Page> PageModified { get; set; }
        public ICollection<Page> PageDeleted { get; set; }

        public ICollection<ActionsPage> ActionsPageCreated { get; set; }
        public ICollection<ActionsPage> ActionsPageModified { get; set; }
        public ICollection<ActionsPage> ActionsPageDeleted { get; set; }

        #endregion
        #region User

        public ICollection<User> UserCreated { get; set; }
        public ICollection<User> UserModified { get; set; }
        public ICollection<User> UserDeleted { get; set; }
        public ICollection<UserType> UserTypeCreated { get; set; }
        public ICollection<UserType> UserTypeModified { get; set; }
        public ICollection<UserType> UserTypeDeleted { get; set; }
        public ICollection<UserRegion> UserRegionCreated { get; set; }
        public ICollection<UserRegion> UserRegionModified { get; set; }
        public ICollection<UserRegion> UserRegionDeleted { get; set; }



        #endregion
        #region Guide
        public ICollection<GasStation> GasStationCreated { get; set; }
        public ICollection<GasStation> GasStationModified { get; set; }
        public ICollection<GasStation> GasStationDeleted { get; set; }
        public ICollection<GasStationContact> GasStationContactCreated { get; set; }
        public ICollection<GasStationContact> GasStationContactModified { get; set; }
        public ICollection<GasStationContact> GasStationContactDeleted { get; set; }

        public ICollection<MainInfluence> MainInfluenceCreated { get; set; }
        public ICollection<MainInfluence> MainInfluenceModified { get; set; }
        public ICollection<MainInfluence> MainInfluenceDeleted { get; set; }

        public ICollection<MainInfluenceQuestion> MainInfluenceQuestionCreated { get; set; }
        public ICollection<MainInfluenceQuestion> MainInfluenceQuestionModified { get; set; }
        public ICollection<MainInfluenceQuestion> MainInfluenceQuestionDeleted { get; set; }


        #region Branch
        public ICollection<Branch> BranchCreated { get; set; }
        public ICollection<Branch> BranchModified { get; set; }
        public ICollection<Branch> BranchDeleted { get; set; }
        #endregion

        #region Region
        public ICollection<Region> RegionCreated { get; set; }
        public ICollection<Region> RegionModified { get; set; }
        public ICollection<Region> RegionDeleted { get; set; }
        #endregion
        #region City

        public ICollection<City> CityCreated { get; set; }
        public ICollection<City> CityModified { get; set; }
        public ICollection<City> CityDeleted { get; set; }
        #endregion


        #region District
        public ICollection<District> DistrictCreated { get; set; }
        public ICollection<District> DistrictModified { get; set; }
        public ICollection<District> DistrictDeleted { get; set; }


        #endregion
        #region Questions
        public ICollection<Question> QuestionCreated { get; set; }
        public ICollection<Question> QuestionModified { get; set; }
        public ICollection<Question> QuestionDeleted { get; set; }

        public ICollection<AnswerCategory> AnswerCategoryCreated { get; set; }
        public ICollection<AnswerCategory> AnswerCategoryModified { get; set; }
        public ICollection<AnswerCategory> AnswerCategoryDeleted { get; set; }

        public ICollection<Answer> AnswerCreated { get; set; }
        public ICollection<Answer> AnswerModified { get; set; }
        public ICollection<Answer> AnswerDeleted { get; set; }
        #endregion
        #endregion
    
        public ICollection<ManualDistribution> ManualDistributionCreated { get; set; }
        public ICollection<ManualDistribution> ManualDistributionModified { get; set; }
        public ICollection<ManualDistribution> ManualDistributionDeleted { get; set; }


        public ICollection<DashboardChart> DashboardChartCreated { get; set; }
        public ICollection<DashboardChart> DashboardChartModified { get; set; }
        public ICollection<DashboardChart> DashboardChartDeleted { get; set; }


        public ICollection<VisitNote> VisitNoteCreated { get; set; }
        public ICollection<VisitNote> VisitNoteModified { get; set; }
        public ICollection<VisitNote> VisitNoteDeleted { get; set; }

        public ICollection<VisitNoteImage> VisitNoteImageCreated { get; set; }
        public ICollection<VisitNoteImage> VisitNoteImageModified { get; set; }
        public ICollection<VisitNoteImage> VisitNoteImageDeleted { get; set; }



        public ICollection<VisitQuestionImage> VisitQuestionImageCreated { get; set; }
        public ICollection<VisitQuestionImage> VisitQuestionImageModified { get; set; }
        public ICollection<VisitQuestionImage> VisitQuestionImageDeleted { get; set; }


        public ICollection<VisitQuestionImageDetail> VisitQuestionImageDetailCreated { get; set; }
        public ICollection<VisitQuestionImageDetail> VisitQuestionImageDetailModified { get; set; }
        public ICollection<VisitQuestionImageDetail> VisitQuestionImageDetailDeleted { get; set; }


        public ICollection<VisitSignture> VisitSigntureCreated { get; set; }
        public ICollection<VisitSignture> VisitSigntureModified { get; set; }
        public ICollection<VisitSignture> VisitSigntureDeleted { get; set; }


        public ICollection<VisitSigntureDataImage> VisitSigntureDataImageCreated { get; set; }
        public ICollection<VisitSigntureDataImage> VisitSigntureDataImageModified { get; set; }
        public ICollection<VisitSigntureDataImage> VisitSigntureDataImageDeleted { get; set; }


        public ICollection<VisitInfo> VisitInfoCreated { get; set; }
        public ICollection<VisitInfo> VisitInfoModified { get; set; }
        public ICollection<VisitInfo> VisitInfoDeleted { get; set; }




        public ICollection<VisitApproval> VisitApprovalCreated { get; set; }
        public ICollection<VisitApproval> VisitApprovalModified { get; set; }
        public ICollection<VisitApproval> VisitApprovalDeleted { get; set; }
        
        public ICollection<VisitAnswer> VisitAnswerCreated { get; set; }
        public ICollection<VisitAnswer> VisitAnswerModified { get; set; }
        public ICollection<VisitAnswer> VisitAnswerDeleted { get; set; }

        public ICollection<VisitAnswerOption> VisitAnswerOptionCreated { get; set; }
        public ICollection<VisitAnswerOption> VisitAnswerOptionModified { get; set; }
        public ICollection<VisitAnswerOption> VisitAnswerOptionDeleted { get; set; }
        #endregion

    }
}
