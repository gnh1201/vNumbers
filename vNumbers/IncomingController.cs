using CefSharp;
using CefSharp.WinForms;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using vNumbers.Incoming;
using vNumbers.Model;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
using System.Diagnostics;

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
                new vIncoming(new Quackr()),
                new vIncoming(new GetFreeSMSNumber())
            };
            Browsers = new Dictionary<ChromiumWebBrowser, bool>();

            CefSettings cefSettings = new CefSettings() {
                UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/11.1.2 Safari/605.1.15",
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"CefSharp\Cache")
            };
            Cef.Initialize(cefSettings);

            for (int i = 0; i < Concurrent; i++)
            {
                ChromiumWebBrowser browser= new ChromiumWebBrowser();
                browser.LoadingStateChanged += OnLoadingStateChanged;
                //browser.Size = new Size(1024, 786);
                Browsers.Add(browser, false);

                Form form = new Form
                {
                    Text = "Preview",
                    Size = new Size(1024, 786)
                };
                browser.Dock = DockStyle.Fill;
                form.Controls.Add(browser);
                form.Show();
            }

            DataBase = new LiteDatabase(@"data.db");
            MessageCollection = DataBase.GetCollection<vMessage>("messages");
            MessageCollection.EnsureIndex(x => x.Hash);

            FetchURLs();
        }
        public void Preview()
        {

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
                        Console.WriteLine("[Exception] " + e.Message);
                    }

                    break;
                }
            }
        }

        public void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            bool IsLoading = args.IsLoading;

            ChromiumWebBrowser browser = (ChromiumWebBrowser)sender;
            Console.WriteLine(browser.Address);

            // Wait for the Page to finish loading
            if (!IsLoading)
            {
                Task.Run(async () =>
                {
                    // Get page source
                    Thread.Sleep(3000);
                    string source = await browser.GetMainFrame().GetSourceAsync();
                    Parse(source, browser.Address);

                    // Set loading status
                    Browsers[browser] = IsLoading;
                });
            }
        }

        public void DoWork()
        {
            while (true)
            {
                foreach (ChromiumWebBrowser browser in Browsers.Keys.ToList()) {
                    Thread.Sleep(300);

                    bool isLoading = Browsers[browser];
                    if (!isLoading)
                    {
                        string incomingURL = IncomingURLs[Counter];
                        Browsers[browser] = true;
                        browser.Load(incomingURL);
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
