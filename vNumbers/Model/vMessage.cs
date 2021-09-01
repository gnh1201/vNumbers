using LiteDB;
using System;
using System.Security.Cryptography;
using System.Text;

namespace vNumbers.Model
{
    public class vMessage
    {
        public ObjectId Id { get; set; }
        public string Carrier { get; set; }
        public string Country { get; set; }
        public string Domain { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Text { get; set; }
        public string Hash { get; set; }
        public DateTime ReceivedDateTtime { get; set; }
        public DateTime ConfirmedDateTime { get; set; }
        public string GetHash()
        {
            return Hash;
        }
        public vMessage ComputeHash()
        {
            // https://stackoverflow.com/questions/17292366/hashing-with-sha1-algorithm-in-c-sharp
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(Domain + Country + Sender + Receiver + Text));
                StringBuilder sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    _ = sb.Append(b.ToString("X2"));
                }

                Hash = sb.ToString();

                return this;
            }
        }
    }
}
