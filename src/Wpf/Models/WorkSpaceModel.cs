using Avv.Apps.Commons;
using Avv.Apps.Parameters;
using StatefulModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Wpf.Models
{
    public abstract class WorkSpaceModel : BindableBase
    {
        /// <summary>
        /// ｱｲﾃﾑ構成
        /// </summary>
        public ObservableSynchronizedCollection<VideoModel> Items
        {
            get { return _Items; }
            set { SetProperty(ref _Items, value); }
        }
        private ObservableSynchronizedCollection<VideoModel> _Items;

        /// <summary>
        /// Httpﾒｿｯﾄﾞ
        /// </summary>
        protected virtual string Method { get { return "GET"; } }

        /// <summary>
        /// Httpｺﾝﾃﾝﾂﾀｲﾌﾟ
        /// </summary>
        protected virtual string ContentType { get { return null; } }

        /// <summary>
        /// ｱｲﾃﾑ構成を再読み込みします。
        /// </summary>
        public abstract void Reload();

        /// <summary>
        /// 指定したUrlのHtmlﾃｷｽﾄを取得します。
        /// </summary>
        /// <param name="url">Url</param>
        /// <returns>Htmlﾃｷｽﾄ</returns>
        protected string GetSmileVideoHtmlText(string url)
        {
            LoginModel.Instance.Login();

            var req = GetRequest(url);                  // ﾘｸｴｽﾄ作成
            var res = GetResponse(req);                 // ﾚｽﾎﾟﾝｽ取得
            var txt = HttpUtil.GetResponseString(res);  // ﾚｽﾎﾟﾝｽからHttpText取得

            // 制御文字を除外する
            var excludes = Enumerable.Range(0, 31).Where(i => i != 10);
            Array.ForEach(excludes.ToArray(), i => txt = txt.Replace(((char)i).ToString(), ""));

            return txt;
        }

        /// <summary>
        /// 指定したUrlとﾊﾟﾗﾒｰﾀでHTTPﾘｸｴｽﾄを取得します。
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="parameter">ﾊﾟﾗﾒｰﾀ</param>
        /// <returns><code>HttpWebRequest</code></returns>
        private HttpWebRequest GetRequest(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = Method;
            httpWebRequest.ContentType = ContentType;

            httpWebRequest.Timeout = HttpRequestConst.Timeout;
            httpWebRequest.ReadWriteTimeout = HttpRequestConst.Timeout;
            httpWebRequest.UserAgent = HttpRequestConst.UserAgent;
            httpWebRequest.Referer = HttpRequestConst.Referer;

            return httpWebRequest;
        }

        /// <summary>
        /// HTTPﾚｽﾎﾟﾝｽを取得します。
        /// </summary>
        /// <param name="httpWebRequest">HTTPﾘｸｴｽﾄ</param>
        /// <returns></returns>
        private HttpWebResponse GetResponse(HttpWebRequest httpWebRequest)
        {
            HttpWebResponse httpWebResponse;
            if (LoginModel.Instance.Cookie != null)
            {
                // ﾛｸﾞｲﾝ情報を持つｸｯｷｰをｺﾝﾃﾅに追加する。
                httpWebRequest.CookieContainer = new CookieContainer();

                Array.ForEach(LoginModel.Instance.Cookie.GetCookies(httpWebRequest.RequestUri).OfType<Cookie>().ToArray(),
                    cookie =>
                    {
                        if (cookie.Name.ToLower() == "col")
                        {
                            cookie.Value = "1";
                        }
                        httpWebRequest.CookieContainer.Add(cookie);
                    }
                );
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Array.ForEach(httpWebRequest.CookieContainer.GetCookies(httpWebRequest.RequestUri).OfType<Cookie>().ToArray(),
                    cookie =>
                    {
                        if (cookie.Name.ToLower() == "col")
                        {
                            cookie.Value = "1";
                        }
                        HttpUtil.InternetSetCookie(cookie);
                    }
                );
            }
            else
            {
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            }

            return httpWebResponse;
        }
    }
}
