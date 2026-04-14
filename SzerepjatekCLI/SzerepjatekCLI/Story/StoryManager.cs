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
            var json = File.ReadAllText("Data/story.json");
            var list = JsonSerializer.Deserialize<List<StoryNode>>(json);

            _nodes = list.ToDictionary(n => n.Id);
        }

        public StoryNode GetNode(string id)
        {
            return _nodes[id];
        }
    }
}
