using Business.Models.Order;

namespace Business.Abstract;

public interface IOrderService
{
  Task<OrderStatusViewModel> CreateOrder(OrderCheckOutInfoInput orderCheckOutInfoInput);
  Task<OrderStatusViewModel> SuspectOrder(OrderCheckOutInfoInput orderCheckOutInfoInput);
  
  Task<List<OrderViewModel>> GetOrders();
  Task<List<OrderViewModel>> GetAllOrder();

}