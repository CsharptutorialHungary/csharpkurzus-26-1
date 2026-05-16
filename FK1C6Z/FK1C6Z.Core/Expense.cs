using System;
using System.Collections.Generic;
using System.Text;

namespace FK1C6Z.Core
{
  public record class Expense(Guid Id, string Category, string Description, decimal Amount, DateTime Date);
}
