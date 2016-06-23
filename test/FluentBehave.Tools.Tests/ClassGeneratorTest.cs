using System.IO;
using Xunit;

namespace FluentBehave.Tools.Tests
{
    public class ClassGeneratorTest
    {
        [Fact]
        public void ClassGeneratorHaveToGenerateCorrectClassForResettingPasswordFeature()
        {
            var featureText = File.ReadAllText("ResettingPassword.feature");
            var featureParser = new FeatureParser();
            var feature = featureParser.Parse(featureText);

            var classGenerator = new ClassGenerator(feature, "ResettigPassword", "FluentBehave.Test");
            var classBody = classGenerator.Generate();

            var classText = File.ReadAllText("ResettingPassword.txt");
            Assert.Equal(classText, classBody);
        }

        [Fact]
        public void ClassGeneratorHaveToGenerateCorrectClassForErrorListFeature()
        {
            var featureText = File.ReadAllText("ErrorsList.feature");
            var featureParser = new FeatureParser();
            var feature = featureParser.Parse(featureText);

            var classGenerator = new ClassGenerator(feature, "ErrorsList", "FluentBehave.Test");
            var classBody = classGenerator.Generate();

            var classText = File.ReadAllText("ErrorsList.txt");
            Assert.Equal(classText, classBody);
        }
    }
}
