using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wpf.Pager.LazyLoading.Demo
{
    public class MainWindowVm : INotifyPropertyChanged
    {
        private const int NumberOfItemsPerPage = 20;

        private IEnumerable<TestData> _testDatas;

        public MainWindowVm()
        {
            PageClickedCommand = new RelayCommand(PageClicked);
            PageSize = 3;
        }

        private int _totalPage;

        public int TotalPage
        {
            get
            {
                return _totalPage;
            }
            set
            {
                _totalPage = value;
                RaisePropertyChangedEvent("TotalPage");
            }
        }

        private int _pageSize;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
                RaisePropertyChangedEvent("PageSize");
            }
        }

        private ICommand _pageClickedCommand;

        public ICommand PageClickedCommand
        {
            get
            {
                return _pageClickedCommand;
            }
            set
            {
                _pageClickedCommand = value;
                RaisePropertyChangedEvent("PageClickedCommand");
            }
        }

        private ObservableCollection<TestData> _result;

        public ObservableCollection<TestData> Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                RaisePropertyChangedEvent("Result");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PageClicked(object page)
        {
            var skip = ((int)page - 1) * NumberOfItemsPerPage;

            var pagedData = GetPagesData(skip, NumberOfItemsPerPage);

            Result = new ObservableCollection<TestData>(pagedData);
        }

        public MainWindowVm LoadContent()
        {
            _testDatas = TestData.GetTestData();

            var pagedData = GetPagesData(0, NumberOfItemsPerPage);

            Result = new ObservableCollection<TestData>(pagedData);

            float tPage = (float)_testDatas.Count() / (float)NumberOfItemsPerPage;

            TotalPage = Convert.ToInt32(Math.Ceiling(tPage));

            return this;
        }

        private IEnumerable<TestData> GetPagesData(int skip, int take)
        {
            return _testDatas.Skip(skip).Take(NumberOfItemsPerPage);
        }
    }
}
