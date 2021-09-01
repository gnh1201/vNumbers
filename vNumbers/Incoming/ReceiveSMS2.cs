// www.receivesms.co

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class ReceiveSMS2 : IncomingBase
    {
        public ReceiveSMS2()
        {
            Domain = "www.receivesms.co";
        }

        public override List<vMessage> Parse(string HTMLContent, string URL)
        {
            throw new System.NotImplementedException();
        }
    }
}
