using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
namespace GoogleProxy
{
    public class HttpClientHelper
    {
        public NCookieManager cookieManager { set; get; }
        public HttpClientHelper()
        {
            cookieManager = new NCookieManager();
        }
        public string GetHtml(string url)
        {
            NCookie ncookie = cookieManager.Get();
            ncookie.Refere = url;
            CookieContainer cookiecontainer = new CookieContainer();
            foreach (var nc in ncookie.Cookies)
            {
                cookiecontainer.Add(new Cookie(nc.Key, nc.Value, "/", "scholar.google.com"));
            }
            using (HttpClientHandler handler = new HttpClientHandler() { CookieContainer = cookiecontainer, UseCookies = true })
            using (HttpClient httpclient = new HttpClient(handler))
            {
                httpclient.DefaultRequestHeaders.Add("Accept", "text/html, application/xhtml+xml, */*");
                httpclient.DefaultRequestHeaders.Add("Accept-Language", "zh-Hans-CN,zh-Hans;q=0.8,en-US;q=0.5,en;q=0.3");
                httpclient.DefaultRequestHeaders.Add("User-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
                httpclient.DefaultRequestHeaders.Add("Host", "scholar.google.com");
                httpclient.DefaultRequestHeaders.Add("DNT", "1");
                httpclient.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
                return httpclient.GetStringAsync(url).Result;
            }
        }
    }
}
