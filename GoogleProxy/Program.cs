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
            string url = "http://scholar.google.com/scholar?q=history&hl=zh-CN&as_sdt=0,5";
            Random random = new Random();
            HttpClientHelper helper = new HttpClientHelper();
            while (true)
            {
                int interval = random.Next(1000, 5000);
                Thread.Sleep(interval);
                try
                {
                    string html = helper.GetHtml(url);
                    if (html != null && html.Contains("Structure and change in economic"))
                    {
                        Console.WriteLine(DateTime.Now + ":" + true);
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
