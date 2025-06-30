using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using VodovozManager.Data.Repositories;
using VodovozManager.Infrastructure;
using VodovozManager.Models;
using VodovozManager.Views;

namespace VodovozManager.ViewModels
{
  public class MainViewModel : ObservableObject
  {
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<Counterparty> _counterpartyRepository;
    private readonly IRepository<Order> _orderRepository;

    private Employee _selectedEmployee;
    private Counterparty _selectedCounterparty;
    private Order _selectedOrder;

    public ObservableCollection<Employee> Employees { get; set; }
    public Employee SelectedEmployee
    {
      get => _selectedEmployee;
      set
      {
        _selectedEmployee = value;
        OnPropertyChanged();
      }
    }

    public ObservableCollection<Counterparty> Counterparties { get; set; }
    public Counterparty SelectedCounterparty
    {
      get => _selectedCounterparty;
      set
      {
        _selectedCounterparty = value;
        OnPropertyChanged();
      }
    }

    public ObservableCollection<Order> Orders { get; set; }
    public Order SelectedOrder
    {
      get => _selectedOrder;
      set
      {
        _selectedOrder = value;
        OnPropertyChanged();
      }
    }

    public ICommand AddEmployeeCommand { get; }
    public ICommand EditEmployeeCommand { get; }
    public ICommand DeleteEmployeeCommand { get; }

    public ICommand AddCounterpartyCommand { get; }
    public ICommand EditCounterpartyCommand { get; }
    public ICommand DeleteCounterpartyCommand { get; }

    public ICommand AddOrderCommand { get; }
    public ICommand EditOrderCommand { get; }
    public ICommand DeleteOrderCommand { get; }

