// sms-receive.net

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class SMSReceive : IncomingBase
    {
        public SMSReceive()
        {
            Domain = "sms-receive.net";
        }

        public override List<vMessage> Parse(string HTMLContent, string CurrentURL)
        {
            throw new System.NotImplementedException();
        }
    }
}
