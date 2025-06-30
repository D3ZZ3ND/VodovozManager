using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using VodovozManager.Infrastructure;
using VodovozManager.Models;

namespace VodovozManager.ViewModels
{
  public class EmployeeViewModel : ObservableObject
  {
    public Employee Employee { get; set; }
    public string Title { get; }
    public ICommand SaveCommand { get; set; }

    public IEnumerable<Position> Positions => Enum.GetValues(typeof(Position)).Cast<Position>();

    public EmployeeViewModel(Employee employee, string title)
    {
      Employee = employee;
      Title = title;
      SaveCommand = new RelayCommand(SaveChanges);
    }

    private void SaveChanges(object obj)
    {
      // TODO: валидация.
    }
  }
}
