using FluentBehave.Tools.Model;
using System.Collections.Generic;

namespace FluentBehave.Tools
{
    public class FeatureParser
    {
        public Feature Parse(string featureText)
        {
            Feature feature = new Feature();
            string[] lines = featureText.Split('\n');

            int lineNumber = 0;
            while(lineNumber < lines.Length)
            {
                var line = lines[lineNumber];

                if(line.StartsWith($"{GherkinKeywords.Feature}:"))
                {
                    feature.Text = line.Replace($"{GherkinKeywords.Feature}:", string.Empty).Trim();
                }

                if(line.StartsWith($"{GherkinKeywords.Scenario}:"))
                {
                    var title = line.Replace($"{GherkinKeywords.Scenario}:", string.Empty).Trim();
                    var scenario = new Scenario(title);
                    feature.ScenarioCollection.Add(scenario);

                    lineNumber = ParseScenario(scenario, lines, lineNumber);
                }

                lineNumber++;
            }

            return feature;
        }

        private int ParseScenario(Scenario scenario, string[] lines, int lineNumber)
        {
            lineNumber++;
            while(lineNumber < lines.Length)
            {
                if(lines[lineNumber].StartsWith(GherkinKeywords.Given))
                {
                    lineNumber = ParseSentences(scenario.GivenCollection, GherkinKeywords.Given, lines, lineNumber);
                }
                else if (lines[lineNumber].StartsWith(GherkinKeywords.When))
                {
                    lineNumber = ParseSentences(scenario.WhenCollection, GherkinKeywords.When, lines, lineNumber);
                }
                else if (lines[lineNumber].StartsWith(GherkinKeywords.Then))
                {
                    lineNumber = ParseSentences(scenario.ThenCollection, GherkinKeywords.Then, lines, lineNumber);
                }
                else
                {
                    break;
                }

                lineNumber++;
            }

            return lineNumber;
        }

        private int ParseSentences(IList<Sentence> sentences, string prefix, string[] lines, int lineNumber)
        {
            var line = lines[lineNumber];
            var givenLine = line.Replace(prefix, string.Empty).Trim();

            var given = new Sentence(givenLine);
            sentences.Add(given);

            lineNumber++;
            while (lineNumber < lines.Length)
            {
                givenLine = lines[lineNumber].Trim();
                if(givenLine.StartsWith($"{GherkinKeywords.And} "))
                {
                    givenLine = givenLine.Replace($"{GherkinKeywords.And} ", string.Empty);
                    given = new Sentence(givenLine);
                    sentences.Add(given);
                }
                else
                {
                    break;
                }

                lineNumber++;
            }

            return --lineNumber;
        }
    }
}
