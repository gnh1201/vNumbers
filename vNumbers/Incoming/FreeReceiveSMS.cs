// www.freereceivesms.com

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class FreeReceiveSMS : IncomingBase
    {
        public FreeReceiveSMS()
        {
            Domain = "www.freereceivesms.com";
        }

        public override List<vMessage> Parse(string HTMLContent, string URL)
        {
            throw new System.NotImplementedException();
        }
    }
}
