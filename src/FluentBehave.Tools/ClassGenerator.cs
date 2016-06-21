using FluentBehave.Tools.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FluentBehave.Tools
{
    public class ClassGenerator
    {
        class Method
        {
            public Method()
            {
                Parameters = new Dictionary<string, string>();
            }

            public string Title { get; set; }
            public string Name { get; set; }
            public string Prefix { get; set; }
            public Dictionary<string, string> Parameters { get; set; }
        }

        public string Generate(Feature feature, string className, string namespaceName)
        {
            var classTemplate = Templates.ClassTemplate;

            classTemplate = classTemplate.Replace("<%NAMESPACE%>", namespaceName);
            classTemplate = classTemplate.Replace("<%NAME%>", className);
            classTemplate = classTemplate.Replace("<%DESCRIPTION%>", feature.Text);
            classTemplate = classTemplate.Replace("<%CLASSNAME%>", className);

            Dictionary<string, Method> methods = new Dictionary<string, Method>();
            var scenarioBuilder = new StringBuilder();
            foreach(var scenario in feature.ScenarioCollection)
            {
                var scenarioTemplate = Templates.ScenarioTemplate;
                scenarioTemplate = scenarioTemplate.Replace("<%SCENARIOTITLE%>", scenario.Title);
                var scenarioMethodName = GetMethodName(scenario.Title);
                scenarioTemplate = scenarioTemplate.Replace("<%SCENARIONAME%>", scenarioMethodName);

                var methodBodyBuilder = new StringBuilder();
                CreateScenarioMethods(scenario.GivenCollection, GherkinKeywords.Given, methods, methodBodyBuilder);
                CreateScenarioMethods(scenario.WhenCollection, GherkinKeywords.When, methods, methodBodyBuilder);
                CreateScenarioMethods(scenario.ThenCollection, GherkinKeywords.Then, methods, methodBodyBuilder);

                scenarioTemplate = scenarioTemplate.Replace("<%SCENARIOBODY%>", methodBodyBuilder.ToString());
                scenarioBuilder.Append(scenarioTemplate);
            }

            classTemplate = classTemplate.Replace("<%SCENARIOS%>", scenarioBuilder.ToString());

            var methodsBuilder = new StringBuilder();
            foreach (var method in methods)
            {
                var methodTemplate = Templates.MethodTemplate;
                methodTemplate = methodTemplate.Replace("<%METHODTITLE%>", method.Value.Title);
                methodTemplate = methodTemplate.Replace("<%METHODNAME%>", method.Value.Name);
                methodTemplate = methodTemplate.Replace("<%METHODPREFIX%>", method.Value.Prefix);

                string parameters = string.Join(",", method.Value.Parameters.Keys.Select(x => "string " + x));
                methodTemplate = methodTemplate.Replace("<%METHODPARAMS%>", parameters);
                methodsBuilder.Append(methodTemplate);
            }

            classTemplate = classTemplate.Replace("<%METHODS%>", methodsBuilder.ToString());

            return classTemplate;
        }

        private void CreateScenarioMethods(IList<Sentence> sentences, string prefix, Dictionary<string, Method> methods, StringBuilder methodBodyBuilder)
        {
            foreach (var given in sentences)
            {
                var method = GetMethod(given.Text, prefix);
                methodBodyBuilder.AppendLine("            " + method.Name + "(" + string.Join(",", method.Parameters.Values) + ");");

                if(!methods.ContainsKey(method.Name))
                {
                    methods.Add(method.Name, method);
                }
            }
        }

        private Method GetMethod(string title, string prefix)
        {
            var method = new Method();
            method.Prefix = prefix;
            method.Title = title;

            string methodName = GetMethodName(title);

            var stringRegex = new Regex("\"([a-zA-Z0-9!@#$%^&*. ]*)\"");
            var stringMatches = stringRegex.Matches(methodName);
            int index = 0;
            foreach (Match match in stringMatches)
            {
                method.Parameters.Add($"p{index}", match.Value);
                methodName = methodName.Remove(match.Index, match.Length);
                index++;
            }

            method.Name = prefix + methodName;
            return method;
        }

        private string GetMethodName(string title)
        {
            title = title.Replace("-", " ");
            var parts = title.Split(' ');
            var methodName = string.Empty;
            for (int i = 0; i < parts.Length; ++i)
            {
                methodName += UppercaseFirst(parts[i]);
            }

            return methodName;
        }

        private string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}
