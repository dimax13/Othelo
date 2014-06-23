using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Othelo.Common
{
    class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary> 
        /// プロパティの変更があったときに発行されます。 
        /// </summary> 
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> 
        /// PropertyChangedイベントを発行します。 
        /// </summary> 
        /// <param name="propertyName">プロパティ名</param> 
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var h = this.PropertyChanged;
            if (h != null)
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
