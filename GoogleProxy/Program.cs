using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace GoogleProxy
{
    class Program
    {
        static void Main(string[] args)
        {
      
            string[] keywords = new string[] { "keyword", "application", "computer", "nature", "sciecne" };
            Random random = new Random();
            HttpClientHelper helper = new HttpClientHelper();
            int count = 0;
            while (true)
            {
                string url = "http://scholar.google.com/scholar?q={0}&hl=zh-CN&as_sdt=0,5";
                count++;
                string keyword = keywords[count % keywords.Length];
                int interval = random.Next(1000, 5000);
                Thread.Sleep(interval);
                try
                {
                    url = string.Format(url, keyword);
                    string html = helper.GetHtml(url);
                    if (html != null && html.Contains("<b>"+keyword))
                    {
                        Console.WriteLine(DateTime.Now + ":" + true+ " "+count);
                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now + ":" + false);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(DateTime.Now + ":" + ex.Message);
                }

            }
            Console.Read();
        }
    }
}
