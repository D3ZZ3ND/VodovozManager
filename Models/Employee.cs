using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodovozManager.Models;
  public class Employee
  {
    public virtual int Id { get; protected set; }
    public virtual string FullName { get; set; }
    public virtual Position Position { get; set; }
    public virtual DateTime BirthDate { get; set; }
  }