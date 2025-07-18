﻿using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodovozManager.Data
{
  public class NHibernateHelper
  {
    private static ISessionFactory _sessionFactory;

    private static ISessionFactory SessionFactory
    {
      get
      {
        if (_sessionFactory == null)
        {
          var configuration = new Configuration();

          // hibernate.cfg.xml.
          configuration.Configure(); 
          _sessionFactory = configuration.BuildSessionFactory();
        }
        return _sessionFactory;
      }
    }

    public static ISession OpenSession()
    {
      return SessionFactory.OpenSession();
    }
  }
}
