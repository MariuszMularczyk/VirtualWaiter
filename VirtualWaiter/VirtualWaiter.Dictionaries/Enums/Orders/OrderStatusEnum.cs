using System.ComponentModel.DataAnnotations;
using VirtualWaiter.Resources.Orders;

namespace VirtualWaiter.Dictionaries
{
    public enum WorkerFunctionEnum : int
    {
        [Display(ResourceType = typeof(OrderStatusResource), Name = "OrderStatus_Awaiting")]
        Awaiting = 1,
        [Display(ResourceType = typeof(OrderStatusResource), Name = "OrderStatus_InProgress")]
        InProgress = 2,
        [Display(ResourceType = typeof(OrderStatusResource), Name = "OrderStatus_Done")]
        Done = 3
    }
}
