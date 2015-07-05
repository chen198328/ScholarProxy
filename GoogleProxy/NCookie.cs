using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace GoogleProxy
{
    public class NCookie
    {
        public Dictionary<string, string> Cookies { set; get; }
        public string Refere { set; get; }
        public DateTime LastestRequestTime { set; get; }
        public DateTime ExpireTime { set; get; }
        /// <summary>
        /// 请求总次数
        /// </summary>
        public int Count { set; get; }
        public NCookie()
        {
            Cookies = new Dictionary<string, string>();
        }
    }
}
