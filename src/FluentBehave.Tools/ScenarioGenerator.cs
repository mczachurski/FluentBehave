using FluentBehave.Tools.ClassModel;
using FluentBehave.Tools.GherkinModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FluentBehave.Tools
{
    public class ScenarioGenerator
    {
        private readonly Scenario _scenario;

        public ScenarioGenerator(Scenario scenario)
        {
            _scenario = scenario;
        }

        public ScenarioMethod Generate()
        {
            var scenarioMethod = new ScenarioMethod();

            var scenarioTemplate = Templates.ScenarioTemplate;
            scenarioTemplate = scenarioTemplate.Replace("<%SCENARIOTITLE%>", _scenario.Title.UppercaseFirst());
            var scenarioMethodName = GetMethodName(_scenario.Title);
            scenarioTemplate = scenarioTemplate.Replace("<%SCENARIONAME%>", scenarioMethodName);

            var methodBodyBuilder = new StringBuilder();
            CreateScenarioMethods(_scenario.GivenCollection, GherkinKeywords.Given, scenarioMethod, methodBodyBuilder);
            CreateScenarioMethods(_scenario.WhenCollection, GherkinKeywords.When, scenarioMethod, methodBodyBuilder);
            CreateScenarioMethods(_scenario.ThenCollection, GherkinKeywords.Then, scenarioMethod, methodBodyBuilder);

            scenarioTemplate = scenarioTemplate.Replace("<%SCENARIOBODY%>", methodBodyBuilder.ToString());

            scenarioMethod.Body = scenarioTemplate;
            return scenarioMethod;
        }

        private void CreateScenarioMethods(IList<Sentence> sentences, string prefix, ScenarioMethod scenarioMethod, StringBuilder methodBodyBuilder)
        {
            foreach (var given in sentences)
            {
                var method = GetMethod(given.Text, prefix);
                methodBodyBuilder.AppendLine("            " + method.Name + "(" + string.Join(", ", method.Parameters.OrderBy(x => x.Key).Select(x => x.Value)) + ");");
                scenarioMethod.Methods.Add(method);
            }
        }

        private Method GetMethod(string title, string prefix)
        {
            var method = new Method();
            method.Prefix = prefix;
            method.Title = title;
            method.ClearTitle = title;

            var stringRegex = new Regex("\"([a-zA-Z0-9!@#$%^&*-. ]*)\"");
            var stringMatches = stringRegex.Matches(method.ClearTitle);
            int index = stringMatches.Count - 1;
            foreach (Match match in stringMatches.OfType<Match>().OrderByDescending(i => i.Index))
            {
                method.Parameters.Add($"p{index}", match.Value);
                method.ClearTitle = method.ClearTitle.Remove(match.Index, match.Length);
                index--;
            }

            string methodName = GetMethodName(method.ClearTitle);
            method.Name = prefix + methodName;
            method.ClearTitle = method.ClearTitle.Trim().RemoveMultipleSpaces();
            return method;
        }

        private string GetMethodName(string title)
        {
            title = title.Replace("-", " ");
            Regex rgx = new Regex("[^a-zA-Z0-9 ]");
            title = rgx.Replace(title, string.Empty);

            var parts = title.Split(' ');
            var methodName = string.Empty;
            for (int i = 0; i < parts.Length; ++i)
            {
                methodName += parts[i].UppercaseFirst();
            }

            return methodName;
        }
    }
}
