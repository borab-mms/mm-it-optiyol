using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.PIM
{
    public class ProductSummaryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }
        public int PG { get; set; }
        public int MPG { get; set; }
        public string SalesPrice { get; set; }
        public string Barcode { get; set; }
        public List<ProductFeatureItem> ProductFeatureItems { get; set; }
    }
    public class ProductFeatureItem
    {
        public string FeatureId { get; set; }
        public string FeatureName { get; set; }
        public List<ProductSubFeaturesItem> SubFeatures { get; set; }
    }
    public class ProductSubFeaturesItem
    {
        public string SubFeatureId { get; set; }
        public string SubFeatureName { get; set; }
        public string SubFeatureValue { get; set; }
    }
}
