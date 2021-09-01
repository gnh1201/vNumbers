// www.bfkdim.com

using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class Bfkdim : IncomingBase
    {
        public Bfkdim()
        {
            Domain = "www.bfkdim.com";
        }

        public override List<vMessage> Parse(string HTMLContent, string CurrentURL)
        {
            throw new System.NotImplementedException();
        }
    }
}
