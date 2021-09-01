using CefSharp;
using CefSharp.OffScreen;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using vNumbers.Incoming;
using vNumbers.Model;
using System.Linq;

namespace vNumbers
{
    public class IncomingController
    {
        private List<string> IncomingURLs;
        private List<vIncoming> Incomings;
        private Dictionary<ChromiumWebBrowser, bool> Browsers;
        private LiteDatabase DataBase;
        private ILiteCollection<vMessage> MessageCollection;
        private int Concurrent = 3;
        private int Counter = 0;

        public IncomingController()
        {
            IncomingURLs = new List<string>();
            Incomings = new List<vIncoming>
            {
                new vIncoming(new Quackr())
            };
            Browsers = new Dictionary<ChromiumWebBrowser, bool>();

            for (int i = 0; i < Concurrent; i++)
            {
                ChromiumWebBrowser browser = new ChromiumWebBrowser();
                browser.LoadingStateChanged += OnLoadingStateChanged;
                Browsers.Add(browser, false);
            }

            DataBase = new LiteDatabase(@"data.db");
            MessageCollection = DataBase.GetCollection<vMessage>("messages");
            MessageCollection.EnsureIndex(x => x.Hash);

            FetchURLs();
        }

        public void FetchURLs() {
            string line;
            StreamReader file = new StreamReader(@"urls.txt");
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
                if (CurrentURL.IndexOf("://" + v.Provider.Domain) > -1)
                {
                    try
                    {
                        List<vMessage> messages = v.Provider.Parse(HTMLContent, CurrentURL);
                        foreach (vMessage message in messages)
                        {
                            Console.WriteLine("[" + message.Sender + " -> " + message.Receiver + "] " + message.Text);
                        }
                        col.InsertBulk(messages);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        public async void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            bool IsLoading = args.IsLoading;

            ChromiumWebBrowser browser = (ChromiumWebBrowser)sender;
            Console.WriteLine(browser.Address);

            // Wait for the Page to finish loading
            if (!IsLoading)
            {
                // Get page source
                Thread.Sleep(5000);
                string source = await browser.GetBrowser().MainFrame.GetSourceAsync();
                Parse(source, browser.Address);

                // Set loading status
                Browsers[browser] = IsLoading;
            }
        }

        public void DoWork()
        {
            while (true)
            {
                foreach (ChromiumWebBrowser browser in Browsers.Keys.ToList()) {
                    Thread.Sleep(1);

                    bool isLoading = Browsers[browser];
                    if (!isLoading)
                    {
                        Browsers[browser] = true;
                        browser.Load(IncomingURLs[Counter]);
                        Counter++;

                        // Reset counter to 0
                        if (Counter >= IncomingURLs.Count)
                        {
                            Counter = 0;
                        }
                    }
                }
            }
        }
    }
}
