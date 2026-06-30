using MM.IT.Data.Entities.Base;
using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE
{
    /// <summary>
    /// OrderHead Tablosu 
    /// </summary>
    [Table("OrderHead", Schema = "MarketPlace")]
    public class OrderHeadEntity : IEntity
    {
        [Key]
        public string OrderHeadId { get; set; }
        public int? OrderStatusId { get; set; }
        public int CustomerOrderNumber { get; set; }
        public int ChannelCode { get; set; }
        public string ChannelOrderNumber { get; set; }
        public string ChannelPackageNumber { get; set; }
        public string IntegratorOrderNumber { get; set; }
        public string? CustomerType { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime ChannelOrderDate { get; set; }
        public int? ChannelOrderStatusId { get; set; }
        public string ShippingCompany { get; set; }
        public string ChannelShipmentType { get; set; }
        public string? CargoCode { get; set; }
        public DateTime? ShippingDue { get; set; }
        public string? CurrencyCode { get; set; }
        public string? ChannelOrderNote { get; set; }
        public string? OrderType { get; set; }
        public string? FomDeliveryType { get; set; }
        public int? OutletId { get; set; }
        public int? WhId { get; set; }
        public string? FomOrderStatus { get; set; }
        public DateTime? FomOrderDate { get; set; }
        public int? UserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
