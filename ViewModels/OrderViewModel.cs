using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using VodovozManager.Infrastructure;
using VodovozManager.Models;

namespace VodovozManager.ViewModels
{
  public class OrderViewModel : ObservableObject
  {
    public Order Order { get; set; }
    public IEnumerable<Employee> Employees { get; }
    public IEnumerable<Counterparty> Counterparties { get; }
    public string Title { get; }
    public ICommand SaveCommand { get; set; }

    public OrderViewModel(Order order, IEnumerable<Employee> employees, IEnumerable<Counterparty> counterparties, string title)
    {
      Order = order;
      Employees = employees;
      Counterparties = counterparties;
      Title = title;

      // Правильное отображение значений для combobox.
      if (Order.Employee != null)
      {
        Order.Employee = Employees.FirstOrDefault(e => e.Id == Order.Employee.Id);
      }

      if (Order.Counterparty != null)
      {
        Order.Counterparty = Counterparties.FirstOrDefault(c => c.Id == Order.Counterparty.Id);
      }
    }
  }
}
