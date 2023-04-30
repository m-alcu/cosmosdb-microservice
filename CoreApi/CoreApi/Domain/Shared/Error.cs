namespace CoreApi.Domain.Shared;

public class Error : IEquatable<Error>
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null");

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }

    public string Message { get; }

    public static implicit operator string(Error error) => error.Code;

    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null) return true;

        if (a is null || b is null) return false;

        return a.Code == b.Code && a.Message == b.Message;
    }

    public static bool operator !=(Error? a, Error? b)
    {
        return !(a == b);
    }

    public bool Equals(Error other)
    {
        if (other is null) return false;
        return Code == other.Code && Message == other.Message;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (!(obj is Error)) return false;
        return Equals((Error)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + Code.GetHashCode();
            hash = hash * 23 + Message.GetHashCode();
            return hash;
        }
    }
}
