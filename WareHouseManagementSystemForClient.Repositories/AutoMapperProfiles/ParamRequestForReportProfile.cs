using AutoMapper;
using WareHouseManagementSystemForClient.Model.URLSearchParameterModels;

namespace WareHouseManagementSystemForClient.Repositories.AutoMapperProfiles
{
    public class ParamRequestForReportProfile : Profile
    {
        public ParamRequestForReportProfile()
        {
            CreateMap<CargoURLSearch, ReportsURLSearch>();
        }
    }
}
