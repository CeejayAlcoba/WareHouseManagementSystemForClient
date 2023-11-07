using AutoMapper;
using WareHouseManagementSystemForClient.Model.DTOModels.ReportModels;

namespace WareHouseManagementSystemForClient.Repositories.AutoMapperProfiles
{
    public class ReportTableProfile : Profile
    {
        public ReportTableProfile()
        {
            CreateMap<IEnumerable<Report>, ReportDTO>()
                .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.Select(c => c.TotalQuantity).FirstOrDefault()))
                .ForMember(dest => dest.TotalSKU, opt => opt.MapFrom(src => src.Select(c => c.TotalSKU).FirstOrDefault()))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.ToList()));
            CreateMap<Report, ReportItem>();
            CreateMap<ReportDTO,ReportTotalsDTO>();
        }
    }
}
