// quackr.io

using System.Collections.Generic;
using vNumbers.Model;
using HtmlAgilityPack;
using System;

namespace vNumbers.Incoming
{
    public class Quackr : IncomingBase
    {
        public Quackr()
        {
            Domain = "quackr.io";
        }

        public override List<vMessage> Parse(string HTMLContent, string CurrentURL)
        {
            List<vMessage> messages = new List<vMessage>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLContent);

            HtmlNode body = doc.DocumentNode.SelectSingleNode("body");
            HtmlNode tableBody = body.SelectSingleNode("table[@class='is-striped']/tbody");
            HtmlNodeCollection rows = tableBody.SelectNodes("tr");
            foreach(HtmlNode row in rows)
            {
                HtmlNodeCollection cols = row.SelectNodes("td");

                // get informations from table
                string timestamp = cols[0].InnerText;
                string sender = cols[1].InnerText;
                string text = cols[2].InnerText;

                // parse timestamp
                TimeSpan ts = new TimeSpan();
                DateTime dt;
                string[] _timestamp = timestamp.Split(' ');
                if (_timestamp.Length == 3 && _timestamp[2] == "ago") {
                    int h = 0, i = 0, s = 0;

                    if (int.TryParse(_timestamp[0], out int t))
                    {
                        switch (_timestamp[2])
                        {
                            case "second": case "seconds": s = t;  break;    // example: 2 seconds ago
                            case "minute": case "minutes": i = t; break;     // example: 2 minutes ago
                            case "hour": case "hours": h = t; break;         // example: 2 hours ago
                        }

                        ts = new TimeSpan(h, i, s);
                    }
                }
                dt = DateTime.Now - ts;

                // get informations from other variables
                string domain = Domain;
                string receiver = CurrentURL.Substring(CurrentURL.LastIndexOf('/') + 1);
                string country = body.SelectSingleNode("div[@id='wrapper']/div/main/messages/section/div/div/div/h1/span").InnerText;
                string carrier = "Unknown";

                // insert message to the database
                messages.Add(new vMessage
                {
                    Carrier = carrier,
                    Country = country,
                    Domain = domain,
                    Sender = sender,
                    Receiver = receiver,
                    Text = text,
                    ReceivedDateTtime = dt,
                    ConfirmedDateTime = DateTime.Now
                });
            }

            return messages;
        }
    }
}
