using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    public class DataWrapper
    {
        private List<MovieData> Datas;

        public DataWrapper()
        {
            this.Datas = new List<MovieData>();
        }

        public DataWrapper(List<MovieData> Datas)
        {
            this.Datas = Datas;
        }

        public List<MovieData> GetDatas()
        {
            return Datas;
        }

        public void SetDatas(List<MovieData> Datas)
        {
            this.Datas.AddRange(Datas);
        }

        public void SetDatas(DataWrapper Datas)
        {
            this.Datas.AddRange(Datas.GetDatas());
        }

        public void SetData(MovieData Data)
        {
            this.Datas.Add(Data);
        }

        public void ShowAll()
        {
            foreach (var res in this.Datas)
            {
                res.Show();
            }
        }
    }
}
