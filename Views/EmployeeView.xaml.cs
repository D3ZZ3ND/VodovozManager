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
  /// Логика EmployeeView.xaml
  /// </summary>
  public partial class EmployeeView : Window
  {
    public EmployeeView()
    {
      InitializeComponent();
    }
    //public void SetViewModel(EmployeeViewModel viewModel)
    //{
    //  DataContext = viewModel;
    //  viewModel.SaveCommand = new RelayCommand(o =>
    //  {
    //    DialogResult = true;
    //    Close();
    //  });
    //}
  }
}
