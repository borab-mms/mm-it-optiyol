using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.DigitalCard
{
    public class DCItemSummaryModel
    {
        public int Id { get; set; }
        public string CustomerOrderNumber { get; set; }
        public int ItemId { get; set; }
        public string LineItemId { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int DCProductMapsId { get; set; }
        public string BarcodeNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerUserId { get; set; }
        public string CustomerSalutation { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal ItemPriceNet { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
