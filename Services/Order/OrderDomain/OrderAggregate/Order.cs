using OrderCore;

namespace OrderDomain.OrderAggregate;

public class Order : Entity, IAggregateRoot
{
    //Ef core feature type, owned types, shadow property, backing field
    //https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities
    //https://docs.microsoft.com/en-us/ef/core/modeling/shadow-properties
    //https://docs.microsoft.com/en-us/ef/core/modeling/backing-field
    //https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities#backing-field
    //https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities#backing-field
    //https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities#backing-field
    public DateTime CreatedTime { get; private set; }
    public Address Address { get; private set; }
    public string BuyerId { get; private set; }
    
    private readonly List<OrderItem> _orderItems;
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
    
    public Order() { }


    public Order(string buyerId, Address address)
    {
        BuyerId = buyerId;
        Address = address;
        CreatedTime = DateTime.Now;
        _orderItems = new List<OrderItem>();
    }
    
    public void AddOrderItem(string productId, string productName, string pictureUrl ,decimal price)
    {
        var existProduct = _orderItems.Any(x => x.ProductId == productId);
        if (!existProduct)
        {
            var orderItem = new OrderItem(productId, productName, pictureUrl, price);
            _orderItems.Add(orderItem);
        }
       
    }
    
    public decimal GetTotalPrice()
    {
        return _orderItems.Sum(x => x.Price);
    }
    


}