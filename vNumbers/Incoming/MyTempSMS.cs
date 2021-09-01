// mytempsms.com

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class MyTempSMS : IncomingBase
    {
        public MyTempSMS()
        {
            Domain = "mytempsms.com";
        }

        public override List<vMessage> Parse(string HTMLContent, string URL)
        {
            throw new System.NotImplementedException();
        }
    }
}
