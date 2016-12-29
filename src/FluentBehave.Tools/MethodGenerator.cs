using FluentBehave.Tools.ClassModel;
using System.Linq;

namespace FluentBehave.Tools
{
    public class MethodGenerator
    {
        private readonly Method _method;

        public MethodGenerator(Method method)
        {
            _method = method;
        }

        public Method Generate()
        {
            var methodTemplate = Templates.MethodTemplate;
            methodTemplate = methodTemplate.Replace("<%METHODTITLE%>", _method.ClearTitle.UppercaseFirst());
            methodTemplate = methodTemplate.Replace("<%METHODNAME%>", _method.Name);
            methodTemplate = methodTemplate.Replace("<%METHODPREFIX%>", _method.Prefix);

            string parameters = string.Join(", ", _method.Parameters.Keys.OrderBy(x => x).Select(x => "string " + x));
            methodTemplate = methodTemplate.Replace("<%METHODPARAMS%>", parameters);

            _method.Body = methodTemplate;
            return _method;
        }
    }
}
