using System;
using System.Collections.Generic;
using System.Text;

namespace SzerepjatekCLI.Story
{
    using System.Text.Json.Serialization;

    record class StoryNode
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("content")]
        public string Text { get; set; }

        [JsonPropertyName("action")]
        public string? Action { get; set; }

        [JsonPropertyName("choices")]
        public List<Choice> Choices { get; set; }
    }
}
