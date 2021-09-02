using System;

namespace vNumbers.Model
{
    public class vFile
    {
        public byte[] Data { get; set; }
        public string Name { get; set; }
        public string SourceURI { get; set; }
        public DateTime ReceivedDateTime { get; set; }
        public DateTime ConfirmedDateTime { get; set; }
    }
}
