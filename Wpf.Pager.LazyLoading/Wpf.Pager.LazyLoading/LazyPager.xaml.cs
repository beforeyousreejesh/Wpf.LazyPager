using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf.Pager.LazyLoading
{
    /// <summary>
    /// Interaction logic for LazyPager.xaml
    /// </summary>
    public partial class LazyPager : UserControl
    {
        private ScrollViewer _scrollPager;

        private List<ButtonsModel> _numberButtons;

        [Bindable(true)]
        public int TotalPage
        {
            get { return (int)GetValue(TotalPageProperty); }
            set
            {
                SetValue(TotalPageProperty, value);
            }
        }


        public static readonly DependencyProperty TotalPageProperty =
            DependencyProperty.Register("TotalPage",
                typeof(int),
                typeof(LazyPager),
                new PropertyMetadata(0, (d, e) =>
                {
                    if (e.NewValue != e.OldValue)
                    {
                        var control = d as LazyPager;

                        control.BuildPages();
                    }
                }));

        [Bindable(true)]
        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set
            {
                SetValue(PageSizeProperty, value);
            }
        }

        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize",
                typeof(int),
                typeof(LazyPager),
                new PropertyMetadata(5));


        [Bindable(true)]
        public ICommand PageChangedCommand
        {
            get { return GetValue(PageChangedCommandProperty) as ICommand; }
            set { SetValue(PageChangedCommandProperty, value); }
        }

        public static readonly DependencyProperty PageChangedCommandProperty =
            DependencyProperty.Register("PageChangedCommand", typeof(ICommand),
              typeof(LazyPager), new PropertyMetadata(null));



        public LazyPager()
        {
            InitializeComponent();

            gridPagingControl.DataContext = this;

            btnNext.Visibility = Visibility.Collapsed;
            btnPrev.Visibility = Visibility.Collapsed;
            btnHome.Visibility = Visibility.Collapsed;
            btnLast.Visibility = Visibility.Collapsed;
        }

        private void BuildPages()
        {
            _numberButtons = GenerateButtons(1, TotalPage);
            listOfButton.ItemsSource = _numberButtons;

            if (_scrollPager != null && _numberButtons != null)
            {
                if (_numberButtons.Count > PageSize)
                {
                    var widthS = 33 * PageSize;
                    _scrollPager.Width = widthS;
                    btnPrev.IsEnabled = false;
                    btnNext.IsEnabled = true;
                    btnHome.IsEnabled = false;
                    btnLast.IsEnabled = true;
                }
                else
                {
                    var widthS = 33 * _numberButtons.Count;
                    _scrollPager.Width = widthS;
                    btnPrev.IsEnabled = false;
                    btnNext.IsEnabled = false;
                    btnHome.IsEnabled = false;
                    btnLast.IsEnabled = false;
                }

                btnNext.Visibility = Visibility.Visible;
                btnPrev.Visibility = Visibility.Visible;
                btnHome.Visibility = Visibility.Visible;
                btnLast.Visibility = Visibility.Visible;
            }
        }


        private List<ButtonsModel> GenerateButtons(int start, int end)
        {
            var listOfPageButtons = new List<ButtonsModel>();

            for (int i = start; i <= end; i++)
            {
                listOfPageButtons.Add(new ButtonsModel(this) { PageNumber = i });
            }

            return listOfPageButtons;
        }

        private void PrevClick(object sender, RoutedEventArgs e)
        {
            if (_scrollPager != null)
            {
                btnLast.IsEnabled = true;
                btnNext.IsEnabled = true;
                _scrollPager.ScrollToHorizontalOffset(_scrollPager.HorizontalOffset - 33);

                if (_scrollPager.HorizontalOffset - 33 == 0 || _scrollPager.HorizontalOffset == 0)
                {
                    btnHome.IsEnabled = false;
                    btnPrev.IsEnabled = false;
                }

            }
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            if (_scrollPager != null)
            {
                btnHome.IsEnabled = true;
                btnPrev.IsEnabled = true;
                _scrollPager.ScrollToHorizontalOffset(_scrollPager.HorizontalOffset + 33);

                if (_scrollPager.HorizontalOffset + 33 == _scrollPager.ScrollableWidth)
                {
                    btnLast.IsEnabled = false;
                    btnNext.IsEnabled = false;
                }
            }
        }

        private void listOfButton_Loaded(object sender, RoutedEventArgs e)
        {
            _scrollPager =
                VisualTreeHelper.GetChild(listOfButton, 0) as ScrollViewer;


            if (_scrollPager != null && _numberButtons != null)
            {
                if (_numberButtons.Count > PageSize)
                {
                    var widthS = 33 * PageSize;
                    _scrollPager.Width = widthS;
                    btnPrev.IsEnabled = false;
                    btnNext.IsEnabled = true;
                    btnHome.IsEnabled = false;
                    btnLast.IsEnabled = true;
                }
                else
                {
                    var widthS = 33 * _numberButtons.Count;
                    _scrollPager.Width = widthS;
                    btnPrev.IsEnabled = false;
                    btnNext.IsEnabled = false;
                    btnHome.IsEnabled = false;
                    btnLast.IsEnabled = false;
                }


                btnNext.Visibility = Visibility.Visible;
                btnPrev.Visibility = Visibility.Visible;
                btnHome.Visibility = Visibility.Visible;
                btnLast.Visibility = Visibility.Visible;
            }
        }

        private void HomeClick(object sender, RoutedEventArgs e)
        {
            if (_scrollPager != null)
            {
                btnLast.IsEnabled = true;
                btnNext.IsEnabled = true;

                _scrollPager.ScrollToLeftEnd();

                btnHome.IsEnabled = false;
                btnPrev.IsEnabled = false;
            }
        }

        private void LastClick(object sender, RoutedEventArgs e)
        {
            if (_scrollPager != null)
            {
                btnHome.IsEnabled = true;
                btnPrev.IsEnabled = true;

                _scrollPager.ScrollToRightEnd();

                btnNext.IsEnabled = false;
                btnLast.IsEnabled = false;
            }
        }
    }
}
