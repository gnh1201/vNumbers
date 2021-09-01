namespace vNumbers.Model
{
    public class vIncoming
    {
        public vIncoming(IncomingBase provider)
        {
            Provider = provider;
        }
        public IncomingBase Provider { get; set; }
    }
}
