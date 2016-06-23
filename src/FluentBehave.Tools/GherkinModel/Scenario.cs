using System.Collections.Generic;

namespace FluentBehave.Tools.GherkinModel
{
    public class Scenario
    {
        public Scenario(string title)
        {
            Title = title;
            GivenCollection = new List<Sentence>();
            WhenCollection = new List<Sentence>();
            ThenCollection = new List<Sentence>();
        }

        public string Title { get; set; }
        public IList<Sentence> GivenCollection { get; set; }
        public IList<Sentence> WhenCollection { get; set; }
        public IList<Sentence> ThenCollection { get; set; }
    }
}
