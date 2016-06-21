using System;

namespace FluentBehave
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class GivenAttribute : Attribute
    {
        private string _title;

        public GivenAttribute(string title)
        {
            _title = title;
        }
    }
}
