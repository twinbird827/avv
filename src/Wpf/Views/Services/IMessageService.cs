using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Wpf.Views.Services
{
    public interface IMessageService
    {
        /// <summary>
        /// ｴﾗｰを表示します。
        /// </summary>
        /// <param name="message">ｴﾗｰ内容</param>
        void ShowError(string message);
    }
}
