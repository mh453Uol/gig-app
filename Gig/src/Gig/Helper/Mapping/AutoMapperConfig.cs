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
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<GigsFormViewModel, Models.Gig>();
                cfg.CreateMap<Models.Gig, GigsFormViewModel>();
            });
        }
    }
}
