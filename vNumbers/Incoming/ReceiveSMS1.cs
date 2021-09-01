// receive-smss.com

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class ReceiveSMS1 : IncomingBase
    {
        public ReceiveSMS1()
        {
            Domain = "receive-smss.com";
        }

        public override List<vMessage> Parse(string HTMLContent, string URL)
        {
            throw new System.NotImplementedException();
        }
    }
}
