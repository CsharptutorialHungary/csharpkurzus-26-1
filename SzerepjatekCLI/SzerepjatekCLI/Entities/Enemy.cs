using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzerepjatekCLI.Entities;

internal class Enemy : Character
{
    public Enemy()
    {
    }
    public override int Attack()
    {
        //ide kell majd megírni a harcrendszert vagy innen kell rá hivatkozni. Sőt, ezt kell meghívni a public harcnrendszerből TODO
        return AttackPower;
    }
}
