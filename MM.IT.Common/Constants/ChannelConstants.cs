using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Constants
{
    public static class ChannelConstants
    {
        /// <summary>
        /// ilk kanal ismi
        /// </summary>
        public const string FOMFirstData = "FOM:FirstData:0";

        /// <summary>
        /// ikinci kanal ismi
        /// </summary>
        public const string FOMNewOrderData = "FOM:NewOrderData:2";

        /// <summary>
        /// üçüncü kanal ismi
        /// </summary>
        public const string FOMOrderDbInsertData = "FOM:OrderDbInsertData:3";

        /// <summary>
        /// dördüncü kanal ismi
        /// </summary>
        public const string FOMOrderUpdateData = "FOM:OrderUpdateData:4";

        /// <summary>
        /// beşinci kanal ismi
        /// </summary>
        public const string FOMSMSSendData = "FOM:SMSSendData:5";

        /// <summary>
        /// altıncı kanal ismi
        /// </summary>
        public const string FOMFlowCheckData = "FOM:FlowCheckData:6";

        /// <summary>
        /// onuncu kanal ismi
        /// </summary>
        public const string FOMBackOrderData = "FOM:BackOrderData:10";

        /// <summary>
        /// onbirinci kanal ismi
        /// </summary>
        public const string FOMShippedData = "FOM:ShippedData:11";

        /// <summary>
        /// on ikinci kanal ismi
        /// </summary>
        public const string FOMPimData = "FOM:PimData:12";

        /// <summary>
        /// on üçüncü kanal ismi
        /// </summary>
        public const string FOMSendToFspData = "FOM:SendToFspData:13";

        /// <summary>
        /// on beşinci kanal ismi
        /// </summary>
        public const string FOMVCRData = "FOM:VCRData:15";

        /// <summary>
        /// on altıncı kanal ismi
        /// </summary>
        public const string FOMVCRErrorData = "FOM:VCRErrorData:16";

        /// <summary>
        /// on yedinci kanal ismi
        /// </summary>
        public const string FOMT800MPData = "FOM:T800MPData:17";

        /// <summary>
        /// on sekinci kanal ismi
        /// </summary>
        public const string FOMT800MPErrorData = "FOM:T800MPErrorData:18";

        /// <summary>
        /// on dokuncu kanal ismi
        /// </summary>
        public const string FOMMasterDataStatusListData = "FOM:MasterDataStatusListData:19";
    }
}
