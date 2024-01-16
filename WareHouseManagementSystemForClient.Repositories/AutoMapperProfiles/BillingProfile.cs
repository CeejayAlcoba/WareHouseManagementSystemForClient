using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.DTOModels.BillingModel;
using WareHouseManagementSystemForClient.Model.DTOModels.ReportModels;

namespace WareHouseManagementSystemForClient.Repositories.AutoMapperProfiles
{
    public class BillingProfile : Profile
    {
        public BillingProfile()
        {
            CreateMap<IEnumerable<BillingSQL>, BillingDTO>()
                .ForMember(dest => dest.TotalVolume, opt => opt.MapFrom(src => src.Select(c => c.TotalVolume).FirstOrDefault()))
                .ForMember(dest => dest.TotalCharge, opt => opt.MapFrom(src => src.Select(c => c.TotalCharge).FirstOrDefault()))
                .ForMember(dest => dest.BillingItems, opt => opt.MapFrom(src => src.ToList()));
            CreateMap<BillingItem, BillingDTO>();
            CreateMap<BillingSQL,BillingItem >();
        }
    }
}
