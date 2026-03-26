namespace Ecom.Core.Entites.Order;

public class DeliveryMethod: BaseEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string DeliveryTime { get; set; }

}