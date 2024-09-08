namespace Shopping.Web.Models.Ordering
{
    public record Order(
        Guid Id,
        Guid CustomerId,
        string OrderName,
        AddressModel ShippingAddress,
        AddressModel BillingAddress,
        PaymentModel Payment,
        OrderStatus Status,
        List<OrderItem> OrderItems);

    public record OrderItem(Guid OrderId, Guid ProductId, int Quantity, decimal Price);

    public record AddressModel(string FirstName, string LastName, string EmailAddress, string AddressLine, string Country, string State, string ZipCode);

    public record PaymentModel(string CardName, string CardNumber, string Expiration, string Cvv, int PaymentMethod);

    public enum OrderStatus
    {
        Draft = 1,
        Pending = 2,
        Completed = 3,
        Cancelled = 4
    }

    //wrapper classes
    public record GetOrdersResponse(PaginatedResult<Order> Orders);
    public record GetOrdersByNameResponse(IEnumerable<Order> Orders);
    public record GetOrdersByCustomerResponse(IEnumerable<Order> Orders);
}