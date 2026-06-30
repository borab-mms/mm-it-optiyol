using MM.IT.Common.Helpers.PIMHelper;
using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE.Sterling
{
    /// <summary>
    /// OrderHead Tablosu 
    /// </summary>
    [Table("OrderHead", Schema = "FOM")]
    public class SterlingOrderHeadEntity : IEntity
    {

        [Key]
        public Guid GuidId { get; set; }
        //[Key]
        public int CustomerOrderNumber { get; set; }
        public string? CustomerOrderId { get; set; }
        public string? OrderMethod { get; set; }
        public string? Kind { get; set; }
        public string? State { get; set; }
        public string? AggregatedState { get; set; }
        public bool? IsCancellable { get; set; }
        public string? Country { get; set; }
        public string? Language { get; set; }
        public string? SalesLine { get; set; }
        public string? SourceSystemId { get; set; }
        public string? SourceSystemType { get; set; }
        public string? SourceSystemChannel { get; set; }
        public string? OrderState { get; set; }
        public string? BusinessRelationship { get; set; }
        public string? ContractualPartnerId { get; set; }
        public string? ContractualPartnerName { get; set; }
        public string? ContractualBusinessPartnerId { get; set; }
        public string? ContractualOperationType { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerPartyId { get; set; }
        public bool? CustomerGuest { get; set; }
        public DateTime? OrderCreatedDate { get; set; }
        public DateTime? OrderUpdatedDate { get; set; }
        public int? SellerId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<SterlingOrderItemEntity> OrderItems {  get; set; }
        //public virtual ICollection<SterlingOrderItemsStateQuantityEntity> SterlingOrderItemStateQuantities {  get; set; }
	}
}
