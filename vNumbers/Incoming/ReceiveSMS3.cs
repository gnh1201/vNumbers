// www.receivesms.org

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class ReceiveSMS3 : IncomingBase
    {
        public ReceiveSMS3()
        {
            Domain = "www.receivesms.org";
        }

        public override List<vMessage> Parse(string HTMLContent, string URL)
        {
            throw new System.NotImplementedException();
        }
    }
}
