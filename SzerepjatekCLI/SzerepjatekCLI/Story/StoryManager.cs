using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace SzerepjatekCLI.Story
{
    internal class StoryManager
    {
        private Dictionary<string, StoryNode> _nodes;

        public StoryManager()
        {
            LoadStory();
        }

        private void LoadStory()
        {
            try
            {
                //AppContext.BaseDirectory
                var json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Data", "story.json"));
                using var doc = JsonDocument.Parse(json);
                var storyArray = doc.RootElement.GetProperty("story").GetRawText();
                var list = JsonSerializer.Deserialize<List<StoryNode>>(storyArray);
                _nodes = list
                    .Where(n => !string.IsNullOrWhiteSpace(n.Id))
                    .ToDictionary(n => n.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading story: {ex.Message}");
            }
        }

        public StoryNode GetNode(string id)
        {
            if (_nodes.TryGetValue(id, out var node))
            {
                return node;
            }
            throw new Exception("Story node not found");
        }

        public bool IsEndNode(string id)
        {
            var node = GetNode(id);
            return node.Choices == null || node.Choices.Count == 0;
        }
    }
}
