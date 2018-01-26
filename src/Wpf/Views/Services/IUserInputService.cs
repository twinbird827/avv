using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Wpf.Views.Services
{
    public interface IUserInputService
    {
        /// <summary>
        /// ﾕｰｻﾞにﾒﾆｭｰ名の入力を促し、入力された新しい名前を取得します。
        /// </summary>
        /// <param name="old">古い名前</param>
        /// <returns>新しい名前</returns>
        string GetNewFavoriteName(string old);
    }
}
