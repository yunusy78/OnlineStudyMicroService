using AutoMapper;
using OrderApplication.Dtos;
using OrderDomain.OrderAggregate;

namespace OrderApplication.Mapping;

public class ObjectMapper
{
    public static readonly Lazy<IMapper> lazy = new(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
           cfg.AddProfile<CustomMapper>();
        });
        return config.CreateMapper();
    });
    
    public static IMapper Mapper => lazy.Value;
    
}