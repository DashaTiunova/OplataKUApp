﻿using AutoMapper;
using ClientInfoData;
using OplataKUWebApi.Models.Client;
using OplataKUWebApi.Models.Pay;
namespace OplataKUWebApi

{
    public class MapperProfile:Profile

    {
        public MapperProfile()
        {
            CreateMap<ClientAddDto, ClientInfo>();
            CreateMap<PayAddDto, PayInfo>();
            CreateMap<PayInfo, PayGetDto>();
            CreateMap<ClientInfo, ClientGetDto>()
                //.ForMember(dest => dest.ClientId,
                //opt => opt.MapFrom(src => src.Id))

                .ForMember(dest => dest.Fullname,
                opt => opt.MapFrom(src => $"{src.Lastname}  {src.Firstname}  {src.Midname}"))

             .ForMember(dest => dest.ApartId,
                opt => opt.MapFrom(src => src.ApartId));
            ///если нужно скрыть данные
            //  .ForMember(dest => dest.Email,
            //  opt => opt.Ignore());
        }
    }
}
