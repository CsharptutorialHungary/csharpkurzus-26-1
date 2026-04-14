using System;
using System.Collections.Generic;
using System.Text;

namespace SzerepjatekCLI.Story
{
    internal class StoryNode
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public List<Choice> Choices { get; set; }
    }
}
