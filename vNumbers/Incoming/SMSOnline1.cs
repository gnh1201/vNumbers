// www.smsonline.cloud

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class SMSOnline1 : IncomingBase
    {
        public SMSOnline1()
        {
            Domain = "www.smsonline.cloud";
        }

        public override List<vMessage> Parse(string HTMLContent, string URL)
        {
            throw new System.NotImplementedException();
        }
    }
}
