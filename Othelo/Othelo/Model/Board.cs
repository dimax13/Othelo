using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace Othelo.Model
{
    class Board
    {
        private Disc[] _data;
        public static int ROWNUM = 8, COLNUM = ROWNUM;
        public Board()
        {
            _data = Enumerable.Range(0, ROWNUM * COLNUM).Select(_ => new Disc() { ID = _, Color = DiscColor.NONE, Row = _ / COLNUM, Col = _ % COLNUM }).ToArray();
        }

        /// <summary>
        /// 全データを取得する。
        /// </summary>
        public IEnumerable<Disc> AllData
        {
            get
            {
                foreach (var item in _data) yield return item;
            }
            set
            {
                _data = value.ToArray();
            }
        }

        /// <summary>
        /// 指定した行、列のデータを取得する。
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <returns></returns>
        public Disc this[int row, int col]
        {
            get { return _data[row * COLNUM + col]; }
            private set { _data[row * COLNUM + col] = value; }
        }

        /// <summary>
        /// 盤を初期化
        /// ４つ石を置く
        /// </summary>
        public void Initialize()
        {
            this.Set(ROWNUM / 2 - 1, COLNUM / 2 - 1, DiscColor.BLACK);
            this.Set(ROWNUM / 2, COLNUM / 2, DiscColor.BLACK);
            this.Set(ROWNUM / 2 - 1, COLNUM / 2, DiscColor.WHITE);
            this.Set(ROWNUM / 2, COLNUM / 2 - 1, DiscColor.WHITE);
        }

        public void Set(int row, int col, DiscColor color)
        {
            this[row, col].Color = color;
        }

        /// <summary>
        /// 指定した一列の石データを取得する。
        /// </summary>
        /// <param name="row">開始行</param>
        /// <param name="col">開始列</param>
        /// <param name="ori">方向</param>
        /// <returns></returns>
        public IEnumerable<Disc> GetLine(int row, int col, Orientation ori)
        {
            var changeValue = new Point();
            switch (ori)
            {
                case Orientation.TOP:
                    changeValue = new Point(0, -1);
                    break;
                case Orientation.LEFTTOP:
                    changeValue = new Point(-1, -1);
                    break;
                case Orientation.LEFT:
                    changeValue = new Point(-1, 0);
                    break;
                case Orientation.LEFTBOTTOM:
                    changeValue = new Point(-1, 1);
                    break;
                case Orientation.BOTTOM:
                    changeValue = new Point(0, 1);
                    break;
                case Orientation.RIGHTBOTTOM:
                    changeValue = new Point(1, 1);
                    break;
                case Orientation.RIGHT:
                    changeValue = new Point(1, 0);
                    break;
                case Orientation.RIGHTTOP:
                    changeValue = new Point(1, -1);
                    break;
                default:
                    break;
            }
            var res = new List<Disc>();
            for (int i = row + (int)changeValue.Y, j = col + (int)changeValue.X; i >= 0 && i < ROWNUM && j >= 0 && j < COLNUM; i += (int)changeValue.Y, j += (int)changeValue.X)
            {
                res.Add(_data[i * COLNUM + j]);
                if (_data[i * COLNUM + j].Color == _data[row * COLNUM + col].Color) break;
                if (_data[i * COLNUM + j].Color == DiscColor.NONE || _data[i * COLNUM + j].Color == DiscColor.PLAYABLE) break;
            }
            return res;
        }
    }

    public enum Orientation
    {
        TOP, LEFTTOP, LEFT, LEFTBOTTOM, BOTTOM, RIGHTBOTTOM, RIGHT, RIGHTTOP
    }
}
