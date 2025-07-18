# VodovozManager - Тестовое задание

Это десктопное приложение на WPF и NHibernate, разработанное в качестве тестового задания. Приложение представляет собой систему для CRUD-операций (создание, чтение, обновление, удаление) над сущностями: Сотрудники, Контрагенты и Заказы.

## ✨ Технологический стек

* **Платформа:** .NET / C#
* **UI Framework:** WPF
* **Архитектурный паттерн:** MVVM (Model-View-ViewModel)
* **ORM:** NHibernate
* **База данных:** MySQL
* **Внедрение зависимостей (DI):** Microsoft.Extensions.DependencyInjection

## 🚀 Как запустить

1.  **Клонировать репозиторий:**
    ```bash
    git clone <URL вашего репозитория>
    ```

2.  **База данных:**
    * Убедитесь, что у вас установлен и запущен сервер MySQL или MariaDB.
    * Выполните SQL-скрипт для создания базы данных `TestTaskDB` и необходимых таблиц (рекомендуется разместить скрипт в папке `/sql`).

3.  **Конфигурация:**
    * Откройте файл `hibernate.cfg.xml` в корне проекта.
    * Укажите ваши данные для подключения к БД в строке `connection.connection_string`.

4.  **Сборка и запуск:**
    * Откройте решение `.sln` в Visual Studio 2022.
    * Соберите и запустите проект. NuGet автоматически восстановит все зависимости.
