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
            return _board;
        }

        /// <summary>
        /// 対戦が終了している状態かを取得する。
        /// </summary>
        /// <returns></returns>
        public bool IsFinish()
        {
            var playedNum = _board.AllData.Where(_ => _.Color == DiscColor.NONE).Count();
            var res = playedNum == 0 || playedNum == _board.AllData.Count();
            return res;
        }

        /// <summary>
        /// 指定箇所に石を打つ。
        /// </summary>
        /// <returns></returns>
        public Board Play(object param)
        {
            var id = (int)param;
            var row = (int)id / Board.COLNUM;
            var col = (int)id % Board.COLNUM;
            _board.Set(row, col, _turn % 2 == 0 ? DiscColor.BLACK : DiscColor.WHITE);
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
