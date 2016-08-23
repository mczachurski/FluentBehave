# FluentBehave

FluentBehave is a simple application which can convert Gherkin scenarios to C# classes. It's written as a .NET CLI Command Tool (very similar to EntityFramework Command Tool).

## Getting started

FluentBehave is easy to use. You have to add to your ```project.json``` information about FluentBehave. Simple ```project.json``` file should look like this: 

```json
{
  "version": "1.0.0",

  "buildOptions": {
    "preserveCompilationContext": true
  },

  "dependencies": {
    "Microsoft.NETCore.App": {
      "version": "1.0.0",
      "type": "platform"
    },
    "dotnet-test-xunit": "1.0.0-rc3-000000-02",
    "xunit": "2.1.0",
    "moq.netcore": "4.4.0-beta8",
    "FluentBehave": "1.0.0-beta4",
    "FluentBehave.Tools": {
      "version": "1.0.0-beta4",
      "type": "build"
    }
  },

  "tools": {
    "FluentBehave.Tools": {
      "version": "1.0.0-beta4",
      "imports": [
        "portable-net45+win8+dnxcore50",
        "portable-net45+win8"
      ]
    }
  },

  "frameworks": {
    "netcoreapp1.0": {
      "imports": [
        "dotnet5.6",
        "dnxcore50",
        "portable-net45+win8"
      ]
    }
  },

  "testRunner": "xunit",

  "tooling": {
    "defaultNamespace": "MyApplication.Specs"
  }
}
```

After restoring all dependencies you can run ```FluentBehave``` from console. 

```console
Usage: dotnet fb [options]
Options:
 -f|--feature <FEATURE_FILE>  Feature file which be translated
 -n|--namespace <NAMESPACE>   Namespace for new C# class
 -o|--output <OUTPUT_DIR>     Directory in which to find outputs
 -c|--class <CLASS_NMAE>      Name of new C# class
 -h|--help                    This help
```

## Example

For below file with Gherkin scenarions:

```gerkin
Feature: Resseting password

    As a user
    I want to reset my password
    So that I can have access to system even if I forgot my old password

Scenario: User have to receive email when he resseting password
Given user with email "allen.nixon@soldoit.test" has access to the system
	And his password is "admin"
When he enters email "allen.nixon@soldoit.test"
	And he requests for email with further instructions
Then he receive email message
	And email contains link to set new password

Scenario: User can change his password
Given Email is sent to existing user "hunter.hansen@soltexon.test"
When he enters email "hunter.hansen@soltexon.test"
	And he enters new password "P@ssword!1"
	And he requests for changing password
Then he can sign-in to system using new credentials

Scenario: User enters not existing email when he resseting password
Given user with email "allen.nixon@soldoit.test" has access to the system
When he enters email "notexisting@unit4.com"
	And he requests for email with further instructions
Then he cannot receive email message from the system

Scenario: User cannot change password without email from the system
Given user with email "allen.nixon@soldoit.test" has access to the system
	And he doesn't have email from system with information
When he enters email "allen.nixon@soldoit.test"
	And he enters new password "P@ssword!1"
	And he requests for changing password
Then the new password is not set

Scenario: User cannot change password when he do mistake in email address
Given Email is sent to existing user "hunter.hansen@soltexon.test"
When he enters email "notexisting@unit4.com"
	And he enters new password "P@ssword!1"
	And he requests for changing password
Then the new password is not set

Scenario: Password not according to policy
Given Email is sent to existing user "hunter.hansen@soltexon.test"
When he enters email "hunter.hansen@soltexon.test"
	And he enters new password "a"
	And he requests for changing password
Then there is message that password is to short
```

We can execute below FluentBehave command:

```bash
$> dotnet fb -f ResettingPassword.feature -n MyApplication.Specs
```

Above command will generate ```ResettingPassword.cs``` file:

