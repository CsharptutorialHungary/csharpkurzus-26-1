using System;
using System.Collections.Generic;
using System.Text;

namespace EmberekDB.Core
{
    public record class Ember
    {
        private readonly string _name;
        private readonly int _age;
        private readonly string _gender;

        public Ember(string name, int age, string gender) {
            _name = name;
            _age = age;
            _gender = gender;
        }

    }
}
