// getfreesmsnumber.com

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using vNumbers.Model;

namespace vNumbers.Incoming
{
    public class GetFreeSMSNumber : IncomingBase
    {
        public GetFreeSMSNumber() {
            Domain = "getfreesmsnumber.com";
        }

        public override List<vMessage> Parse(string HTMLContent, string CurrentURL)
        {
            List<vMessage> messages = new List<vMessage>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLContent);

            HtmlNode body = doc.DocumentNode.SelectSingleNode("//body");
            HtmlNode head = doc.DocumentNode.SelectSingleNode("//head");

            string text_status = body.SelectSingleNode("//div[@class='alert alert-success font-weight-bold']").InnerText.Trim();
            string text_title = head.SelectSingleNode("title").InnerText;

            string domain = Domain;
            string receiver = text_status.Substring(9, text_status.LastIndexOf(" is") - 9);
            string country = text_title.Substring(15, text_title.LastIndexOf(" mobile") - 15);
            string carrier = "Unknown";

            HtmlNodeCollection rows = body.SelectNodes("//div[@class='card m-2 text-center']");
            foreach (HtmlNode row in rows)
            {
                string timestamp = row.SelectSingleNode("div[@class='card-body']/footer").InnerText.Trim();
                string sender = row.SelectSingleNode("div[@class='card-header']/span/a").InnerText;
                string text = row.SelectSingleNode("div[@class='card-body']").InnerHtml;
                text = text.Substring(0, text.LastIndexOf("<div")).Trim();

                // parse timestamp
                TimeSpan ts = new TimeSpan();
                DateTime dt;
                string[] _timestamp = timestamp.Split(' ');
                if (_timestamp.Length == 3 && _timestamp[2] == "ago")
                {
                    int h = 0, i = 0, s = 0;

                    if (int.TryParse(_timestamp[0], out int t))
                    {
                        switch (_timestamp[1])
                        {
                            case "second": case "seconds": s = t; break;    // example: 2 seconds ago
                            case "minute": case "minutes": i = t; break;    // example: 2 minutes ago
                            case "hour": case "hours": h = t; break;        // example: 2 hours ago
                        }

                        ts = new TimeSpan(h, i, s);
                    }
                }
                dt = DateTime.Now - ts;

                // insert message to the database
                messages.Add(new vMessage
                {
                    Carrier = carrier,
                    Country = country,
                    Domain = domain,
                    Sender = sender,
                    Receiver = receiver,
                    Text = text,
                    ReceivedDateTime = dt,
                    ConfirmedDateTime = DateTime.Now
                }.ComputeHash());
            }

            return messages;
        }
    }
}
