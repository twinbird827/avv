using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Apps.Parameters
{
    public class Variables
    {
        /// <summary>
        /// ｱﾌﾟﾘｹｰｼｮﾝ固有のId
        /// </summary>
        public const string ApplicationId = "P&imsbQSWKKQ";

        /// <summary>
        /// HTTPﾚｽﾎﾟﾝｽのｴﾝｺｰﾃﾞｨﾝｸﾞ
        /// </summary>
        public static Encoding ResponseEncoding { get; private set; } = Encoding.UTF8;

        /// <summary>
        /// XMLﾌｧｲﾙのｴﾝｺｰﾃﾞｨﾝｸﾞ
        /// </summary>
        public static Encoding XmlEncoding { get; private set; } = Encoding.UTF8;

        /// <summary>
        /// HTTPﾘｸｴｽﾄ関連のﾊﾟﾗﾒｰﾀを格納した設定ﾌｧｲﾙのﾊﾟｽ
        /// </summary>
        public const string HttpRequestConstPath = ".\\lib\\http-request.ini";

        /// <summary>
        /// ﾒﾆｭｰ情報を格納した設定ﾌｧｲﾙのﾊﾟｽ
        /// </summary>
        public const string MenuModelPath = ".\\lib\\menu-model.xml";

    }
}
