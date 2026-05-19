# Kalkulátor – Aggregáló műveletek bővítése

## Áttekintés

A `Calculator.Core` projektet új **aggregáló műveletekkel** egészítettük ki. Ezek a műveletek a verem összes elemén dolgoznak (nem csak egy vagy két operanduson), így statisztikai és összesítő számításokat is végezhetünk a kalkulátorral.

### Érintett fájlok

| Fájl | Típus |
|------|-------|
| `NumberStackExtensions.cs` | Új fájl |
| `Tokens/Operations/AggregateOperation.cs` | Új fájl |
| `Tokens/Operations/Average.cs` | Új fájl |
| `Tokens/Operations/Maximum.cs` | Új fájl |
| `Tokens/Operations/Minimum.cs` | Új fájl |
| `Tokens/Operations/Median.cs` | Új fájl |
| `Tokens/Operations/Mode.cs` | Új fájl |
| `Tokens/Operations/RootMeanSquare.cs` | Új fájl |
| `Tokens/Operations/Sum.cs` | Új fájl |
| `Tokenizer.cs` | Módosított |
| `NumberStack.cs` | Módosított (formázás) |

---

## 1. Extension member – `PopAll()` (C# 14 / .NET 10)

A `NumberStackExtensions.cs` fájl a C# 14-ben bevezetett **extension member** szintaxist használja, amely az `INumberStack` interfészt bővíti egy `PopAll()` metódussal. Ez a metódus az összes elemet kiveszi a veremből és egy `IReadOnlyList<double>` kollekcióként adja vissza.

```csharp
namespace Calculator.Core;

internal static class NumberStackExtensions
{
    extension(INumberStack stack)
    {
        public IReadOnlyList<double> PopAll()
        {
            double[] numbers = new double[stack.Count];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = stack.Pop();
            }

            return numbers;
        }
    }
}
```

### Fontos tudnivalók

- Az `extension(INumberStack stack)` blokk egy **C# 14 nyelvi újdonság**, amely leváltja a korábbi `static` extension method szintaxist.
- A metódus `internal`, tehát csak a projekten belül érhető el.
- A visszatérési típus `IReadOnlyList<double>`, ami biztosítja, hogy a hívó fél nem módosíthatja az eredményt.

---

## 2. Absztrakt ősosztály – `AggregateOperation`

Az `AggregateOperation` egy absztrakt osztály, amely az `Operation` osztályból származik. Ez szolgál alapul az összes aggregáló műveletnek.

```csharp
internal abstract class AggregateOperation : Operation
{
    public override void Apply(INumberStack stack)
    {
        if (stack.Count == 0)
        {
            throw new InvalidOperationException("Not enough values on stack");
        }

        IReadOnlyList<double> values = stack.PopAll();

        double result = Apply(values);

        stack.Push(result);
    }

    protected abstract double Apply(IReadOnlyList<double> values);
}
```

### Működési elv

1. Ellenőrzi, hogy van-e elem a veremben.
2. A `PopAll()` extension metódussal kiveszi az összes elemet.
3. Meghívja a leszármazott osztály `Apply(IReadOnlyList<double>)` metódusát.
4. Az eredményt visszahelyezi a verembe.

### Tervezési minták

- **Template Method pattern** – az `Apply(INumberStack)` metódus definiálja az algoritmust, a konkrét számítást a leszármazottak valósítják meg.
- Az osztályhierarchia: `Operation` → `AggregateOperation` → konkrét műveletek (pl. `Sum`, `Average`, stb.)

---

## 3. Konkrét aggregáló műveletek

Minden konkrét osztály `sealed` és az `AggregateOperation`-ből származik. Mindegyik a `OperationPrecedence.FunctionCall` (3) precedenciával rendelkezik.

### 3.1. `Sum` – Összeg

```csharp
internal sealed class Sum : AggregateOperation
{
    public override int Precedence => OperationPrecedence.FunctionCall;

    protected override double Apply(IReadOnlyList<double> values) => values.Sum();
}
```

Kulcsszó a tokenizálásban: `sum`

---

### 3.2. `Average` – Átlag

```csharp
internal sealed class Average : AggregateOperation
{
    public override int Precedence => OperationPrecedence.FunctionCall;

    protected override double Apply(IReadOnlyList<double> values) => values.Average();
}
```

Kulcsszó a tokenizálásban: `avg`

---

### 3.3. `Maximum` – Maximum

```csharp
internal sealed class Maximum : AggregateOperation
{
    public override int Precedence => OperationPrecedence.FunctionCall;

    protected override double Apply(IReadOnlyList<double> values) => values.Max();
}
```

