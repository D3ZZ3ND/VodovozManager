using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodovozManager.Models
{
  public class Order
  {
    public virtual int Id { get; protected set; }
    public virtual DateTime OrderDate { get; set; }
    public virtual decimal Amount { get; set; }
    public virtual Employee Employee { get; set; }
    public virtual Counterparty Counterparty { get; set; }
  }
}
