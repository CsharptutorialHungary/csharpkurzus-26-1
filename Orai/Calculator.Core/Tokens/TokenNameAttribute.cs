using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Core.Tokens;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
internal class TokenNameAttribute : Attribute
{
    public string Name { get; }

    public TokenNameAttribute(string name)
        => Name = name;
}
