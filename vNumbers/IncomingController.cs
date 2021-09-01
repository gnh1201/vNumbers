using CefSharp;
using CefSharp.OffScreen;
using LiteDB;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using vNumbers.Incoming;
using vNumbers.Model;

namespace vNumbers
{
    public class IncomingController
    {
        private List<string> IncomingURLs;
        private List<vIncoming> Incomings;
        private Dictionary<ChromiumWebBrowser, bool> Browsers;
        private LiteDatabase DataBase;
        private ILiteCollection<vMessage> MessageCollection;
        private int Concurrent = 5;
        private int Counter = 0;

        public IncomingController()
        {
            IncomingURLs = new List<string>();
            Incomings = new List<vIncoming>
            {
                new vIncoming(new Quackr()),
                new vIncoming(new GetFreeSMSNumber()),
                new vIncoming(new ReceiveSMS1())
            };
            Browsers = new Dictionary<ChromiumWebBrowser, bool>();

            for (int i = 0; i < Concurrent; i++)
            {
                ChromiumWebBrowser browser = new ChromiumWebBrowser();
                browser.LoadingStateChanged += OnLoad;
                Browsers.Add(browser, false);
            }

            DataBase = new LiteDatabase(@"Data\data.db");
            MessageCollection = DataBase.GetCollection<vMessage>("messages");
            MessageCollection.EnsureIndex(x => x.Hash);

            FetchURLs();
        }

        public void FetchURLs() {
            string line;
            StreamReader file = new StreamReader(@"Data\urls.txt");
            while ((line = file.ReadLine()) != null)
            {
                IncomingURLs.Add(line);
            }
        }

        public void Parse(string HTMLContent, string CurrentURL)
        {
            ILiteCollection<vMessage> col = DataBase.GetCollection<vMessage>("messages");

            foreach (vIncoming v in Incomings)
            {
                if (CurrentURL.IndexOf(":/" + v.Provider.Domain) > -1)
                {
                    col.InsertBulk(v.Provider.Parse(HTMLContent, CurrentURL));
                }
            }
        }

        public async void OnLoad(object sender, LoadingStateChangedEventArgs args)
        {
            bool IsLoading = args.IsLoading;

            ChromiumWebBrowser browser = (ChromiumWebBrowser)args.Browser;
            Browsers[browser] = IsLoading;

            // Wait for the Page to finish loading
            if (!IsLoading)
            {
                Thread.Sleep(1000);
                Parse(await browser.GetBrowser().MainFrame.GetSourceAsync(), browser.Address);
                Counter++;

                // Reset counter to 0
                if (Counter >= IncomingURLs.Count)
                {
                    Counter = 0;
                }
            }
        }

        public void DoWork()
        {
            while (true)
            {
                Thread.Sleep(1);
                foreach (KeyValuePair<ChromiumWebBrowser, bool> entry in Browsers) {
                    if (entry.Value == false)
                    {
                        ChromiumWebBrowser browser = entry.Key;
                        browser.Load(IncomingURLs[Counter]);
                    }
                }
            }
        }
    }
}