    public MainViewModel(
        IRepository<Employee> employeeRepository,
        IRepository<Counterparty> counterpartyRepository,
        IRepository<Order> orderRepository)
    {
      _employeeRepository = employeeRepository;
      _counterpartyRepository = counterpartyRepository;
      _orderRepository = orderRepository;

      try
      {
        LoadData();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Не удалось загрузить данные из базы данных. Проверьте подключение.\n\nОшибка: {ex.Message}", "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      AddEmployeeCommand = new RelayCommand(AddEmployee);
      EditEmployeeCommand = new RelayCommand(EditEmployee, o => SelectedEmployee != null);
      DeleteEmployeeCommand = new RelayCommand(DeleteEmployee, o => SelectedEmployee != null);

      AddCounterpartyCommand = new RelayCommand(AddCounterparty, o => Employees.Count > 0);
      EditCounterpartyCommand = new RelayCommand(EditCounterparty, o => SelectedCounterparty != null);
      DeleteCounterpartyCommand = new RelayCommand(DeleteCounterparty, o => SelectedCounterparty != null);

      AddOrderCommand = new RelayCommand(AddOrder, o => Employees.Count > 0 && Counterparties.Count > 0);
      EditOrderCommand = new RelayCommand(EditOrder, o => SelectedOrder != null);
      DeleteOrderCommand = new RelayCommand(DeleteOrder, o => SelectedOrder != null);
    }

    private void LoadData()
    {
      Employees = new ObservableCollection<Employee>(_employeeRepository.GetAll());
      Counterparties = new ObservableCollection<Counterparty>(_counterpartyRepository.GetAll());
      Orders = new ObservableCollection<Order>(_orderRepository.GetAll());
    }

    private void AddEmployee(object obj)
    {
      try
      {
        var newEmployee = new Employee { BirthDate = System.DateTime.Now };
        var vm = new EmployeeViewModel(newEmployee, "Новый сотрудник");
        var view = new EmployeeView { DataContext = vm };
        vm.SaveCommand = new RelayCommand(o => { view.DialogResult = true; view.Close(); });

        if (view.ShowDialog() == true)
        {
          var saved = _employeeRepository.Save(vm.Employee);
          Employees.Add(saved);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при добавлении сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
    private void EditEmployee(object obj)
    {
      try
      {
        var employeeToEdit = _employeeRepository.Get(SelectedEmployee.Id);
        var vm = new EmployeeViewModel(employeeToEdit, "Редактирование сотрудника");
        var view = new EmployeeView { DataContext = vm };
        vm.SaveCommand = new RelayCommand(o => { view.DialogResult = true; view.Close(); });

        if (view.ShowDialog() == true)
        {
          _employeeRepository.Update(vm.Employee);
          var updated = _employeeRepository.Get(vm.Employee.Id);
          int index = Employees.IndexOf(SelectedEmployee);
          Employees[index] = updated;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при редактировании сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void DeleteEmployee(object obj)
    {
      if (MessageBox.Show($"Удалить сотрудника: {SelectedEmployee.FullName}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
        return;

      try
      {
        _employeeRepository.Delete(SelectedEmployee);
        Employees.Remove(SelectedEmployee);
      }
      catch (InvalidOperationException ex)
      {
        MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void AddCounterparty(object obj)
    {
      try
      {
        var newCounterparty = new Counterparty();
        var vm = new CounterpartyViewModel(newCounterparty, Employees, "Новый контрагент");
        var view = new CounterpartyView { DataContext = vm };
        vm.SaveCommand = new RelayCommand(o => { view.DialogResult = true; view.Close(); });

        if (view.ShowDialog() == true)
        {
          var saved = _counterpartyRepository.Save(vm.Counterparty);
          Counterparties.Add(saved);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при добавлении контрагента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void EditCounterparty(object obj)
    {
      try
      {
        var counterpartyToEdit = _counterpartyRepository.Get(SelectedCounterparty.Id);
        var vm = new CounterpartyViewModel(counterpartyToEdit, Employees, "Редактирование контрагента");
        var view = new CounterpartyView { DataContext = vm };
        vm.SaveCommand = new RelayCommand(o => { view.DialogResult = true; view.Close(); });

        if (view.ShowDialog() == true)
        {
          _counterpartyRepository.Update(vm.Counterparty);
          var updated = _counterpartyRepository.Get(vm.Counterparty.Id);
          int index = Counterparties.IndexOf(SelectedCounterparty);
          Counterparties[index] = updated;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при редактировании контрагента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void DeleteCounterparty(object obj)
    {
      if (MessageBox.Show($"Удалить контрагента: {SelectedCounterparty.Name}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
        return;

      try
      {
        _counterpartyRepository.Delete(SelectedCounterparty);
        Counterparties.Remove(SelectedCounterparty);
      }
      catch (InvalidOperationException ex)
      {
        MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void AddOrder(object obj)
    {
      try
      {
        var newOrder = new Order { OrderDate = System.DateTime.Now };
        var vm = new OrderViewModel(newOrder, Employees, Counterparties, "Новый заказ");
        var view = new OrderView { DataContext = vm };
        vm.SaveCommand = new RelayCommand(o => { view.DialogResult = true; view.Close(); });

        if (view.ShowDialog() == true)
        {
          var saved = _orderRepository.Save(vm.Order);
          Orders.Add(saved);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при добавлении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void EditOrder(object obj)
    {
      try
      {
        var orderToEdit = _orderRepository.Get(SelectedOrder.Id);
        var vm = new OrderViewModel(orderToEdit, Employees, Counterparties, "Редактирование заказа");
        var view = new OrderView { DataContext = vm };
        vm.SaveCommand = new RelayCommand(o => { view.DialogResult = true; view.Close(); });

        if (view.ShowDialog() == true)
        {
          _orderRepository.Update(vm.Order);
          var updated = _orderRepository.Get(vm.Order.Id);
          int index = Orders.IndexOf(SelectedOrder);
          Orders[index] = updated;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при редактировании заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void DeleteOrder(object obj)
    {
      if (MessageBox.Show($"Удалить заказ ID: {SelectedOrder.Id}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
        return;

      try
      {
        _orderRepository.Delete(SelectedOrder);
        Orders.Remove(SelectedOrder);
      }
      catch (InvalidOperationException ex)
      {
        MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
  }
}
