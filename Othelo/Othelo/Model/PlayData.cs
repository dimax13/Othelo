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
            return _board.AllData.Where(_ => _.Color == DiscColor.NONE).Count() == 0;
        }

        /// <summary>
        /// 指定箇所に石を打つ。
        /// </summary>
        /// <returns></returns>
        public Board Play(object param)
        {
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
