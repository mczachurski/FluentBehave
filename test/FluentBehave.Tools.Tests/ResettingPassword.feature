Feature: Resseting password

    As a user
    I want to reset my password
    So that I can have access to system even if I forgot my old password

Scenario: User have to receive email when he resseting password
Given user with email "allen.nixon@soldoit.test" has access to the system "some-system"
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
Given user with email "allen.nixon@soldoit.test" has access to the system "some-system"
When he enters email "notexisting@unit4.com"
	And he requests for email with further instructions
Then he cannot receive email message from the system

Scenario: User cannot change password without email from the system
Given user with email "allen.nixon@soldoit.test" has access to the system "some-system"
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