using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    class Crawler
    {

        //nico動画対応させる(基本Getベース URLをを指定できるように作り変える)
        // 最終的にcsvとかリストで流したいクエリの集合を作っておいて走らせるとすべてを回してすべて保存するように
        private WebRequest Request;
        private string RequestUrl;
        private const string QueryHeader = "?search_query=";
        private string CurrentQuery = "";
        private bool hasQuery = false;
        byte[] ParamsByte;
        private string ResponseHTML;
        private int PageCount;


        public Crawler()
        {
            Initializer();
        }

        public Crawler(string Query)
        {
            SetQuery(Query);
            Initializer();
        }

        public void Initializer()
        {
            PageCount = 1;
            ResponseHTML = "";
            if (!hasQuery)
            {
                Console.WriteLine("Error, please again after set query.");
            }
            else
            {
                WebRequestInitializer("https://www.youtube.com/results" + QueryHeader + CurrentQuery);

            }
        }

        public void WebRequestInitializer(string url)
        {
            Request = WebRequest.Create(url);
            ParamsByte = null;
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.Method = "POST";
        }


        public string ReplaceQuery(string Query)
        {
            // 呼び出されたら空白を+に置き換える
            return Query.Replace(" ", "+");
        }


        public void SetQuery(string Query)
        {
            //Queryに空白が含まれている場合splitするように
            if (Query.Contains(" "))
            {
                CurrentQuery = ReplaceQuery(Query);
            }
            else
            {
                CurrentQuery = Query;
            }

            hasQuery = true;
        }

        public string GetQuery()
        {
            if (!hasQuery)
            {
                return null; // 失敗を返す
            }
            return null;
        }

        public int StartCrawler(int flag)
        {
            if (!hasQuery)
            {
                Console.WriteLine("Failed... Please set Query using by \"SetQuery\" method. ");
                // 場合によってはここでクエリ指定させる？
                return -1;
            }

            switch (flag)
            {
                case 0:
                    YoutubeCrawler();
                    break;
                case 1:
                    NicovideoCrawler();
                    break;
                default:
                    break;
            }

            return 0;
        }

        public void YoutubeCrawler()
        {
            for (int i = 1; i < PageCount + 1; i++)
            {
                WebRequestInitializer("https://www.youtube.com/results" + QueryHeader + CurrentQuery + "&page=" + i);
                ParamsByte = Encoding.UTF8.GetBytes(QueryHeader + CurrentQuery);
                Request.ContentLength = ParamsByte.Length;
                //Requestにパラメータ分のLengthを設定
                //Console.WriteLine(ParamsByte.ToString());
                Stream DataStream = Request.GetRequestStream();
                DataStream.Write(ParamsByte, 0, ParamsByte.Length);
                DataStream.Close();
                WebResponse response = Request.GetResponse();
                Console.WriteLine("Section result is ..." + ((HttpWebResponse)response).StatusDescription);
                DataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(DataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                ResponseHTML += responseFromServer;
                // Display the content.
                //Console.WriteLine(responseFromServer);
                // Clean up the streams.
                reader.Close();
                DataStream.Close();
                response.Close();
            }
        }

        public void NicovideoCrawler()
        {
            for (int i = 1; i < PageCount + 1; i++)
            {
                WebRequestInitializer("http://www.nicovideo.jp/search/" + CurrentQuery + "?page=" + i);
                Console.WriteLine("http://www.nicovideo.jp/search/" + CurrentQuery + "?page=" + i);

                ParamsByte = Encoding.UTF8.GetBytes(QueryHeader + CurrentQuery);
                Request.ContentLength = ParamsByte.Length;
                //Requestにパラメータ分のLengthを設定
                //Console.WriteLine(ParamsByte.ToString());
                Stream DataStream = Request.GetRequestStream();
                DataStream.Write(ParamsByte, 0, ParamsByte.Length);
                DataStream.Close();
                WebResponse response = Request.GetResponse();
                Console.WriteLine("Section result is ..." + ((HttpWebResponse)response).StatusDescription);
                DataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(DataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                ResponseHTML += responseFromServer;
                // Display the content.
                //Console.WriteLine(responseFromServer);
                // Clean up the streams.
                reader.Close();
                DataStream.Close();
                response.Close();
            }
        }

        public void StartCrawler(int flag, int count)
        {
            PageCount = count;
            StartCrawler(flag);
        }

        public string GetResult()
        {
            return ResponseHTML;
            // HTMLをStringで返す
            // Parserクラスを作る
            // XML , Json, CSVなどのファイルを書き出すorサーバに上げる
        }
    }
}
