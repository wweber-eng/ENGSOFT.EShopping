using AutoMapper;

namespace Catalog.Application.Mappers
{
    public static class BaseMapper 
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(valueFactory: () =>
        {
            var config = new MapperConfiguration(configure: cfg =>
            { 
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<ProductMapperProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
      
    }
}
