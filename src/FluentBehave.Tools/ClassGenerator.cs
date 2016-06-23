using FluentBehave.Tools.ClassModel;
using FluentBehave.Tools.GherkinModel;
using System.Collections.Generic;
using System.Text;

namespace FluentBehave.Tools
{
    public class ClassGenerator
    {
        private readonly Feature _feature;
        private readonly string _className;
        private readonly string _namespaceName;
        private string _classBody;
        private Dictionary<string, Method> _methods;

        public ClassGenerator(Feature feature, string className, string namespaceName)
        {
            _feature = feature;
            _className = className;
            _namespaceName = namespaceName;
        }

        public string Generate()
        {
            _classBody = Templates.ClassTemplate;
            _methods = new Dictionary<string, Method>();

            ReplaceClassProperties();
            GenerateScenanrios();
            GeneratePrivateMethods();

            return _classBody;
        }

        private void ReplaceClassProperties()
        {
            _classBody = _classBody.Replace("<%NAMESPACE%>", _namespaceName);
            _classBody = _classBody.Replace("<%NAME%>", _className);
            _classBody = _classBody.Replace("<%DESCRIPTION%>", _feature.Text.UppercaseFirst());
            _classBody = _classBody.Replace("<%CLASSNAME%>", _className);
        }

        private void GenerateScenanrios()
        {
            var scenarioBuilder = new StringBuilder();
            foreach (var scenario in _feature.ScenarioCollection)
            {
                var scenarioGenerator = new ScenarioGenerator(scenario);
                var scenarioMethod = scenarioGenerator.Generate();

                scenarioBuilder.Append(scenarioMethod.Body);
                foreach (var method in scenarioMethod.Methods)
                {
                    if (!_methods.ContainsKey(method.Name))
                    {
                        _methods.Add(method.Name, method);
                    }
                }
            }

            _classBody = _classBody.Replace("<%SCENARIOS%>", scenarioBuilder.ToString());
        }

        private void GeneratePrivateMethods()
        {
            var methodsBuilder = new StringBuilder();
            foreach (var method in _methods)
            {
                var methodGenerator = new MethodGenerator(method.Value);
                var generated = methodGenerator.Generate();
                methodsBuilder.Append(generated.Body);
            }

            _classBody = _classBody.Replace("<%METHODS%>", methodsBuilder.ToString());
        }
    }
}
