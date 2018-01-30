using Avv.Wpf.Models;
using StatefulModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Wpf.ViewModels
{
    public class SearchByWordViewModel : WorkSpaceViewModel
    {
        /// <summary>
        /// ｺﾝｽﾄﾗｸﾀ
        /// </summary>
        public SearchByWordViewModel() : this(new SearchByWordModel())
        {

        }

        /// <summary>
        /// ｺﾝｽﾄﾗｸﾀ
        /// </summary>
        public SearchByWordViewModel(SearchByWordModel model) : base(model)
        {
            Source = model;
            SortModel = SortModel.Instance;
            SortItems = SortModel.SortItems.ToSyncedSynchronizationContextCollection(m => m, AnonymousSynchronizationContext.Current);
        }

        /// <summary>
        /// 本ｲﾝｽﾀﾝｽのﾃﾞｰﾀ実体
        /// </summary>
        public SearchByWordModel Source { get; set; }

        /// <summary>
        /// ｿｰﾄ用ﾓﾃﾞﾙ
        /// </summary>
        public SortModel SortModel
        {
            get { return _SortModel; }
            set { SetProperty(ref _SortModel, value); }
        }
        private SortModel _SortModel;

        /// <summary>
        /// ﾀｸﾞ検索 or ｷｰﾜｰﾄﾞ検索
        /// </summary>
        public bool IsTag
        {
            get { return Source.IsTag; }
            set { SetProperty(ref _IsTag, value); Source.IsTag = value; }
        }
        private bool _IsTag;

        /// <summary>
        /// 検索ﾜｰﾄﾞ
        /// </summary>
        public string Word
        {
            get { return Source.Word; }
            set { SetProperty(ref _Word, value); Source.Word = value; }
        }
        private string _Word;

        /// <summary>
        /// ｿｰﾄﾘｽﾄ
        /// </summary>
        public SynchronizationContextCollection<SortItemModel> SortItems
        {
            get { return _SortItems; }
            set { SetProperty(ref _SortItems, value); }
        }
        private SynchronizationContextCollection<SortItemModel> _SortItems;

        /// <summary>
        /// 選択中のｿｰﾄ項目
        /// </summary>
        public SortItemModel SelectedSortItem
        {
            get { return SortModel.SelectedItem; }
            set { SetProperty(ref _SelectedSortItem, value); SortModel.SelectedItem = value; }
        }
        private SortItemModel _SelectedSortItem;

        /// <summary>
        /// 検索ﾎﾞﾀﾝ押下時の処理
        /// </summary>
        public void OnClicked()
        {
            // ﾘﾛｰﾄﾞ
            Source.Reload();
        }
    }
}
