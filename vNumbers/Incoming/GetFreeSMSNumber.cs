// getfreesmsnumber.com

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class GetFreeSMSNumber : IncomingBase
    {
        public GetFreeSMSNumber() {
            Domain = "getfreesmsnumber.com";
        }

        public override List<vMessage> Parse(string HTMLContent, string URL)
        {
            throw new System.NotImplementedException();
        }
    }
}
