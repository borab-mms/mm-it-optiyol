using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.PIM
{
    public class Feature
    {
        public string FeatureId { get; set; }
        public string FeatureName { get; set; }
        public List<SubFeature> SubFeatures { get; set; }
    }

    public class FeatureAndSubFeatureModel
    {
        public List<Feature> Features { get; set; }
    }

    public class SubFeature
    {
        public string SubFeatureId { get; set; }
        public string SubFeatureName { get; set; }
        public string SubFeatureValue { get; set; }
    }
}
