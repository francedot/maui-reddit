using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiReddit.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected ViewModelBase()
        {
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string name = null)
        {
            if (Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(name);
            return true;
        }

        #endregion
    }
}
