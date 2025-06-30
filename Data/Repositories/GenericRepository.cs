using NHibernate;
using NHibernate.Exceptions;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VodovozManager.Models;

namespace VodovozManager.Data.Repositories
{
  /// <summary>
  /// Реализация универсального репозитория с NHibernate.
  /// </summary>
  public class GenericRepository<T> : IRepository<T> where T : class
  {
    private readonly ISessionFactory _sessionFactory;

    public GenericRepository(ISessionFactory sessionFactory)
    {
      _sessionFactory = sessionFactory;
    }

    public T Get(int id)
    {
      using (var session = _sessionFactory.OpenSession())
      {
        return session.Get<T>(id);
      }
    }

    public IList<T> GetAll()
    {
      using (var session = _sessionFactory.OpenSession())
      {
        IQueryable<T> query = session.Query<T>();

        if (typeof(T) == typeof(Order))
        {
          query = (IQueryable<T>)session.Query<Order>()
                                        .Fetch(o => o.Employee)
                                        .Fetch(o => o.Counterparty);
        }
        else if (typeof(T) == typeof(Counterparty))
        {
          query = (IQueryable<T>)session.Query<Counterparty>()
                                        .Fetch(c => c.Curator);
        }

        return query.ToList();
      }
    }

    public T Save(T entity)
    {
      using (var session = _sessionFactory.OpenSession())
      using (var transaction = session.BeginTransaction())
      {
        session.Save(entity);
        transaction.Commit();
        return entity;
      }
    }

    public void Update(T entity)
    {
      using (var session = _sessionFactory.OpenSession())
      using (var transaction = session.BeginTransaction())
      {
        session.Update(entity);
        transaction.Commit();
      }
    }

    /// <summary>
    /// Удаляет сущность, перехватывая возможные исключения нарушения целостности БД.
    /// </summary>
    public void Delete(T entity)
    {
      try
      {
        using (var session = _sessionFactory.OpenSession())
        using (var transaction = session.BeginTransaction())
        {
          session.Delete(entity);
          transaction.Commit();
        }
      }
      catch (GenericADOException ex)
      {
        throw new InvalidOperationException($"Произошла ошибка при удалении. Возможно, на эту запись ссылаются другие сущности (например, заказ ссылается на этого сотрудника).", ex);
      }
    }
  }
}