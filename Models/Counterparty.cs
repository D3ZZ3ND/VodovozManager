using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodovozManager.Models
{
  public class Counterparty
  {
    public virtual int Id { get; protected set; }
    public virtual string Name { get; set; }
    public virtual string TIN { get; set; }

    /// <summary>
    /// Связь "многие-к-одному"
    /// </summary>
    public virtual Employee Curator { get; set; }
  }
}
