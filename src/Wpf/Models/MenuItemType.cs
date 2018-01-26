using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Wpf.Models
{
    public enum MenuItemType
    {
        /// <summary>
        /// 文字による検索
        /// </summary>
        SearchByWord = 0,

        /// <summary>
        /// ﾗﾝｷﾝｸﾞ
        /// </summary>
        Ranking = 1,

        /// <summary>
        /// 一時ﾌｫﾙﾀﾞ
        /// </summary>
        Temporary = 2,

        /// <summary>
        /// お気に入り
        /// </summary>
        Favorite = 4,

        /// <summary>
        /// ﾏｲﾘｽﾄによる検索
        /// </summary>
        SearchByMylist = 8,

        /// <summary>
        /// ﾏｲﾘｽﾄ
        /// </summary>
        Mylist = 16,

        /// <summary>
        /// 設定
        /// </summary>
        Setting = 32
    }
}
