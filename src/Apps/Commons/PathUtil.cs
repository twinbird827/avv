using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Apps.Commons
{
    public class PathUtil
    {
        /// <summary>
        /// ｱﾌﾟﾘｹｰｼｮﾝの実行時ﾌｫﾙﾀﾞを取得します。
        /// </summary>
        public static string ApplicationDirectoryPath
        {
            get
            {
                return System.IO.Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().Location
                );
            }
        }

        /// <summary>
        /// ﾊﾟｽを結合して1つのﾊﾟｽにします。
        /// </summary>
        /// <param name="paths">ﾊﾟｽの配列</param>
        /// <returns>ﾊﾟｽ文字列</returns>
        public static string Combine(params string[] paths)
        {
            return System.IO.Path.Combine(paths.Select(s => s.Trim(' ', '　', '.')).ToArray());
        }

        /// <summary>
        /// 引数のﾊﾟｽをｱﾌﾟﾘｹｰｼｮﾝの実行時ﾌｫﾙﾀﾞからの絶対ﾊﾟｽとして取得します。
        /// </summary>
        /// <param name="paths">ﾊﾟｽの配列</param>
        /// <returns>ｱﾌﾟﾘｹｰｼｮﾝの実行時ﾌｫﾙﾀﾞからの絶対ﾊﾟｽ</returns>
        public static string GetFullPath(params string[] paths)
        {
            return Combine(ApplicationDirectoryPath, Combine(paths));
        }


    }
}
