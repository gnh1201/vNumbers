namespace vNumbers.Model
{
    public class vIncoming
    {
        public vIncoming(IncomingBase provider)
        {
            Provider = provider;
            NoScript = false;
        }
        public IncomingBase Provider { get; set; }
        public bool NoScript { get; set; }
    }
}
