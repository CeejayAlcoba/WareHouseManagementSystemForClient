using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagementSystemForClient.Model.DTOModels.BillingModel;

namespace WareHouseManagementSystemForClient.Repositories.AutoMapperProfiles.BillingProfiles
{
    public  class HandlingOutProfile : Profile
    {
        public HandlingOutProfile()
        {
            CreateMap<IEnumerable<HandlingOutSqlColumns>, HandlingOutBillDTO>()
                .ForMember(dest => dest.HandlingOutBill_Det, opt => opt.MapFrom(src => src.Select(c => c.HandlingOutBill_Det).FirstOrDefault()))
                .ForMember(dest => dest.TotalCharge, opt => opt.MapFrom(src => src.Select(c => c.TotalCharge).FirstOrDefault()))
                .ForMember(dest => dest.UOM, opt => opt.MapFrom(src => src.Select(c => c.UOM).FirstOrDefault()))
                .ForMember(dest => dest.TotalUomValue, opt => opt.MapFrom(src => src.Select(c => c.TotalUomValue).FirstOrDefault()))
                .ForMember(dest => dest.HandlingOutItems, opt => opt.MapFrom(src => src.ToList()));

            //CreateMap<BillingItem, BillingDTO>();
            //CreateMap<BillingSQL, BillingItem>();
        }
    }
}
