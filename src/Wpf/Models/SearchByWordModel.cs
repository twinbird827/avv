using Avv.Apps.Commons;
using Avv.Wpf.Views.Services;
using StatefulModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Wpf.Models
{
    public class SearchByWordModel : WorkSpaceModel
    {
        /// <summary>
        /// 検索ﾜｰﾄﾞ
        /// </summary>
        public string Word
        {
            get { return _Word; }
            set { SetProperty(ref _Word, value); }
        }
        private string _Word = null;

        /// <summary>
        /// ﾀｸﾞ検索 or ｷｰﾜｰﾄﾞ検索
        /// </summary>
        public bool IsTag
        {
            get { return _IsTag; }
            set { SetProperty(ref _IsTag, value); }
        }
        private bool _IsTag = default(bool);

        /// <summary>
        /// ﾘﾐｯﾄ (何件取得するか)
        /// </summary>
        public int Limit
        {
            get { return _Limit; }
            set { SetProperty(ref _Limit, value); }
        }
        private int _Limit = 100;

        /// <summary>
        /// ｵﾌｾｯﾄ (取得する開始位置)
        /// </summary>
        public int Offset
        {
            get { return _Offset; }
            set { SetProperty(ref _Offset, value); }
        }
        private int _Offset = 0;

        public override void Reload()
        {
            Items.Clear();

            string targets = IsTag ? "tagsExact" : "title,description,tags";
            string q = HttpUtil.ToUrlEncode(Word);
            string fields = "contentId,title,description,tags,categoryTags,viewCounter,mylistCounter,commentCounter,startTime,lastCommentTime,lengthSeconds,thumbnailUrl,communityIcon";
            string offset = Offset.ToString();
            string limit = Limit.ToString();
            string context = "kaz.server-on.net/v2";
            string sort = SortModel.Instance.SelectedItem.Keyword;
            string url = $"http://api.search.nicovideo.jp/api/v2/video/contents/search?q={q}&targets={targets}&fields={fields}&_sort={sort}&_offset={offset}&_limit={limit}&_context={context}";
            string txt = GetSmileVideoHtmlText(url);

            if (IsError(txt))
            {
                return;
            }

            var json = DynamicJson.Parse(txt);

            foreach(dynamic data in json["data"])
            {
                Items.Add(new VideoModel()
                {
                    ContentId = data["contentId"],
                    Title = data["title"],
                    Description = data["description"],
                    Tags = data["tags"],
                    CategoryTag = data["categoryTag"],
                    ViewCounter = data["viewCounter"],
                    MylistCounter = data["mylistCounter"],
                    CommentCounter = data["commentCounter"],
                    StartTime = DateTime.Parse(data["startTime"]),
                    LastCommentTime = new DateTime(data["lastCommentTime"] * 1000),
                    LengthSeconds = data["lengthSeconds"],
                    ThumbnailUrl = data["thumbnailUrl"],
                    CommunityIcon = data["communityIcon"]
                });
            }

            ServiceFactory.MessageService.ShowError(url);
        }

        private bool IsError(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
            {
                ServiceFactory.MessageService.ShowError("何らかの通信エラーです。");
                return true;
            }

            var json = DynamicJson.Parse(txt);

            if (json["meta"]["status"] == "400")
            {
                ServiceFactory.MessageService.ShowError("不正なパラメータです。");
                return true;
            }

            if (json["meta"]["status"] == "500")
            {
                ServiceFactory.MessageService.ShowError("検索サーバの異常です。");
                return true;
            }

            if (json["meta"]["status"] == "503")
            {
                ServiceFactory.MessageService.ShowError("サービスがメンテナンス中です。メンテナンス終了までお待ち下さい。");
                return true;
            }

            if (json["meta"]["status"] == "410")
            {
                ServiceFactory.MessageService.ShowError("検索結果が見つかりませんでした。");
                return true;
            }

            if (json["meta"]["status"] == "403")
            {
                ServiceFactory.MessageService.ShowError("非公開に設定されています。");
                return true;
            }

            if (json["meta"]["status"] != "200")
            {
                ServiceFactory.MessageService.ShowError("何らかの通信エラーです。");
                return true;
            }

            return false;
        }
    }
}
