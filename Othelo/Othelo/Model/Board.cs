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
        private const int ROWNUM = 8, COLNUM = ROWNUM;
        public Board()
        {
            _data = Enumerable.Range(0, ROWNUM * COLNUM).Select(_ => new Disc() { ID = _, Color = DiscColor.BLACK, Row = _ / COLNUM, Col = _ % COLNUM }).ToArray();
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
            set { _data[row * COLNUM + col] = value; }
        }

        /// <summary>
        /// 盤を初期化
        /// ４つ石を置く
        /// </summary>
        public void Initialize()
        {

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
            for (int i = row, j = col; i >= 0 && i < ROWNUM && j >= 0 && j < COLNUM; i += (int)changeValue.Y, j += (int)changeValue.X)
                yield return _data[i * COLNUM + j];
        }
    }

    public enum Orientation
    {
        TOP, LEFTTOP, LEFT, LEFTBOTTOM, BOTTOM, RIGHTBOTTOM, RIGHT, RIGHTTOP
    }
}
