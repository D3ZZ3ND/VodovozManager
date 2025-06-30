using System.Collections.Generic;

namespace VodovozManager.Data.Repositories
{
  /// <summary>
  /// Определяет универсальный репозиторий для операций с сущностями.
  /// </summary>
  /// <typeparam name="T">Тип сущности.</typeparam>
  public interface IRepository<T> where T : class
  {
    /// <summary>
    /// Получает сущность по ее идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <returns>Найденная сущность или null.</returns>
    T Get(int id);

    /// <summary>
    /// Получает все сущности указанного типа.
    /// </summary>
    /// <returns>Список всех сущностей.</returns>
    IList<T> GetAll();

    /// <summary>
    /// Сохраняет новую сущность в базе данных.
    /// </summary>
    /// <param name="entity">Сущность для сохранения.</param>
    /// <returns>Сохраненная сущность с присвоенным ID.</returns>
    T Save(T entity);

    /// <summary>
    /// Обновляет существующую сущность в базе данных.
    /// </summary>
    /// <param name="entity">Сущность для обновления.</param>
    void Update(T entity);

    /// <summary>
    /// Удаляет сущность из базы данных.
    /// </summary>
    /// <param name="entity">Сущность для удаления.</param>
    void Delete(T entity);
  }
}