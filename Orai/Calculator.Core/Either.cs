using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Calculator.Core;

public sealed class Either<TSuccess, TError>
    where TError : Exception
    where TSuccess : struct, INumber<TSuccess>
{
    private readonly TSuccess? _success;
    private readonly bool _isSucces;
    private readonly TError? _error;

    private Either(TSuccess success)
    {
        _success = success;
        _isSucces = true;
    }

    private Either(TError error)
    {
        _error = error; 
        _isSucces = false;
    }

    public static implicit operator Either<TSuccess, TError>(TSuccess success)
    {
        return new Either<TSuccess, TError>(success);
    }

    public static implicit operator Either<TSuccess, TError>(TError error)
    {
        return new Either<TSuccess, TError>(error);
    }

    public bool TryGetSuccess([NotNullWhen(true)] out TSuccess?  success)
    {
        if (_isSucces)
        {
            success = _success!;
            return true;
        }

        success = default;
        return false;
    }

    public bool TryGetError([NotNullWhen(true)]out TError? error)
    {
        if (!_isSucces)
        {
            error = _error!;
            return true; 
        }

        error = default; 
        return false;
    }
}
