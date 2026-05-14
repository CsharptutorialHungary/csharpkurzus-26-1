using System;
using System.Collections.Generic;
using System.Text;

using SzerepjatekCLI.Items;

namespace SzerepjatekCLI.Entities;

public class Player : Character
{

    public Player()
    {
    }
    public override int Attack()
    { 
        //ide kell majd megírni a harcrendszert vagy innen kell rá hivatkozni. Sőt, ezt kell meghívni a public harcnrendszerből TODO
        return AttackPower;
    }
    public bool isFitInInventory(Item item)
    {
        int totalWeight = 0;
        foreach (var i in Inventory)
        {
            totalWeight += i.Weight;
        }
        return totalWeight + item.Weight <= 100;
    }
}
