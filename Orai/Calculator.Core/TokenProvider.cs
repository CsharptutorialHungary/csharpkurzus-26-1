using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Transactions;

using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Operations;

using static Calculator.Core.Tokens.Operations.FunctionOperation;

namespace Calculator.Core;

internal class TokenProvider : ITokenProvider
{
    private Dictionary<string, IToken> _tokens;

    public IReadOnlyDictionary<string, IToken> Tokens
        => _tokens;

    public TokenProvider()
    {
        _tokens = new Dictionary<string, IToken>();
        LoadNamedTokens();
        LoadFunctions();
    }

    private void LoadNamedTokens()
    {
        var candidates = typeof(IToken).Assembly.GetTypes()
            .Where(t => !t.IsInterface && !t.IsAbstract)
            .Where(t => t.IsAssignableTo(typeof(IToken)));

        foreach (var candidate in candidates)
        {
            var nameAttribute = candidate.GetCustomAttribute<TokenNameAttribute>();

            if (nameAttribute is not null)
            {
                IToken token = (IToken)Activator.CreateInstance(candidate)!;
                _tokens.Add(nameAttribute.Name, token);
            }

        }
    }

    private void LoadFunctions()
    {
        var candidates = typeof(Math).GetMethods(BindingFlags.Static | BindingFlags.Public);
        foreach (var candidate in candidates)
        {
            if (candidate.ReturnType != typeof(double))
                continue;

            ParameterInfo[] parameters = candidate.GetParameters();

            if (parameters.Length == 1 && parameters[0].ParameterType == typeof(double))
            {
                string name = candidate.Name.ToLower();
                SingleParamFunction function = (SingleParamFunction)Delegate.CreateDelegate(typeof(SingleParamFunction), candidate);
                _tokens.Add(name, new FunctionOperation(function));
            }
        }
    }
}
