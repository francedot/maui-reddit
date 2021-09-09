using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReddit.ViewModels
{
    public abstract class PageViewModelBase : ViewModelBase
    {
        public abstract Task OnNavigatedToAsync();
        public abstract Task OnNavigatedFromAsync();
    }
}
