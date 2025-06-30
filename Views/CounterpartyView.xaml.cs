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
  /// Interaction logic for CounterpartyView.xaml
  /// </summary>
  public partial class CounterpartyView : Window
  {
    public CounterpartyView()
    {
      InitializeComponent();
    }
    // Этот метод не обязателен, но может быть полезен
    public void SetViewModel(CounterpartyViewModel viewModel)
    {
      DataContext = viewModel;
      viewModel.SaveCommand = new RelayCommand(o =>
      {
        DialogResult = true;
        Close();
      });
    }
  }
}
