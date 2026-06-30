using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MasterData
{

    /// <summary>
    /// Products Tablosu 
    /// </summary>
    [Table("Products", Schema = "Pim")]
    public class MasterDataProductEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? EcommProductName { get; set; }
        public string? EcommEan { get; set; }
        public string? Type { get; set; }
        public string? HandlingType { get; set; }
        public int? StatusId { get; set; }
        public DateTime? LastOnlineRelease { get; set; }
        public DateTime? OnlineReleasedAt { get; set; }
        public DateTime? MdmCreatedAt { get; set; }
        public DateTime? MdmUpdatedAt { get; set; }
        public DateTime? PimCreatedAt { get; set; }
        public bool? isArchived { get; set; }
        public int? GlobalId { get; set; }
        public string? PrimaryEan { get; set; }
        public string? ProductName { get; set; }
        public string? NameCustomerFriendly { get; set; }
        public string? LifecycleStatus { get; set; }
        public string? KindOfProduct { get; set; }
        public bool? ReleasedForOnline { get; set; }
        public int? LogisticClass { get; set; }
        public int? ManufacturerId { get; set; }
        public string? ManufacturerName { get; set; }
        public DateTime? EolDate { get; set; }
        public bool? BasePriceRequiredFlag { get; set; }
        public string? FeatureFrameName { get; set; }
        public string? SeoSlug { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
    }


}
