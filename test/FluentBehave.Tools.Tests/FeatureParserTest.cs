using System.IO;
using Xunit;

namespace FluentBehave.Tools.Tests
{
    public class FeatureParserTest
    {
        [Fact]
        public void MustReadFeatureTitle()
        {
            var featureText = File.ReadAllText("ResettingPassword.feature");
            var featureParser = new FeatureParser();

            var feature = featureParser.Parse(featureText);

            Assert.Equal("Resseting password", feature.Text);
        }

        [Fact]
        public void MustReadAllScenarios()
        {
            var featureText = File.ReadAllText("ResettingPassword.feature");
            var featureParser = new FeatureParser();

            var feature = featureParser.Parse(featureText);

            Assert.Equal(6, feature.ScenarioCollection.Count);
        }

        [Fact]
        public void MustReadScenarioTitle()
        {
            var featureText = File.ReadAllText("ResettingPassword.feature");
            var featureParser = new FeatureParser();

            var feature = featureParser.Parse(featureText);

            Assert.Equal("User have to receive email when he resseting password", feature.ScenarioCollection[0].Title);
            Assert.Equal("User can change his password", feature.ScenarioCollection[1].Title);
            Assert.Equal("User enters not existing email when he resseting password", feature.ScenarioCollection[2].Title);
            Assert.Equal("User cannot change password without email from the system", feature.ScenarioCollection[3].Title);
            Assert.Equal("User cannot change password when he do mistake in email address", feature.ScenarioCollection[4].Title);
            Assert.Equal("Password not according to policy", feature.ScenarioCollection[5].Title);
        }

        [Fact]
        public void ScenarioMustContainsGivens()
        {
            var featureText = File.ReadAllText("ResettingPassword.feature");
            var featureParser = new FeatureParser();

            var feature = featureParser.Parse(featureText);

            Assert.Equal(2, feature.ScenarioCollection[0].GivenCollection.Count);
            Assert.Equal("user with email \"allen.nixon@soldoit.test\" has access to the system", feature.ScenarioCollection[0].GivenCollection[0].Text);
            Assert.Equal("his password is \"admin\"", feature.ScenarioCollection[0].GivenCollection[1].Text);
        }

        [Fact]
        public void ScenarioMustContainsWhens()
        {
            var featureText = File.ReadAllText("ResettingPassword.feature");
            var featureParser = new FeatureParser();

            var feature = featureParser.Parse(featureText);

            Assert.Equal(2, feature.ScenarioCollection[0].GivenCollection.Count);
            Assert.Equal("he enters email \"allen.nixon@soldoit.test\"", feature.ScenarioCollection[0].WhenCollection[0].Text);
            Assert.Equal("he requests for email with further instructions", feature.ScenarioCollection[0].WhenCollection[1].Text);
        }

        [Fact]
        public void ScenarioMustContainsThens()
        {
            var featureText = File.ReadAllText("ResettingPassword.feature");
            var featureParser = new FeatureParser();

            var feature = featureParser.Parse(featureText);

            Assert.Equal(2, feature.ScenarioCollection[0].GivenCollection.Count);
            Assert.Equal("he receive email message", feature.ScenarioCollection[0].ThenCollection[0].Text);
            Assert.Equal("email contains link to set new password", feature.ScenarioCollection[0].ThenCollection[1].Text);
        }
    }
}
