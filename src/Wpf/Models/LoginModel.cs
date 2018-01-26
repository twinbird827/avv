using Avv.Apps.Commons;
using Avv.Apps.Parameters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Wpf.Models
{
    public class LoginModel : BindableBase
    {
        /// <summary>
        /// ｼﾝｸﾞﾙﾄﾝﾊﾟﾀｰﾝのため、ﾌﾟﾗｲﾍﾞｰﾄｺﾝｽﾄﾗｸﾀ。
        /// </summary>
        private LoginModel()
        {

        }

        /// <summary>
        /// LoginModelｲﾝｽﾀﾝｽを取得します。
        /// </summary>
        public static LoginModel Instance { get; private set; } = new LoginModel();

        /// <summary>
        /// ﾛｸﾞｲﾝしているかどうか
        /// </summary>
        public bool IsLogin
        {
            get { return _IsLogin; }
            set { SetProperty(ref _IsLogin, value); }
        }
        private bool _IsLogin = false;

        /// <summary>
        /// ﾌﾟﾚﾐｱﾑかどうか
        /// </summary>
        public bool IsPremium
        {
            get { return _IsPremium; }
            set { SetProperty(ref _IsPremium, value); }
        }
        private bool _IsPremium = false;

        /// <summary>
        /// ｸｯｷｰｺﾝﾃﾅ
        /// </summary>
        public CookieContainer Cookie
        {
            get { return _Cookie; }
            set { SetProperty(ref _Cookie, value); }
        }
        private CookieContainer _Cookie = new CookieContainer();

        /// <summary>
        /// ｴﾗｰﾒｯｾｰｼﾞ
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }
        private string _Message = null;

        /// <summary>
        /// ﾌﾟﾛﾊﾟﾃｨを初期化します。
        /// </summary>
        public void Initialize()
        {
            IsLogin = false;
            IsPremium = false;
            Cookie = null;
            Cookie = new CookieContainer();
            Message = null;
        }

        /// <summary>
        /// 指定したUrlとﾊﾟﾗﾒｰﾀでHTTPﾘｸｴｽﾄを取得します。
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="parameter">ﾊﾟﾗﾒｰﾀ</param>
        /// <returns><code>HttpWebRequest</code></returns>
        private HttpWebRequest GetRequest(string mail, string password)
        {
            string url = HttpRequestConst.LoginUrl;
            string parameter = HttpRequestConst.LoginParameter(mail, password);

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            httpWebRequest.Timeout = HttpRequestConst.Timeout;
            httpWebRequest.ReadWriteTimeout = HttpRequestConst.Timeout;
            httpWebRequest.UserAgent = HttpRequestConst.UserAgent;
            httpWebRequest.Referer = HttpRequestConst.Referer;

            // ﾛｸﾞｲﾝ情報を持つｸｯｷｰをｺﾝﾃﾅに追加する。
            httpWebRequest.CookieContainer = new CookieContainer();
            httpWebRequest.CookieContainer.Add(Cookie.GetCookies(httpWebRequest.RequestUri));

            // ﾊﾟﾗﾒｰﾀが存在する場合、ｽﾄﾘｰﾑに追記する。
            if (!string.IsNullOrWhiteSpace(parameter))
            {
                byte[] bytes = Encoding.ASCII.GetBytes(parameter);
                httpWebRequest.ContentLength = bytes.LongLength;
                using (Stream stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, (int)bytes.LongLength);
                }
            }

            return httpWebRequest;
        }

        /// <summary>
        /// 規定のﾕｰｻﾞ、ﾊﾟｽﾜｰﾄﾞを用いて、ﾛｸﾞｲﾝ処理を実行します。
        /// </summary>
        /// <remarks>既にﾛｸﾞｲﾝ済みの場合は中断します。</remarks>
        public void Login()
        {
            if (IsLogin)
            {
                // ﾛｸﾞｲﾝ済みの場合は中断
                return;
            }

            // ﾛｸﾞｲﾝ処理
            Login(HttpRequestConst.Mail, HttpRequestConst.Password);
        }

        /// <summary>
        /// 指定したﾕｰｻﾞ、ﾊﾟｽﾜｰﾄﾞを用いて、ﾛｸﾞｲﾝ処理を実行します。
        /// </summary>
        /// <param name="mail">ﾒｰﾙｱﾄﾞﾚｽ</param>
        /// <param name="password">ﾊﾟｽﾜｰﾄﾞ</param>
        /// <remarks>既にﾛｸﾞｲﾝ済みの場合もﾛｸﾞｲﾝし直します。</remarks>
        public void Login(string mail, string password)
        {
            // ﾛｸﾞｲﾝﾃｽﾄ前にﾌﾟﾛﾊﾟﾃｨを初期化する。
            Initialize();

            if (string.IsNullOrWhiteSpace(mail) || string.IsNullOrWhiteSpace(password))
            {
                // ﾛｸﾞｲﾝ情報が指定されていない場合は中断
                Message = "ログイン情報が入力されていません。";
                return;
            }

            try
            {
                // ﾘｸｴｽﾄを取得する。
                var httpWebRequest = GetRequest(mail, password);
                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                // ﾘｸｴｽﾄからｸｯｷｰ取得
                CookieCollection cookies = httpWebRequest.CookieContainer.GetCookies(httpWebRequest.RequestUri);

                // TODO これいる？
                //IFormatter formatter = new BinaryFormatter();
                //using (Stream serializationStream = new FileStream(Variable.LOG_FOLDER + "\\Cookie.bin", FileMode.Create, FileAccess.Write, FileShare.None))
                //{
                //    formatter.Serialize(serializationStream, cookies);
                //}

                // 取得したｸｯｷｰをIEに流用
                HttpUtil.InternetSetCookie(cookies);

                // 自身のｸｯｷｰｺﾝﾃﾅに追加
                Cookie.Add(cookies);

                // ﾚｽﾎﾟﾝｽを用いてﾛｸﾞｲﾝ処理を実行する。
                if (LoginWithResponse(HttpUtil.GetResponseString(httpWebResponse)))
                {
                    // ﾛｸﾞｲﾝが成功したら、ﾛｸﾞｲﾝ情報を保存する。
                    HttpRequestConst.Mail = mail;
                    HttpRequestConst.Password = password;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());   // TODO
                Initialize();
                Message = "何らかの原因でエラーが発生しました。";
            }

        }

        /// <summary>
        /// ﾚｽﾎﾟﾝｽを用いてﾛｸﾞｲﾝ処理を実行します。
        /// </summary>
        /// <param name="expression">ﾚｽﾎﾟﾝｽﾃﾞｰﾀ</param>
        /// <returns>ﾛｸﾞｲﾝ成功：true / 失敗：false</returns>
        private bool LoginWithResponse(string expression)
        {
            if (expression.Contains("ログイン情報が間違っています"))
            {
                Message = "入力されたログイン情報が間違っています。";
            }
            else if (expression.Contains("closed=1&"))
            {
                Message = "入力されたアカウント情報が間違っています。";
            }
            else if (expression.Contains("error=access_locked"))
            {
                Message = "連続アクセス検出のためアカウントロック中\r\nしばらく時間を置いてから試行してください。";
            }
            else if (expression.Contains("is_premium=0&") || expression.Contains("is_premium=1&"))
            {
                // ﾛｸﾞｲﾝ成功
                // ﾛｸﾞｲﾝﾌﾗｸﾞを立てる。
                IsLogin = true;
                IsPremium = expression.Contains("is_premium=1&");
                return true;
            }
            else
            {
                Message = "何らかの原因でログインできませんでした\r\nしばらく時間を置いてから試行してください。";
            }

            return false;
        }

        /// <summary>
        /// ﾛｸﾞｱｳﾄします。
        /// </summary>
        public void Logout()
        {
            Initialize();
        }
    }
}
