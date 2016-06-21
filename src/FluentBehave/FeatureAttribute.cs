using System;
using Xunit.Sdk;

namespace FluentBehave
{
    [TraitDiscoverer("Xunit.Sdk.TraitDiscoverer", "xunit.core")]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FeatureAttribute : Attribute, ITraitAttribute
    {
        public FeatureAttribute(string name, string value) { }
    }
}
