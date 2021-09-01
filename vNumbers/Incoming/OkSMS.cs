// oksms.org

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    class OkSMS : IncomingBase
    {
        public OkSMS()
        {
            Domain = "oksms.org";
        }

        public override List<vMessage> Parse(string HTMLContent, string URL)
        {
            throw new System.NotImplementedException();
        }
    }
}
