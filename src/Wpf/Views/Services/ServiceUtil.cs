using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Avv.Wpf.Views.Services
{
    public class ServiceUtil
    {
        /// <summary>
        /// ｱｸﾃｨﾌﾞｳｨﾝﾄﾞｳを取得する
        /// </summary>
        /// <returns>Windowｲﾝｽﾀﾝｽ。取得できない場合はnull</returns>
        public static Window GetActiveWindow()
        {
            var owner =
                Application.Current.Windows.OfType<Window>().SingleOrDefault(
                    x => x.IsActive
            );
            return owner;
        }

        /// <summary>
        /// 現在メッセージ待ち行列の中にある全てのUIメッセージを処理します。
        /// </summary>
        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            var callback = new DispatcherOperationCallback(obj =>
            {
                ((DispatcherFrame)obj).Continue = false;
                return null;
            });
            GetActiveWindow().Dispatcher.BeginInvoke(DispatcherPriority.Background, callback, frame);
            Dispatcher.PushFrame(frame);
            //GetActiveWindow().Dispatcher.BeginInvoke(new Action(() => { }), DispatcherPriority.Background);
        }

        /// <summary>
        /// 指定した画面ｵﾌﾞｼﾞｪｸﾄ内に不正な値が含まれていないか確認します。
        /// </summary>
        /// <param name="parent">画面ｵﾌﾞｼﾞｪｸﾄ</param>
        /// <returns>入力OK：true / 入力NG：false</returns>
        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
                return false;

            // Validate all the bindings on the children
            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (!IsValid(child)) { return false; }
            }

            return true;
        }
    }
}
