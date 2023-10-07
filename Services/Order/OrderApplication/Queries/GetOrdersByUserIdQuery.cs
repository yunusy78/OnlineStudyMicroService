using MediatR;
using OnlineStudyShared;
using OrderApplication.Dtos;

namespace OrderApplication.Queries;

public class GetOrdersByUserIdQuery : IRequest<ResponseDto<List<OrderDto>>>
{
    public string UserId { get; set; }
}