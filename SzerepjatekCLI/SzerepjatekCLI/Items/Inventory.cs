using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzerepjatekCLI.Items;

public class Inventory
{
    public int MaxWeight { get;  } = 100;
    public List<Item> Backpack { get; set; } = new List<Item>();
    public int CurrentWeight => Backpack.Sum(i => i.Weight);

    public void Add(Item item)
    {
        Backpack.Add(item);
    }
    public void Remove(Item item)
    {
        Backpack.Remove(item);
    }
    public bool Contains(Item item)
    {
        return Backpack.Contains(item);
    }
    public void AddMoney(Money money, int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.", nameof(amount));
        }
        var moneyItem = Backpack.OfType<MoneyItem>().FirstOrDefault();
        if (moneyItem != null)
        {
            moneyItem.Add(money, amount);
        }
        else
        {
            Backpack.Add(new MoneyItem(money, amount));
        }
    }
    public void RemoveMoney(Money money, int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.", nameof(amount));
        }
        var moneyItem = Backpack.OfType<MoneyItem>().FirstOrDefault();
        if (moneyItem != null)
        {
            moneyItem.Remove(money, amount);
            if (moneyItem.ConvertTo(Money.Bronz) == 0)
            {
                Backpack.Remove(moneyItem);
            }
        }
    }
    public bool HasEnoughMoney(Money money, int amount)
    {
        var moneyItem = Backpack.OfType<MoneyItem>().FirstOrDefault();
        if (moneyItem == null)
            return false;
        return moneyItem.ConvertTo(money) >= amount;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Inventory:");
        sb.AppendLine(string.Join(Environment.NewLine, Backpack.OrderBy(i => i.GetType().Name)));
        sb.AppendLine($"Total Weight: {CurrentWeight}/{MaxWeight}");
        return sb.ToString();
    }
}
