using System.Text.Json.Serialization;

namespace FishLibrary.Core;

public record Fish(string Name, DateTime DateOfBuy, string Color)
{
    [JsonIgnore]
    public int Age
    {
        get
        {
            return (DateTime.Now.Date - DateOfBuy.Date).Days;
        }
    }
};
