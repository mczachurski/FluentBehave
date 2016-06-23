namespace FluentBehave.Tools
{
    public class Templates
    {
        public const string ClassTemplate =
            "using FluentBehave;\r\n" + 
            "using System;\r\n" +
			"\r\n" +
            "namespace <%NAMESPACE%>\r\n" +
            "{\r\n" +
            "    [Feature(\"<%NAME%>\", \"<%DESCRIPTION%>\")]\r\n" +
            "    public class <%CLASSNAME%>\r\n" +
            "    {\r\n" +
			"<%SCENARIOS%>" +
			"<%METHODS%>" +
            "    }\r\n" +
            "}";

        public const string ScenarioTemplate =
            "        [Scenario(\"<%SCENARIOTITLE%>\")]\r\n" +
            "        public void <%SCENARIONAME%>()\r\n" +
            "        {\r\n" +
            "<%SCENARIOBODY%>" +
            "        }\r\n" +
            "\r\n";

        public const string MethodTemplate =
            "        [<%METHODPREFIX%>(\"<%METHODTITLE%>\")]\r\n" +
            "        private void <%METHODNAME%>(<%METHODPARAMS%>)\r\n" +
            "        {\r\n" +
            "            throw new NotImplementedException(\"Implement me!\");\r\n" +
            "        }\r\n" +
            "\r\n";
    }
}
