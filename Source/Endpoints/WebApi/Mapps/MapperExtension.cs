using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace WebApi.Mapps
{
    public static class MapperExtension
    {

        public static IMapper Mapper { get; set; }

        public static void  Configuration(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
