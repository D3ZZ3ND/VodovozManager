using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using VodovozManager.Infrastructure;
using VodovozManager.Models;

namespace VodovozManager.ViewModels
{
  public class CounterpartyViewModel : ObservableObject
  {
    public Counterparty Counterparty { get; set; }
    public IEnumerable<Employee> Employees { get; }
    public string Title { get; }
    public ICommand SaveCommand { get; set; }

    public CounterpartyViewModel(Counterparty counterparty, IEnumerable<Employee> employees, string title)
    {
      Counterparty = counterparty;
      Employees = employees;
      Title = title;

      if (Counterparty.Curator != null)
      {
        Counterparty.Curator = Employees.FirstOrDefault(e => e.Id == Counterparty.Curator.Id);
      }
    }
  }
}
