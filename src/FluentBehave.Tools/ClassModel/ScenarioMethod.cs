using System.Collections.Generic;

namespace FluentBehave.Tools.ClassModel
{
    public class ScenarioMethod : Method
    {
        public ScenarioMethod()
        {
            Methods = new List<Method>();
        }

        public IList<Method> Methods { get; set; }
    }
}
