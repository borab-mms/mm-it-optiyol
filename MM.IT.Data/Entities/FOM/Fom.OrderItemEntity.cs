using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.FOM
{
    /// <summary>
    /// Fom Data DB -> Order Item Entity Nesnesi
    /// </summary>
    [Table("Order_Item", Schema = "dbo")]
    public class FomOrderItemEntity : IEntity
    {
        public string customer_order_number { get; set; }
        public int item_id { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string line_item_status_description { get; set; }
    }
}
