using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.DTOModels.BillingModel;

namespace WareHouseManagementSystemForClient.Repositories.AutoMapperProfiles.BillingProfiles
{
    public class HandlingInProfile : Profile
    {
        public HandlingInProfile()
        {
            CreateMap<IEnumerable<HandlingInSqlColumns>, HandlingInBillDTO>()
                .ForMember(dest => dest.HandlingInBill_Det, opt => opt.MapFrom(src => src.Select(c => c.HandlingInBill_Det).FirstOrDefault()))
                .ForMember(dest => dest.TotalCharge, opt => opt.MapFrom(src => src.Select(c => c.TotalCharge).FirstOrDefault()))
                .ForMember(dest => dest.UOM, opt => opt.MapFrom(src => src.Select(c => c.UOM).FirstOrDefault()))
                .ForMember(dest => dest.TotalUomValue, opt => opt.MapFrom(src => src.Select(c => c.TotalUomValue).FirstOrDefault()))
                .ForMember(dest => dest.HandlingInItems, opt => opt.MapFrom(src => src.ToList()));

            //CreateMap<BillingItem, BillingDTO>();
            //CreateMap<BillingSQL, BillingItem>();
        }
    }
}
