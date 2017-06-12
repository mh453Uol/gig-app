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
                    .ForMember(dest => dest.Genre,
                        options => options.Ignore());
                    
                config.CreateMap<Models.Gig, GigsFormViewModel>();
            });
        }
    }
}
