using System.Text.Json;

namespace Domain.Common;

public class Error
{
    public string Code { get; set; }
    public string Message { get; set; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }

    public Error? Deserialize(string serialized)
    {
        return JsonSerializer.Deserialize<Error>(serialized);
    }
}

public static class Errors
{
    public static class General
    {
        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $"for id: {id}";
            return new("reord.not.found", $"Record not found '{forId}'");
        }

        public static Error ValueIsInvalid(string? value)
        {
            var result = value ?? "Value";
            return new("value.is.invalid", $"'{value}' is invalid");
        }
    }
}