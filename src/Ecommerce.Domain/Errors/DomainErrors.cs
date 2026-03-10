using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Errors;

public static class DomainErrors
{
    public static class Order
    {
        public static readonly Error ProductsNotFound = new(
            "Order.ProductsNotFound",
            "One or more products not found."
        );
        public static readonly Error NotFound = new(
            "Order.NotFound",
            "Order not found."
        );

        public static readonly Error InvalidStatusTransition = new("Order.InvalidStatusTransition", "Invalid status transition.");
    }

    public static class Product
    {
        public static readonly Error ConcurrencyConflict = new(
            "Product.ConcurrencyConflict",
            "The product has been modified by another user. Please refresh the data and try again."
        );

        public static readonly Error NotFound = new(
            "Product.ProductNotFound",
            "Product not found.");
    }
}