using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Wpf.Views.Services
{
    public class ServiceFactory
    {
        /// <summary>
        /// ﾕｰｻﾞ入力ｻｰﾋﾞｽ
        /// </summary>
        public static IUserInputService UserInputService { get; private set; } = new WpfUserInputService();

        /// <summary>
        /// ﾒｯｾｰｼﾞｻｰﾋﾞｽ
        /// </summary>
        public static IMessageService MessageService { get; private set; } = new WpfMessageService();

    }
}
