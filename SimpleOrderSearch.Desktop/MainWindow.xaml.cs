using ReactiveUI.Validation.Extensions;
using ReactiveUI;
using System;
using System.Collections.Generic;
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
using System.Reactive.Disposables;

namespace SimpleOrderSearch.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    //public partial class MainWindow : ReactiveWindow<MainViewModel>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ////this.ViewModel = new MainViewModel();
            ////this.DataContext = this.ViewModel;
            this.DataContext = new MainViewModel();
                      
            ////this.WhenActivated(disposable =>
            ////{
            ////    this.Bind(ViewModel,
            ////  viewModel => viewModel.OrderNo,
            ////  view => view.TxtBoxOrderNo.Text)
            ////  .DisposeWith(disposable);

            ////    this.BindValidation(ViewModel, vm => vm.OrderNumberRule, v => v.TxtBoxOrderNo.Text)
            ////        .DisposeWith(disposable);
            ////});
        }
    }
}
