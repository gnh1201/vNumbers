using CefSharp.OffScreen;
using LiteDB;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using vNumbers.Incoming;
using vNumbers.Model;

namespace vNumbers
{
    public class IncomingController
    {
        private List<string> IncomingURLs;
        private List<vIncoming> Incomings;
        private ChromiumWebBrowser Browser;
        private LiteDatabase DataBase;
        private ILiteCollection<vMessage> MessageCollection;

        public IncomingController()
        {
            IncomingURLs = new List<string>();
            Incomings = new List<vIncoming>
            {
                new vIncoming(new Quackr()),
                new vIncoming(new GetFreeSMSNumber()),
                new vIncoming(new ReceiveSMS1())
            };
            Browser = new ChromiumWebBrowser();
            DataBase = new LiteDatabase(@"Data\data.db");
            MessageCollection = DataBase.GetCollection<vMessage>("messages");
            MessageCollection.EnsureIndex(x => x.Hash);
        }

        public void FetchURLs() {
            string line;
            StreamReader file = new StreamReader(@"Data\urls.txt");
            while ((line = file.ReadLine()) != null)
            {
                IncomingURLs.Add(line);
            }
        }

        public void Parse(string HTMLContent, string URL)
        {
            List<vMessage> messsages;
            ILiteCollection<vMessage> col = DataBase.GetCollection<vMessage>("messages");

            foreach (vIncoming v in Incomings)
            {
                if (URL.IndexOf(":/" + v.Provider.Domain) > -1)
                {
                    messsages = v.Provider.Parse(HTMLContent, URL);
                    _ = col.InsertBulk(messsages);
                }
            }
        }

        public void DoWork()
        {
            int counter = 0;

            while (true)
            {
                string URL = IncomingURLs[counter];
                Browser.Load(URL);

                _ = Task.Run(async () =>
                  {
                      Thread.Sleep(1000);
                      Parse(await Browser.GetBrowser().MainFrame.GetSourceAsync(), URL);
                      counter++;

                      if (counter >= IncomingURLs.Count)
                      {
                          counter = 0;
                      }
                  });
            }
        }
    }
}
