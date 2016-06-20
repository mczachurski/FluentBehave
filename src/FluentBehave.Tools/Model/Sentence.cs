using System.Collections.Generic;

namespace FluentBehave.Tools.Model
{
    public class Sentence
    {
        public Sentence(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}
