// sms-online.co

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class SMSOnline2 : IncomingBase
    {
        public SMSOnline2()
        {
            Domain = "sms-online.co";
        }

        public override List<vMessage> Parse(string HTMLContent, string URL)
        {
            throw new System.NotImplementedException();
        }
    }
}
