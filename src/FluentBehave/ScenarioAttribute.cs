using System;
using Xunit;
using Xunit.Sdk;

namespace FluentBehave
{
    [XunitTestCaseDiscoverer("Xunit.Sdk.FactDiscoverer", "xunit.execution.{Platform}")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ScenarioAttribute : FactAttribute
    {
        public ScenarioAttribute(string title) : base()
        {
            DisplayName = title;
        }
    }
}
