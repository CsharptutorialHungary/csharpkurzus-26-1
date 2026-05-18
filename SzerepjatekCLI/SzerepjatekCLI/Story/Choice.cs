using System;
using System.Collections.Generic;
using System.Text;

namespace SzerepjatekCLI.Story;

using System.Text.Json.Serialization;

public class Choice
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("next")]
    public string Next { get; set; }
}
