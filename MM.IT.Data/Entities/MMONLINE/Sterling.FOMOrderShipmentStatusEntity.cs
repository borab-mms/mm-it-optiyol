using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE
{
    /// <summary>
    /// OrderShipmentStatus Tablosu 
    /// </summary>
    [Table("OrderShipmentStatus", Schema = "Sterling")]
    public class FOMOrderShipmentStatusEntity : BaseEntity<int>
    {
        /// <summary>
        /// CustomerOrderNumber Bilgisi
        /// </summary>
        public string? CustomerOrderNumber { get; set; }

        /// <summary>
        /// OrderMethod Bilgisi
        /// </summary>
        public string? OrderMethod { get; set; }

        /// <summary>
        /// ShipmentNumber Bilgisi
        /// </summary>
        public string? ShipmentNumber { get; set; }

        /// <summary>
        /// OutletId Bilgisi
        /// </summary>
        public int? OutletId { get; set; }

        /// <summary>
        /// ProductId Bilgisi
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// ProductId Bilgisi
        /// </summary>
        public int? SellerId { get; set; }

        /// <summary>
        /// SalesDocNumber Bilgisi
        /// </summary>
        public int? SalesDocNumber { get; set; }

        /// <summary>
        /// ShippingStatus bilgisini saklar.
        /// </summary>
        public bool? ShippingStatus { get; set; }

        /// <summary>
        /// StatusId bilgisini saklar.
        /// </summary>
        public int? StatusId { get; set; } 

        /// <summary>
        /// FulfillmentOrderID bilgisini saklar.
        /// </summary>
        public string? FulfillmentOrderID { get; set; }

        /// <summary>
        /// CarrierName bilgisini saklar.
        /// </summary>
        public string? CarrierName { get; set; }

        /// <summary>
        /// PickupLocation bilgisini saklar.
        /// </summary>
        public string? PickupLocation { get; set; }


        /// <summary>
        /// ShippingTrackingCode bilgisini saklar.
        /// </summary>
        public string? ShippingTrackingCode { get; set; }


        /// <summary>
        /// ShippingName bilgisini saklar.
        /// </summary>
        public string? ShippingName { get; set; }

        /// <summary>
        /// ShippingURL bilgisini saklar.
        /// </summary>
        public string? ShippingURL { get; set; }

        /// <summary>
        /// OrderLineNumber bilgisini saklar.
        /// </summary>
        public string? OrderLineNumber { get; set; }

        /// <summary>
        /// ShippingDate bilgisini saklar.
        /// </summary>
        public DateTime? ShippingDate { get; set; }

        /// <summary>
        /// DeliveryDate bilgisini saklar.
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// Güncelleme tarihi bilgisini saklar.
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

    }
}
