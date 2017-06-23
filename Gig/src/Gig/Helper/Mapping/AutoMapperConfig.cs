using AutoMapper;
using Gig.Models.GigsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Helper.Mapping
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<GigsFormViewModel, Models.Gig>()
                    .ForMember(dest => dest.GenreId,
                        options => options.MapFrom(src => src.Genre))
                    .ForMember(dest => dest.DateAndTime,
                        options => options.MapFrom(src => src.DateAndTime()))
                    .ForMember(dest => dest.Genre,
                        options => options.Ignore());


                config.CreateMap<Models.Gig, GigsFormViewModel>()
                    .ForMember(dest => dest.Date,
                        options => options.MapFrom(src => src.DateAndTime.ToString("dd MMM yyyy")))
                    .ForMember(dest => dest.Time,
                        options => options.MapFrom(src => src.DateAndTime.ToString("HH:mm")))
                    .ForMember(dest => dest.Genre,
                        options => options.MapFrom(src => src.GenreId));
                    
            });
        }
    }
}
