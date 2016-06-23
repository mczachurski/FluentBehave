using System.IO;
using Xunit;

namespace FluentBehave.Tools.Tests
{
    public class ClassGeneratorTest
    {
        [Fact]
        public void ClassGeneratorHaveToGenerateCorrectClass()
        {
            var featureText = File.ReadAllText("UnitTest.feature");
            var featureParser = new FeatureParser();
            var feature = featureParser.Parse(featureText);

            var classGenerator = new ClassGenerator(feature, "UnitTest", "FluentBehave.Test");
            var classBody = classGenerator.Generate();

            var classText = File.ReadAllText("UnitTest.txt");
            Assert.Equal(classText, classBody);
        }
    }
}
