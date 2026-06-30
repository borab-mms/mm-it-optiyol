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
    /// Fom Data DB -> Order_Item_FulFillments Entity Nesnesi
    /// </summary>
    [Table("Order_Item_FulFillment", Schema = "dbo")]
    public class OrderItemFulFillmentEntity : IEntity
    {
        public string customer_order_number { get; set; }
        public int order_item_id { get; set; }
        public int item_id { get; set; }
        public string fulfillment_order_id { get; set; }
        public string fulfillment_method { get; set; }
        public int fulfillment_outlet_procure_node_id { get; set; }
        public int fulfillment_outlet_ship_node_id { get; set; }
        public int fulfillment_quantity { get; set; }
    }
}
