// receive-sms.cc

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class ReceiveSMS4 : IncomingBase
    {
        public ReceiveSMS4()
        {
            Domain = "receive-sms.cc";
        }

        public override List<vMessage> Parse(string HTMLContent, string URL)
        {
            throw new System.NotImplementedException();
        }
    }
}
