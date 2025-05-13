using System.ComponentModel.DataAnnotations;

namespace SecondHandECommerce.Domain.Enums;

public enum OrderStatus
{
    [Display(Name = "Pending Confirmation")]
    Pending,

    [Display(Name = "Order Confirmed")]
    Confirmed,

    [Display(Name = "Order Cancelled")]
    Cancelled,

    [Display(Name = "Item Shipped")]
    Shipped,

    [Display(Name = "Order Completed")]
    Completed
}