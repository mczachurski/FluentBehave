using System.Collections.Generic;

namespace FluentBehave.Tools.ClassModel
{
    public class Method
    {
        public Method()
        {
            Parameters = new Dictionary<string, string>();
        }

        public string Title { get; set; }
        public string ClearTitle { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}
