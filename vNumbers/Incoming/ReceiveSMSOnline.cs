// receive-sms-online.info

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class ReceiveSMSOnline : IncomingBase
    {
        public ReceiveSMSOnline()
        {
            Domain = "receive-sms-online.info";
        }

        public override List<vMessage> Parse(string HTMLContent, string CurrentURL)
        {
            throw new System.NotImplementedException();
        }
    }
}
