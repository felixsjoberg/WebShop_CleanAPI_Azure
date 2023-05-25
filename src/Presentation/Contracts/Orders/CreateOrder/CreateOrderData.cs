namespace Presentation.Contracts;

public record CreateOrderData
(
    Guid UserId,
     IEnumerable<OrderItemRequest> OrderItems,
     CustomerRequest Customer

);

public record OrderItemRequest(
    int ProductId,
    int Quantity
);

public record CustomerRequest(
    int Id,
    string FullName,
    string Streetaddress,
    string City,
    string Zipcode,
    string Country,
    string CountryCode
);
