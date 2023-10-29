using Business.Models.Order;

namespace Business.Abstract;

public interface IOrderService
{
  Task<OrderStatusViewModel> CreateOrder(OrderCheckOutInfoInput orderCheckOutInfoInput);
  Task SuspectOrder(OrderCheckOutInfoInput orderCheckOutInfoInput);
  
  Task<List<OrderViewModel>> GetOrders();
  

}