// temporary-phone-number.com

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class TemporaryPhoneNumber : IncomingBase
    {
        public TemporaryPhoneNumber()
        {
            Domain = "temporary-phone-number.com";
        }

        public override List<vMessage> Parse(string HTMLContent, string CurrentURL)
        {
            throw new System.NotImplementedException();
        }
    }
}
