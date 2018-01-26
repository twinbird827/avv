using Avv.Apps.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Avv.Apps.Parameters
{
    public class HttpRequestConst : IniBase
    {
        /// <summary>
        /// ｺﾝﾌｨｸﾞﾌｧｲﾙのﾃﾞﾌｫﾙﾄｾｸｼｮﾝ
        /// </summary>
        private const string ConfigSection = "GENERAL";

        /// <summary>
        /// ｺﾝｽﾄﾗｸﾀ
        /// </summary>
        private HttpRequestConst() : base(Variables.HttpRequestConstPath, ConfigSection) { }

        /// <summary>
        /// ｺﾝﾌｨｸﾞﾌｧｲﾙにｱｸｾｽするためのｲﾝｽﾀﾝｽ
        /// </summary>
        private static HttpRequestConst Instance { get; set; } = new HttpRequestConst();

        /// <summary>
        /// ﾒｰﾙｱﾄﾞﾚｽ
        /// </summary>
        public static string Mail
        {
            get { return Instance["MAIL"]; }
            set { Instance["MAIL"] = value; }
        }

        /// <summary>
        /// ﾊﾟｽﾜｰﾄﾞ
        /// </summary>
        public static string Password
        {
            get
            {
                // 設定ﾌｧｲﾙに保存されているﾊﾟｽﾜｰﾄﾞを取得する。
                var tmp = Instance["PASSWORD"];

                // 設定されている場合、複合化して取得する。
                return string.IsNullOrWhiteSpace(tmp)
                    ? tmp
                    : Encrypter.DecryptString(tmp, Variables.ApplicationId);
            }
            set
            {
                // 指定した文字列を暗号化して保存する。
                Instance["PASSWORD"] = Encrypter.EncryptString(value, Variables.ApplicationId);
            }
        }

        /// <summary>
        /// ﾛｸﾞｲﾝURL
        /// </summary>
        public static string LoginUrl { get { return Instance["LOGIN_URL"]; } }

        /// <summary>
        /// ﾛｸﾞｲﾝﾊﾟﾗﾒｰﾀ
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string LoginParameter(string mail, string password)
        {
            return String.Format(Instance["LOGIN_PARAMETER"],
                HttpUtility.UrlEncode(mail),
                HttpUtility.UrlEncode(password)
            );
        }

        /// <summary>
        /// ﾀｲﾑｱｳﾄ時間(ﾐﾘ秒)
        /// </summary>
        public static int Timeout { get { return Int32.Parse(Instance["TIMEOUT"]); } }

        /// <summary>
        /// ﾕｰｻﾞｴｰｼﾞｪﾝﾄ
        /// </summary>
        public static string UserAgent { get { return Instance["USER_AGENT"]; } }

        /// <summary>
        /// ﾘﾌｧﾗ
        /// </summary>
        public static string Referer { get { return Instance["REFERER"]; } }

        /// <summary>
        /// ｸｯｷｰURL
        /// </summary>
        public static string CookieUrl { get { return Instance["COOKIE_URL"]; } }

        /// <summary>
        /// ｸｯｷｰﾃﾞｰﾀ
        /// </summary>
        public static string CookieData { get { return Instance["COOKIE_DATA"]; } }

        /// <summary>
        /// ﾏｲﾘｽﾄURL
        /// </summary>
        public static string MylistUrl { get { return Instance["MYLIST_URL"]; } }

        /// <summary>
        /// ﾏｲﾘｽﾄのﾃﾞﾌｫﾙﾄ
        /// </summary>
        public static string MylistDefault { get { return Instance["MYLIST_DEFAULT"]; } }
    }
}
