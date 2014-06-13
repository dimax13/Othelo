using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Othelo.Model;
using Othelo.Common;

namespace Othelo.ViewModel
{
    class GameInfo : ViewModelBase
    {
        public GameInfo()
        {
            PlayData = new PlayData();
            _board = new Board();
        }

        #region Property
        private Board _board;
        public IEnumerable<Disc> Board { get { return _board.AllData; } set { _board.AllData = value; RaisePropertyChanged("Board"); } }

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

        public PlayData PlayData { get; set; }

        #endregion

        #region Command

        private RelayCommand _start;
        public ICommand StartCommand
        {
            get
            {
                if (_start == null)
                {
                    _start = new RelayCommand(param => Board = (PlayData = new PlayData()).Start().AllData);
                }
                return _start;
            }
        }

        private RelayCommand _play;
        public ICommand PlayCommand
        {
            get
            {
                if (_play == null)
                {
                    _play = new RelayCommand(param => Board = PlayData.Play(param).AllData, param => PlayData != null && !PlayData.IsFinish());
                }
                return _play;
            }
        }


        private RelayCommand _save;
        public ICommand SaveCommand
        {
            get
            {
                if (_save == null)
                {
                    _save = new RelayCommand(param => PlayData.Save(), param => PlayData != null && !PlayData.IsFinish());
                }
                return _save;
            }
        }

        public string SaveFileName { set { PlayData.SaveFileName = value; RaisePropertyChanged("SaveFileName"); } }
        
        private RelayCommand _open;
        public ICommand OpenCommand
        {
            get 
            {
                if (_open == null)
                {
                    _open = new RelayCommand(
                        param => PlayData = PlayData.Open(),
                        param => PlayData != null && !PlayData.IsFinish() ? MessageBox.Show("対戦データがありますが終了しますか？", "警告", MessageBoxButton.OKCancel) == System.Windows.MessageBoxResult.OK : true
                        );
                }
                return _open;
            }
        }

        private RelayCommand _exit;

        public ICommand ExitCommand
        {
            get
            {
                if (_exit == null)
                {
                    // 終了していないデータがあれば破棄してもOKかを確認する。
                    _exit = new RelayCommand(
                        param => Environment.Exit(0),
                        param => PlayData != null && !PlayData.IsFinish() ? MessageBox.Show("対戦データがありますが終了しますか？", "警告", MessageBoxButton.OKCancel) == System.Windows.MessageBoxResult.OK : true
                        );
                }
                return _exit;
            }
        }
        


        
        #endregion
    }
}
