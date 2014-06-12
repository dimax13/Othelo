using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Othelo.Model
{
    class Board
    {
        private Disc[] _data;
        private const int ROWNUM = 8, COLNUM = ROWNUM;
        public Board()
        {
            var id = 0;
            _data = Enumerable.Repeat<Disc>(new Disc(){ID = id++}, ROWNUM * COLNUM).ToArray();
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
