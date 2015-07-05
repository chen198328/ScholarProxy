using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
namespace GoogleProxy
{
    public class NCookieManager
    {
        private List<NCookie> NCookies { set; get; }
        /// <summary>
        /// 请求阈值
        /// </summary>
        private int MaxRequestCount { set; get; }
        private int TotalRequestCount { set; get; }
        /// <summary>
        /// 第一次初始化会自动请求添加
        /// </summary>
        public NCookieManager()
        {
            MaxRequestCount = 10;
            NCookies = new List<NCookie>();
            Request();
        }
        static object obj = new object();
        private void Request()
        {
            NCookie ncookie = new NCookie();
            ncookie.Refere = "http://scholar.google.com";
            ncookie.LastestRequestTime = DateTime.Now;

            CookieContainer cookiecontainer = new CookieContainer();
            using (HttpClientHandler httpclienthandler = new HttpClientHandler() { CookieContainer = cookiecontainer })
            using (HttpClient httpclient = new HttpClient(httpclienthandler))
            {
                httpclient.DefaultRequestHeaders.Add("Accept", "text/html, application/xhtml+xml, */*");
                httpclient.DefaultRequestHeaders.Add("Accept-Language", "zh-Hans-CN,zh-Hans;q=0.8,en-US;q=0.5,en;q=0.3");
                httpclient.DefaultRequestHeaders.Add("User-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
                httpclient.DefaultRequestHeaders.Add("Host", "scholar.google.com");
                httpclient.DefaultRequestHeaders.Add("DNT", "1");
                httpclient.DefaultRequestHeaders.Add("Connection", "Keep-Alive");

                string url = "http://scholar.google.com";
                HttpResponseMessage response = httpclient.GetAsync(url).Result;

                Uri uri = new Uri(url);
                var cookies = cookiecontainer.GetCookies(uri).Cast<Cookie>();
                foreach (var cookie in cookies)
                {
                    ncookie.Cookies.Add(cookie.Name, cookie.Value);
                }
            }
            lock (obj)
            {
                NCookies.Add(ncookie);
            }
        }
        private async void RequestAsync()
        {
            NCookie ncookie = new NCookie();
            ncookie.Refere = "http://scholar.google.com";
            ncookie.LastestRequestTime = DateTime.Now;

            CookieContainer cookiecontainer = new CookieContainer();
            using (HttpClientHandler httpclienthandler = new HttpClientHandler() { CookieContainer = cookiecontainer })
            using (HttpClient httpclient = new HttpClient(httpclienthandler))
            {
                httpclient.DefaultRequestHeaders.Add("Accept", "text/html, application/xhtml+xml, */*");
                httpclient.DefaultRequestHeaders.Add("Accept-Language", "zh-Hans-CN,zh-Hans;q=0.8,en-US;q=0.5,en;q=0.3");
                httpclient.DefaultRequestHeaders.Add("User-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
                httpclient.DefaultRequestHeaders.Add("Host", "scholar.google.com");
                httpclient.DefaultRequestHeaders.Add("DNT", "1");
                httpclient.DefaultRequestHeaders.Add("Connection", "Keep-Alive");

                string url = "http://scholar.google.com";
                HttpResponseMessage response = await httpclient.GetAsync(url);

                Uri uri = new Uri(url);
                var cookies = cookiecontainer.GetCookies(uri).Cast<Cookie>();
                foreach (var cookie in cookies)
                {
                    ncookie.Cookies.Add(cookie.Name, cookie.Value);
                }
            }
            lock (obj)
            {
                NCookies.Add(ncookie);
            }
        }
        /// <summary>
        /// 清楚掉过时的NCookie
        /// </summary>
        private void RemoveExpire()
        {
            lock (obj)
            {
                NCookies.RemoveAll(x => x.ExpireTime < DateTime.Now);
            }
        }
        public NCookie Get()
        {
            //RemoveExpire();
            NCookie cookie = NCookies[0];
            cookie.LastestRequestTime = DateTime.Now;
            NCookies = NCookies.OrderBy(x => x.LastestRequestTime).ToList<NCookie>();
            TotalRequestCount++;
            if (TotalRequestCount <= MaxRequestCount || (TotalRequestCount > MaxRequestCount && TotalRequestCount / MaxRequestCount == 0))
            {
                RequestAsync();
            }
            return cookie;
        }
    }
}
