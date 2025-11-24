using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Shiemi.Utilities.ServiceProviders;

public static class MapperProvider
{
    public static Mapper? GetMapper<TSource, TDest>()
    {
        var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<TSource, TDest>(),
            new LoggerFactory()
        );

        return config.CreateMapper() as Mapper;
    }
}