Kulcsszó a tokenizálásban: `max`

---

### 3.4. `Minimum` – Minimum

```csharp
internal sealed class Minimum : AggregateOperation
{
    public override int Precedence => OperationPrecedence.FunctionCall;

    protected override double Apply(IReadOnlyList<double> values) => values.Min();
}
```

Kulcsszó a tokenizálásban: `min`

---

### 3.5. `Median` – Medián

A medián a rendezett adathalmaz középső értéke. Páros elemszám esetén a két középső elem átlagát adja vissza.

```csharp
internal sealed class Median : AggregateOperation
{
    public override int Precedence => OperationPrecedence.FunctionCall;

    protected override double Apply(IReadOnlyList<double> values)
    {
        List<double> sortedValues = [.. values.Order()];

        int valueCount = sortedValues.Count;
        int middleIndex = valueCount / 2;

        return valueCount % 2 == 0
            ? sortedValues.Skip(middleIndex - 1).Take(2).Average()
            : sortedValues[middleIndex];
    }
}
```

#### Megjegyzések

- A `[.. values.Order()]` szintaxis a **collection expression** és a **spread operátor** (`..`) kombinációja (C# 12+).
- Az `Order()` LINQ metódus rendezi az elemeket növekvő sorrendbe.
- Páros elemszámnál: a középső két elem átlagát veszi.
- Páratlan elemszámnál: a középső elemet adja vissza.

Kulcsszó a tokenizálásban: `median`

---

### 3.6. `Mode` – Módusz

A módusz a leggyakrabban előforduló érték. Azonos gyakoriság esetén a kisebb értéket adja vissza.

```csharp
internal sealed class Mode : AggregateOperation
{
    public override int Precedence => OperationPrecedence.FunctionCall;

    protected override double Apply(IReadOnlyList<double> values)
    {
        return values
            .GroupBy(value => value)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key)
            .First()
            .Key;
    }
}
```

#### Megjegyzések

- A `GroupBy` csoportosítja az azonos értékeket.
- Az `OrderByDescending(group => group.Count())` a leggyakoribb csoportot helyezi előre.
- A `ThenBy(group => group.Key)` biztosítja, hogy azonos gyakoriság esetén a kisebb szám kerüljön kiválasztásra.

Kulcsszó a tokenizálásban: `mode`

---

### 3.7. `RootMeanSquare` – Négyzetes középérték (RMS)

Az RMS képlete: `√(Σxᵢ² / n)`

```csharp
internal sealed class RootMeanSquare : AggregateOperation
{
    public override int Precedence => OperationPrecedence.FunctionCall;

    protected override double Apply(IReadOnlyList<double> values)
    {
        double sumOfSquares = values.Sum(value => value * value);

        return Math.Sqrt(sumOfSquares / values.Count);
    }
}
```

Kulcsszó a tokenizálásban: `rms`

---

## 4. Tokenizer regisztráció

A `Tokenizer.cs` fájlban az új műveletek kulcsszavait regisztráltuk a `_tokens` szótárba:

```csharp
["avg"] = new Average(),
["max"] = new Maximum(),
["median"] = new Median(),
["min"] = new Minimum(),
["mode"] = new Mode(),
["rms"] = new RootMeanSquare(),
["sum"] = new Sum()
```

Ez lehetővé teszi, hogy a felhasználó a kalkulátorban ezeket a kulcsszavakat használja a kifejezésekben.

---

## 5. Osztályhierarchia összefoglalás

```
Operation (absztrakt)
├── BinaryOperation (absztrakt) – pl. Addition, Subtraction, ...
├── UnaryOperation / FunctionOperation – pl. sin, cos, ...
└── AggregateOperation (absztrakt) ← ÚJ
    ├── Sum
    ├── Average
    ├── Maximum
    ├── Minimum
    ├── Median
    ├── Mode
    └── RootMeanSquare
```

---

## 6. Felhasznált C# nyelvi elemek összefoglalása

| Nyelvi elem | Verzió | Példa a kódban |
|---|---|---|
| Extension member (`extension(T)` blokk) | C# 14 | `NumberStackExtensions.cs` |
| Collection expression + spread operátor | C# 12 | `[.. values.Order()]` a `Median`-ban |
| Expression-bodied member | C# 6+ | `=> values.Sum()` az `Average`-ben |
| `sealed` osztályok | C# 1+ | Minden konkrét aggregáló művelet |
| Template Method tervezési minta | – | `AggregateOperation.Apply()` |
| LINQ láncolás | C# 3+ | `GroupBy`, `OrderByDescending`, `ThenBy` a `Mode`-ban |
