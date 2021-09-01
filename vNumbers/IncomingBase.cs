using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers
{
    public abstract class IncomingBase
    {
        public string Domain { get; set; }
        public abstract List<vMessage> Parse(string HTMLContent, string URL);
    }
}
