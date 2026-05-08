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
        void HandleAction(string action) //lehet hogy private lesz, mert csak a StoryManager-en belül használjuk
        {
            switch (action)
            {
                case "shop_blacksmith":
                    OpenBlacksmithShop();
                    break;

                case "shop_market":
                    OpenMarketShop();
                    break;

                case "shop_alchemist":
                    OpenAlchemistShop();
                    break;

                case "battle":
                    Fight();
                    break;
            }
        }
        private void OpenBlacksmithShop()
        {
            Console.WriteLine("Kovács bolt megnyitva");

            // item lista
            // vásárlás
            // inventory kezelés
        }

        private void OpenMarketShop()
        {
            Console.WriteLine("Piac megnyitva");
            // item lista
            // vásárlás
            // inventory kezelés
        }
        private void OpenAlchemistShop()
        {
            Console.WriteLine("Alkimista bolt megnyitva");
            // item lista
            // vásárlás
            // inventory kezelés
        }

        private void Fight()
        {
            //harcrendszer
        }
    }
}
