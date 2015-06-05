using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    public class Parser
    {
        private HtmlDocument HTMLDoc;
        private string RawHTML;
        private List<string> LinkLists;
        private bool hasHTML;
        //private string RootUrl = "https://www.youtube.com";
        private List<string> RootURL;
        private DataWrapper Datas;

        public Parser()
        {
            Initializer();
        }

        public Parser(string html)
        {
            Initializer();
            SetHTML(html);
        }

        public int Initializer()
        {
            HTMLDoc = new HtmlAgilityPack.HtmlDocument();
            hasHTML = false;
            Datas = new DataWrapper();
            RootURL = new List<string>();
            RootURL.Add("https://www.youtube.com");
            RootURL.Add("http://www.nicovideo.jp");
            return 0;
        }

        public int SetHTML(string html)
        {
            this.RawHTML = html;
            hasHTML = true;
            return 0;
        }

        public int StartParsing(int flag)
        {
            if (!hasHTML)
            {
                Console.WriteLine("Can not Parse. Please set your HTML.");
                return -1;
            }
            HTMLDoc.LoadHtml(RawHTML);

            switch (flag)
            {
                case 0:
                    YoutubeParsing();
                    break;
                case 1:
                    NicovideoParsing();
                    break;
                default:
                    break;
            }

            return 0;
        }


        public int YoutubeParsing()
        {

            var articles = HTMLDoc.DocumentNode
            //.SelectNodes(@"//h3[@class=""yt-lookup-title""]/a")
            .SelectNodes(@"//h3/a")
            .Select(a => new
            {
                Url = a.Attributes["href"].Value.Trim(),
                Title = a.Attributes["title"].Value.Trim(),
            });
            // Youtubeの構造を見てParsingする

            Console.WriteLine("全{0}記事", articles.Count());
            foreach (var a in articles)
            {
                Console.WriteLine(a.Title);
                Console.WriteLine(" {0}{1}", RootURL[0], a.Url);
                MovieData Data = new MovieData();
                Data.SetTitle(a.Title);
                Data.SetUrl(RootURL[0] + a.Url);
                Datas.SetData(Data);
            }
            return 0;
        }

        public int NicovideoParsing()
        {
            var articles = HTMLDoc.DocumentNode
            .SelectNodes(@"//p[@class=""itemTitle""]/a")
            .Select(a => new
            {
                Url = a.Attributes["href"].Value.Trim(),
                Title = a.Attributes["title"].Value.Trim(),
            });
            // Nicoの構造を見てParsingする

            Console.WriteLine("全{0}記事", articles.Count());
            foreach (var a in articles)
            {
                Console.WriteLine(a.Title);
                Console.WriteLine(" {0}{1}", RootURL[0], a.Url);
                MovieData Data = new MovieData();
                Data.SetTitle(a.Title);
                Data.SetUrl(RootURL[0] + a.Url);
                Datas.SetData(Data);
            }
            return 0;
        }


        public DataWrapper GetDatas()
        {
            return this.Datas;
        }

    }
}