```csharp
using FluentBehave;
using System;

namespace MyApplication.Specs
{
    [Feature("ResettigPassword", "Resseting password")]
    public class ResettigPassword
    {
        [Scenario("User have to receive email when he resseting password")]
        public void UserHaveToReceiveEmailWhenHeRessetingPassword()
        {
            GivenUserWithEmailHasAccessToTheSystem("allen.nixon@soldoit.test");
            GivenHisPasswordIs("admin");
            WhenHeEntersEmail("allen.nixon@soldoit.test");
            WhenHeRequestsForEmailWithFurtherInstructions();
            ThenHeReceiveEmailMessage();
            ThenEmailContainsLinkToSetNewPassword();
        }

        [Scenario("User can change his password")]
        public void UserCanChangeHisPassword()
        {
            GivenEmailIsSentToExistingUser("hunter.hansen@soltexon.test");
            WhenHeEntersEmail("hunter.hansen@soltexon.test");
            WhenHeEntersNewPassword("P@ssword!1");
            WhenHeRequestsForChangingPassword();
            ThenHeCanSignInToSystemUsingNewCredentials();
        }

        [Scenario("User enters not existing email when he resseting password")]
        public void UserEntersNotExistingEmailWhenHeRessetingPassword()
        {
            GivenUserWithEmailHasAccessToTheSystem("allen.nixon@soldoit.test");
            WhenHeEntersEmail("notexisting@unit4.com");
            WhenHeRequestsForEmailWithFurtherInstructions();
            ThenHeCannotReceiveEmailMessageFromTheSystem();
        }

        [Scenario("User cannot change password without email from the system")]
        public void UserCannotChangePasswordWithoutEmailFromTheSystem()
        {
            GivenUserWithEmailHasAccessToTheSystem("allen.nixon@soldoit.test");
            GivenHeDoesntHaveEmailFromSystemWithInformation();
            WhenHeEntersEmail("allen.nixon@soldoit.test");
            WhenHeEntersNewPassword("P@ssword!1");
            WhenHeRequestsForChangingPassword();
            ThenTheNewPasswordIsNotSet();
        }

        [Scenario("User cannot change password when he do mistake in email address")]
        public void UserCannotChangePasswordWhenHeDoMistakeInEmailAddress()
        {
            GivenEmailIsSentToExistingUser("hunter.hansen@soltexon.test");
            WhenHeEntersEmail("notexisting@unit4.com");
            WhenHeEntersNewPassword("P@ssword!1");
            WhenHeRequestsForChangingPassword();
            ThenTheNewPasswordIsNotSet();
        }

        [Scenario("Password not according to policy")]
        public void PasswordNotAccordingToPolicy()
        {
            GivenEmailIsSentToExistingUser("hunter.hansen@soltexon.test");
            WhenHeEntersEmail("hunter.hansen@soltexon.test");
            WhenHeEntersNewPassword("a");
            WhenHeRequestsForChangingPassword();
            ThenThereIsMessageThatPasswordIsToShort();
        }

        [Given("User with email has access to the system")]
        private void GivenUserWithEmailHasAccessToTheSystem(string p0)
        {
            throw new NotImplementedException("Implement me!");
        }

        [Given("His password is")]
        private void GivenHisPasswordIs(string p0)
        {
            throw new NotImplementedException("Implement me!");
        }

        [When("He enters email")]
        private void WhenHeEntersEmail(string p0)
        {
            throw new NotImplementedException("Implement me!");
        }

        [When("He requests for email with further instructions")]
        private void WhenHeRequestsForEmailWithFurtherInstructions()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("He receive email message")]
        private void ThenHeReceiveEmailMessage()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("Email contains link to set new password")]
        private void ThenEmailContainsLinkToSetNewPassword()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Given("Email is sent to existing user")]
        private void GivenEmailIsSentToExistingUser(string p0)
        {
            throw new NotImplementedException("Implement me!");
        }

        [When("He enters new password")]
        private void WhenHeEntersNewPassword(string p0)
        {
            throw new NotImplementedException("Implement me!");
        }

        [When("He requests for changing password")]
        private void WhenHeRequestsForChangingPassword()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("He can sign-in to system using new credentials")]
        private void ThenHeCanSignInToSystemUsingNewCredentials()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("He cannot receive email message from the system")]
        private void ThenHeCannotReceiveEmailMessageFromTheSystem()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Given("He doesn't have email from system with information")]
        private void GivenHeDoesntHaveEmailFromSystemWithInformation()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("The new password is not set")]
        private void ThenTheNewPasswordIsNotSet()
        {
            throw new NotImplementedException("Implement me!");
        }

        [Then("There is message that password is to short")]
        private void ThenThereIsMessageThatPasswordIsToShort()
        {
            throw new NotImplementedException("Implement me!");
        }

    }
}
```

Now we have to implement not implemented methods, and our feature will be covered by BDD test.

