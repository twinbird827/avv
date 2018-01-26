using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Wpf
{
    public abstract class ViewModelBase : BindableBase
    {
        private List<BindableBase> SubscribedModels { get; set; } = new List<BindableBase>();

        /// <summary>
        /// Modelの変更通知を明示的に購読したい場合に使用する。
        /// 本ﾒｿｯﾄﾞを実行することで、<see cref="PropertyChangedByModel(object, PropertyChangedEventArgs)"/>ｲﾍﾞﾝﾄが発生する。
        /// </summary>
        /// <param name="model">ﾓﾃﾞﾙ</param>
        protected void SubscribePropertyChangedByModel(BindableBase model)
        {
            // 既に購読している場合は中断
            if (!SubscribedModels.Contains(model)) return;

            // ﾓﾃﾞﾙのｲﾍﾞﾝﾄを購読する。
            model.PropertyChanged += PropertyChangedByModel;

            // 購読したﾓﾃﾞﾙを記憶する。
            SubscribedModels.Add(model);
        }

        /// <summary>
        /// ﾓﾃﾞﾙで発生したPropertyChanged
        /// </summary>
        /// <param name="sender">ｾﾝﾀﾞｰ</param>
        /// <param name="e">ｲﾍﾞﾝﾄ</param>
        protected virtual void PropertyChangedByModel(object sender, PropertyChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Initialize()
        {

        }

        #region IDisposable Support

        /// <summary>
        /// Disposeによりﾏﾈｰｼﾞ状態を破棄します。
        /// </summary>
        protected override void OnDisposing()
        {
            base.OnDisposing();

            // ﾓﾃﾞﾙｲﾍﾞﾝﾄの購読を解除する。
            Array.ForEach(
                SubscribedModels.ToArray(),
                m => m.PropertyChanged -= PropertyChangedByModel
            );
        }

        /// <summary>
        /// Disposeによりｱﾝﾏﾈｰｼﾞﾘｿｰｽを開放します。
        /// </summary>
        protected override void OnDisposed()
        {
            base.OnDisposed();

            SubscribedModels = null;
        }

        #endregion
    }
}
