using System;
using System.Collections.Generic;
using System.Text;

namespace EmberekDB.Core;

public class EmberManager
{
    public void AddEmber(Ember ember, List<Ember> emberek) {
        emberek.Add(ember);
    }

    public IEnumerable<Ember> Alphabetical(List<Ember> emberek)
        => emberek
        .OrderBy(ember => ember.Name);

    public Ember Youngest(List<Ember> emberek)
        => emberek
        .OrderBy(ember => ember.Age)
        .ThenBy(ember => ember.Name)
        .First();

    public Ember Oldest(List<Ember> emberek)
        => emberek
        .OrderByDescending(ember => ember.Age)
        .ThenBy(ember => ember.Name)
        .First();

    public IEnumerable<Ember> NameSearch(List<Ember> emberek, string name)
        => emberek
        .Where(ember => ember.Name.Contains(name))
        .OrderBy(ember => ember.Name);

    public double AverageAge(List<Ember> emberek) { 
        return emberek.Average(ember => ember.Age);
    }

    public IEnumerable<IGrouping<string, Ember>> ByGender(List<Ember> emberek)
        => emberek
        .GroupBy(ember => ember.Gender);
}
