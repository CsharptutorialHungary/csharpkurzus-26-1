namespace SzerepjatekCLI.Items
{
    public enum Money
    {
        Arany,
        Ezust,
        Bronz
    }

    public class MoneyItem : Item
    {
        public int BronzeAmount { get; private set; }
        //public string Type { get; set; }


        public MoneyItem(Money currency, int amount)
        {
            BronzeAmount = currency switch
            {
                Money.Arany => amount * 1000,
                Money.Ezust => amount * 100,
                Money.Bronz => amount,
                _ => 0
            };
            Id = BronzeAmount; // Id-t a pénzösszeg alapján állítjuk be
            Type = "Money";
        }

        public decimal ConvertTo(Money targetCurrency)
        {
            return targetCurrency switch
            {
                Money.Arany => BronzeAmount / 1000m,
                Money.Ezust => BronzeAmount / 100m,
                Money.Bronz => BronzeAmount,
                _ => 0
            };
        }

        public void Add(Money currency, int amount)
        {
            BronzeAmount += currency switch
            {
                Money.Arany => amount * 1000,
                Money.Ezust => amount * 100,
                Money.Bronz => amount,
                _ => 0
            };
        }

        public bool Remove(Money currency, int amount)
        {
            int bronzeToRemove = currency switch
            {
                Money.Arany => amount * 1000,
                Money.Ezust => amount * 100,
                Money.Bronz => amount,
                _ => 0
            };

            if (BronzeAmount < bronzeToRemove)
                return false;

            BronzeAmount -= bronzeToRemove;
            return true;
        }

        public override string ToString()
        {
            int gold = BronzeAmount / 1000;
            int silver = (BronzeAmount % 1000) / 100;
            int bronze = BronzeAmount % 100;

            return $"Ennyi pénzed van most: {gold} arany, {silver} ezüst, {bronze} bronz";
        }
    }
}