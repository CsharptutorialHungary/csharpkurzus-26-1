namespace EmberekDB.Core
{
    public record class Ember(string Name, int Age, string Gender)
    {
        public override string ToString()
            => $"Név: {Name}  Kor: {Age}  Nem: {Gender}";
    }

}
