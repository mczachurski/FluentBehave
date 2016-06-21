using System;

namespace FluentBehave.Tools
{
    public class Templates
    {
        public const string ClassTemplate =
            "using FluentBehaviour;\n" + 
            "using System;\n" +
			"\n" +
            "namespace <%NAMESPACE%>\n" +
            "{\n" +
            "\n" +
            "    [Feature(\"<%NAME%>\", \"<%DESCRIPTION%>\")]\n" +
            "    public class <%CLASSNAME%>\n" +
            "    {\n" +
            "\n" +
			"<%SCENARIOS%>" +
			"<%METHODS%>" +
            "    }\n" +
            "}";

        public const string ScenarioTemplate =
            "        [Scenario(\"<%SCENARIOTITLE%>\")]\n" +
            "        public void <%SCENARIONAME%>()\n" +
            "        {\n" +
            "<%SCENARIOBODY%>" +
            "        }\n" +
            "\n";

        public const string MethodTemplate =
            "        [<%METHODPREFIX%>(\"<%METHODTITLE%>\")]\n" +
            "        private void <%METHODNAME%>(<%METHODPARAMS%>)\n" +
            "        {\n" +
            "            throw new NotImplementedException(\"Implement me!\");\n" +
            "        }\n" +
            "\n";
    }
}
