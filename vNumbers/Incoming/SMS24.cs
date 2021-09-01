// sms24.me

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    class SMS24 : IncomingBase
    {
        public SMS24()
        {
            Domain = "sms24.me";
        }

        public override List<vMessage> Parse(string HTMLContent, string CurrentURL)
        {
            throw new System.NotImplementedException();
        }
    }
}
