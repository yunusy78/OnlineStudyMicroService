using MediatR;
using OnlineStudyShared;
using OrderApplication.Dtos;

namespace OrderApplication.Queries;

public class GetAllOrdersQuery : IRequest<ResponseDto<List<OrderDto>>>
{
    
}