// receive-sms-free.cc

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class ReceiveSMSFree : IncomingBase
    {
        public ReceiveSMSFree()
        {
            Domain = "sms-online.co";
        }

        public override List<vMessage> Parse(string HTMLContent, string CurrentURL)
        {
            throw new System.NotImplementedException();
        }
    }
}
