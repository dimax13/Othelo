using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Othelo.Model;
using Othelo.Common;

namespace Othelo.ViewModel
{
    class GameInfo : ViewModelBase
    {
        #region Property
        private Board _board;
        public Board Board { get { return _board; } set { _board = value; RaisePropertyChanged("Board"); } }

        private int _blackScore = 2;
        public int BlacScore
        {
            get { return _blackScore; }
            set { _blackScore = value; RaisePropertyChanged("BlacScore"); }
        }

        private int _whiteScore = 2;
        public int WhiteScore
        {
            get { return _whiteScore; }
            set { _whiteScore = value; RaisePropertyChanged("WhiteScore"); }
        }

        #endregion
    }
}
