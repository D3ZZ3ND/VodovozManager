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
using System.Windows.Shapes;
using VodovozManager.Infrastructure;
using VodovozManager.ViewModels;

namespace VodovozManager.Views
{
  /// <summary>
  /// Логика OrderView.xaml
  /// </summary>
  public partial class OrderView : Window
  {
    public OrderView()
    {
      InitializeComponent();
    }

    ////public void SetViewModel(OrderViewModel viewModel)
    ////{
    ////  DataContext = viewModel;
    ////  viewModel.SaveCommand = new RelayCommand(o =>
    ////  {
    ////    DialogResult = true;
    ////    Close();
    ////  });
    ////}
  }
}
