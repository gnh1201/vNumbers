// online-sms.org

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class OnlineSMS : IncomingBase
    {
        public OnlineSMS()
        {
            Domain = "online-sms.org";
        }

        public override List<vMessage> Parse(string HTMLContent, string CurrentURL)
        {
            throw new System.NotImplementedException();
        }
    }
}
