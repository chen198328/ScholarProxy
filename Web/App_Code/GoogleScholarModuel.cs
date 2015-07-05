using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GoogleProxy;
using System.Drawing;
using System.IO;
namespace GoogleScholarWeb
{
    public class GoogleScholarModule : IHttpModule
    {
        HttpClientHelper clienthelper = new HttpClientHelper();
        void IHttpModule.Dispose()
        {
        }

        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            HttpApplication context = (HttpApplication)sender;
            string url = context.Request.Url.PathAndQuery;
            string remoteurl = "http://scholar.google.com" + url;
            if (remoteurl.EndsWith(".gif"))
            {
                Image image = Image.FromFile(context.Server.MapPath("~/Image/scholar_logo_lg_2011.gif"));
                MemoryStream ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                byte[] byData = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(byData, 0, byData.Length);
                ms.Close();
                context.Response.ContentType = "image/gif";
                context.Response.BinaryWrite(byData);
            }
            else if (remoteurl.EndsWith("favicon-png.ico"))
            {
                Image image = Image.FromFile(context.Server.MapPath("~/Image/favicon-png.ico"));
                MemoryStream ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] byData = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(byData, 0, byData.Length);
                ms.Close();
                context.Response.ContentType = "image/bmp";
                context.Response.BinaryWrite(byData);
            }
            else if (remoteurl.EndsWith("sprite.png"))
            {
                Image image = Image.FromFile(context.Server.MapPath("~/Image/sprite.png"));
                MemoryStream ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byData = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(byData, 0, byData.Length);
                ms.Close();
                context.Response.ContentType = "image/png";
                context.Response.BinaryWrite(byData);
            }

            else
            {
                string html = clienthelper.GetHtml(remoteurl);
                context.Response.Write(html);
            }
            context.Response.End();
        }
    }
}
