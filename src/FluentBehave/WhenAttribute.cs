using System;

namespace FluentBehave
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class WhenAttribute : Attribute
    {
        private string _title;

        public WhenAttribute(string title)
        {
            _title = title;
        }
    }
}
