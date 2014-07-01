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
        public IEnumerable<Disc> Board {
            get { return _board.AllData; } 
            set { _board.AllData = value; RaisePropertyChanged("Board"); }
        }

        private int _blackScore = 0;
        public int BlackScore
        {
            get { return _blackScore; }
            set { _blackScore = value; RaisePropertyChanged("BlackScore"); }
        }

        private int _whiteScore = 0;
        public int WhiteScore
        {
            get { return _whiteScore; }
            set { _whiteScore = value; RaisePropertyChanged("WhiteScore"); }
        }

        private bool _isFinished;

        public bool IsFinished
        {
            get { return _isFinished; }
            set { _isFinished = value; RaisePropertyChanged("IsFinished"); }
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
                    _start = new RelayCommand(param => Board = this.Start());
                }
                return _start;
            }
        }
        private IEnumerable<Disc> Start()
        {
            var data = (PlayData = new PlayData()).Start().AllData;
            BlackScore = data.Count(_ => _.Color == DiscColor.BLACK);
            WhiteScore = data.Count(_ => _.Color == DiscColor.WHITE);
            return data;
        }

        private RelayCommand _play;
        public ICommand PlayCommand
        {
            get
            {
                if (_play == null)
                {
                    _play = new RelayCommand(param => Board = this.Play(param), param => this.CanPlay(param));
                }
                return _play;
            }
        }

        /// <summary>
        /// クリックした石を自分の色にする。
        /// </summary>
        /// <param name="param">石自身(Button)<param>
        /// <returns></returns>
        private IEnumerable<Disc> Play(object param)
        {
            var disc = GetDiscFromParam(param);
            if (disc == null) return this.Start();
            var data = PlayData.Play(disc.Row, disc.Col).AllData;
            BlackScore = data.Count(_ => _.Color == DiscColor.BLACK);
            WhiteScore = data.Count(_ => _.Color == DiscColor.WHITE);
            return data;
        }

        private Disc GetDiscFromParam(object param)
        {
            var button = param as System.Windows.Controls.Button;
            if (button == null) return null;
            var parent = System.Windows.Media.VisualTreeHelper.GetParent(button) as System.Windows.Controls.ContentPresenter;
            if (parent == null) return null;
            var disc = parent.Content as Disc;
            return disc;
        }

        /// <summary>
        /// 石を置くことができるか判定
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private bool CanPlay(object param)
        {
            var disc = GetDiscFromParam(param);
            return disc != null && PlayData != null && !(IsFinished = PlayData.IsFinish()) && PlayData.CanPlay(disc.Row, disc.Col);
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
