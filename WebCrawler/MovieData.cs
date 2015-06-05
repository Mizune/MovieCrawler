using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    public class MovieData
    {
        private string Title;
        private string Url;


        public void SetTitle(string Title)
        {
            this.Title = Title;
        }

        public void SetUrl(string Url)
        {
            this.Url = Url;
        }

        public string GetTitle()
        {
            return this.Title;
        }

        public string GetUrl()
        {
            return this.Url;
        }

        public int Show()
        {
            Console.WriteLine("Title : {0}", this.Title);
            Console.WriteLine("Url : {0}", this.Url);

            return 0;
        }
    }

}
