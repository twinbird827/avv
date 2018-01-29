using Avv.Apps.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Avv.Apps.Parameters
{
    public class Variables
    {
        [XmlIgnore()]
        public static Variables Instance { get; private set; } = new Variables();

        /// <summary>
        /// ｱﾌﾟﾘｹｰｼｮﾝ固有のId
        /// </summary>
        public string ApplicationId { get; set; } = "P&imsbQSWKKQ";

        /// <summary>
        /// 
        /// </summary>
        public string EncodingType {
            get { return _EncodingType; }
            set {
                _EncodingType = value;
                AppEncoding = Encoding.GetEncoding(value);
            }
        }
        private string _EncodingType = "UTF-8";

        /// <summary>
        /// ｱﾌﾟﾘｹｰｼｮﾝのｴﾝｺｰﾃﾞｨﾝｸﾞ
        /// </summary>
        [XmlIgnore()]
        public Encoding AppEncoding { get; private set; }

        /// <summary>
        /// HTTPﾘｸｴｽﾄ関連のﾊﾟﾗﾒｰﾀを格納した設定ﾌｧｲﾙのﾊﾟｽ
        /// </summary>
        public string HttpRequestConstPath { get; set; } = ".\\lib\\http-request.ini";

        /// <summary>
        /// ﾒﾆｭｰ情報を格納した設定ﾌｧｲﾙのﾊﾟｽ
        /// </summary>
        public string MenuModelPath { get; set; } = ".\\lib\\menu-model.xml";

        /// <summary>
        /// Httpﾃﾞｰﾀ
        /// </summary>
        public Https Http { get; set; } = new Https();

        public SearchByWords SearchByWord { get; set; } = new SearchByWords();

        #region inner classes

        public class Https
        {
            /// <summary>
            /// ﾒｰﾙｱﾄﾞﾚｽ
            /// </summary>
            public string MailAddress { get; set; }

            /// <summary>
            /// ﾊﾟｽﾜｰﾄﾞ
            /// </summary>
            public string Password {
                get {
                    return string.IsNullOrWhiteSpace(_Password)
                        ? _Password
                        : Encrypter.DecryptString(_Password, Variables.Instance.ApplicationId);
                }
                set
                {
                    _Password = Encrypter.EncryptString(value, Variables.Instance.ApplicationId);
                }
            }
            private string _Password;

            /// <summary>
            /// ﾛｸﾞｲﾝUrl
            /// </summary>
            public string LoginUrl { get; set; } = "https://secure.nicovideo.jp/secure/login";

            /// <summary>
            /// ﾛｸﾞｲﾝﾊﾟﾗﾒｰﾀ
            /// </summary>
            public string LoginParameter { get; set; } = "site=niconico&mail={0}&password={1}&next_url=http://flapi.nicovideo.jp/api/getflv/sm9";

            /// <summary>
            /// ﾀｲﾑｱｳﾄ
            /// </summary>
            public int Timeout { get; set; } = 100000;

            /// <summary>
            /// ﾕｰｻﾞｴｰｼﾞｪﾝﾄ
            /// </summary>
            public string UserAgent { get; set; } = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:17.0) Gecko/20100101 Firefox/17.0";

            /// <summary>
            /// ﾘﾌｧﾗ
            /// </summary>
            public string Referer { get; set; } = "http://www.nicovideo.jp/";

            /// <summary>
            /// ｸｯｷｰUrl
            /// </summary>
            public string CookieUrl { get; set; } = "http://nicovideo.jp/";

            /// <summary>
            /// ｸｯｷｰﾃﾞｰﾀ
            /// </summary>
            public string CookieData { get; set; } = "{0}; expires = {1}";

            /// <summary>
            /// ﾏｲﾘｽﾄUrl
            /// </summary>
            public string MylistUrl { get; set; } = "http://www.nicovideo.jp/mylist/";

            /// <summary>
            /// ﾏｲﾘｽﾄﾃﾞﾌｫﾙﾄ
            /// </summary>
            public string MylistDefault { get; set; } = "http://www.nicovideo.jp/api/deflist/list";
        }

        public class SearchByWords
        {
            public string TargetTag { get; set; } = "tagsExact";

            public string TargetKeyword { get; set; } = "title,description,tags";

            public string Fields { get; set; } = "contentId,title,description,tags,categoryTags,viewCounter,mylistCounter,commentCounter,startTime,lastCommentTime,lengthSeconds,thumbnailUrl,communityIcon";

            public string Context { get; set; } = "kaz.server-on.net/avv";

            public string Url { get; set; } = "http://api.search.nicovideo.jp/api/v2/video/contents/search?q={q}&targets={targets}&fields={fields}&_sort={sort}&_offset={offset}&_limit={limit}&_context={context}";

        }
        #endregion
    }
}
