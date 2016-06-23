namespace FluentBehave.Tools.GherkinModel
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
