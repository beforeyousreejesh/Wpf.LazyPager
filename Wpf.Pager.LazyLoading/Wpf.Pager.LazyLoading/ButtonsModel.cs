using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wpf.Pager.LazyLoading
{
    public class ButtonsModel : INotifyPropertyChanged
    {
        private LazyPager _parent;

        private ICommand _pageCommand;

        public ICommand PageCommand
        {
            get
            {
                return _pageCommand;
            }
            set
            {
                _pageCommand = value;
                RaisePropertyChangedEvent("PageCommand");
            }
        }


        public ButtonsModel(LazyPager parent)
        {
            _parent = parent;
            PageCommand = new RelayCommand(NumberClick);
        }

        public int PageNumber { get; set; }

        public void NumberClick(object param)
        {
            if (_parent != null && _parent.PageChangedCommand != null)
            {
                _parent.PageChangedCommand.Execute(PageNumber);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
