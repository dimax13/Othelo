using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Othelo.Model
{
    class PlayData
    {
        private Board _board;
        private int _turn;

        public PlayData()
        {
            _turn = 0;
            _board = new Board();
        }

        /// <summary>
        /// 対戦を開始する。
        /// </summary>
        /// <returns></returns>
        public Board Start()
        {
            _turn = 1;
            _board = new Board();
            _board.Initialize();
            this.UpdatePlayableDisc();
            return _board;
        }

        /// <summary>
        /// 対戦が終了している状態かを取得する。
        /// </summary>
        /// <returns></returns>
        public bool IsFinish()
        {
            var playedNum = _board.AllData.Where(_ => _.Color == DiscColor.PLAYABLE).Count();
            var res = playedNum == 0;
            return res;
        }

        /// <summary>
        /// 指定箇所に石を打つ。
        /// </summary>
        /// <returns></returns>
        public Board Play(int row, int col)
        {
            _board.Set(row, col, GetTurnColor());
            Reverse(row, col);
            _turn++;
            UpdatePlayableDisc();
            return _board;
        }

        private DiscColor GetTurnColor()
        {
            return _turn % 2 == 0 ? DiscColor.BLACK : DiscColor.WHITE;
        }

        private void UpdatePlayableDisc()
        {
            // PlayableをNoneにする
            foreach (var disc in _board.AllData) disc.Color = disc.Color == DiscColor.PLAYABLE ? DiscColor.NONE : disc.Color;
            SetDiscWhere(line => { return line.LastOrDefault() == null || line.LastOrDefault().Color != DiscColor.NONE || line.Count < 2 || line[line.Count - 2].Color == GetTurnColor();},
                DiscColor.PLAYABLE,
                (line, color) => { line.LastOrDefault().Color = color;});
        }

        private void SetDiscWhere(Func<List<Disc>, bool> isNotPlayable, DiscColor color, Action<List<Disc>, DiscColor> set)
        {
            var discList = _board.AllData.Where(_ => _.Color == GetTurnColor()).ToList();
            foreach (var disc in discList)
            {
                foreach (var ori in Enum.GetValues(typeof(Orientation)).Cast<Orientation>())
                {
                    var line = _board.GetLine(disc.Row, disc.Col, ori).ToList();
                    if (isNotPlayable(line)) continue;
                    set(line, color);
                }
            }

        }

        private void Reverse(int row, int col)
        {
            SetDiscWhere(line => { return line.LastOrDefault() == null || line.LastOrDefault().Color != GetTurnColor(); },
                GetTurnColor(),
                (line, color) => { foreach(var disc in line) disc.Color = color;});
        }

        public bool CanPlay(int row, int col)
        {
            return _board[row, col].Color == DiscColor.PLAYABLE;
        }

        /// <summary>
        /// パスする。
        /// </summary>
        /// <returns></returns>
        public Board Pass()
        {
            _turn++;
            return _board;
        }

        public string SaveFileName { get; set; }
        private const string _extension = ".txt";
        /// <summary>
        /// PlayDataを中断ファイルに保存する。
        /// </summary>
        public void Save()
        {
            string fileNmae = SaveFileName + System.DateTime.Now.ToShortDateString() + _extension;
            var fs = new FileStream(SaveFileName, FileMode.Create);
        }

        /// <summary>
        /// 中断ファイルを開いて再開する。
        /// todo: savefilename から情報を取得してPlayDataの形式に変換する。
        /// </summary>
        /// <returns></returns>
        public PlayData Open()
        {
            return new PlayData();
        }
    }
}
