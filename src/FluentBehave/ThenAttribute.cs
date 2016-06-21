using System;

namespace FluentBehave
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ThenAttribute : Attribute
    {
        private string _title;

        public ThenAttribute(string title)
        {
            _title = title;
        }
    }
}
