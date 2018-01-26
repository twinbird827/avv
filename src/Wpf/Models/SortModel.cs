using StatefulModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Wpf.Models
{
    public class SortModel : BindableBase
    {
        /// <summary>
        /// ｿｰﾄﾘｽﾄ構成
        /// </summary>
        public ObservableSynchronizedCollection<SortItemModel> SortItems
        {
            get { return _SortItems; }
            set { SetProperty(ref _SortItems, value); }
        }
        private ObservableSynchronizedCollection<SortItemModel> _SortItems;

        /// <summary>
        /// 選択中のｿｰﾄ構成
        /// </summary>
        public SortItemModel SelectedItem
        {
            get { return _SelectedItem; }
            set { SetProperty(ref _SelectedItem, value); }
        }
        private SortItemModel _SelectedItem;

        /// <summary>
        /// ｲﾝｽﾀﾝｽ (ｼﾝｸﾞﾙﾄﾝﾊﾟﾀｰﾝ)
        /// </summary>
        public static SortModel Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SortModel();
                }
                return _Instance;
            }
        }
        private static SortModel _Instance;

        private SortModel()
        {
            SortItems = new ObservableSynchronizedCollection<SortItemModel>
            {
                new SortItemModel() { Keyword = "-title", Description = "-title" },
                new SortItemModel() { Keyword = "+title", Description = "+title" },
                new SortItemModel() { Keyword = "-description", Description = "-description" },
                new SortItemModel() { Keyword = "+description", Description = "+description" },
                new SortItemModel() { Keyword = "-tags", Description = "-tags" },
                new SortItemModel() { Keyword = "+tags", Description = "+tags" },
                new SortItemModel() { Keyword = "-categoryTags", Description = "-categoryTags" },
                new SortItemModel() { Keyword = "+categoryTags", Description = "+categoryTags" },
                new SortItemModel() { Keyword = "-viewCounter", Description = "-viewCounter" },
                new SortItemModel() { Keyword = "+viewCounter", Description = "+viewCounter" },
                new SortItemModel() { Keyword = "-mylistCounter", Description = "-mylistCounter" },
                new SortItemModel() { Keyword = "+mylistCounter", Description = "+mylistCounter" },
                new SortItemModel() { Keyword = "-commentCounter", Description = "-commentCounter" },
                new SortItemModel() { Keyword = "+commentCounter", Description = "+commentCounter" },
                new SortItemModel() { Keyword = "-startTime", Description = "-startTime" },
                new SortItemModel() { Keyword = "+startTime", Description = "+startTime" },
                new SortItemModel() { Keyword = "-lastCommentTime", Description = "-lastCommentTime" },
                new SortItemModel() { Keyword = "+lastCommentTime", Description = "+lastCommentTime" },
                new SortItemModel() { Keyword = "-lengthSeconds", Description = "-lengthSeconds" },
                new SortItemModel() { Keyword = "+lengthSeconds", Description = "+lengthSeconds" }
            };
            SelectedItem = SortItems.First();
        }
    }
}
