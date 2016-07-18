Feature: Errors list

	As a administrator
	I want to check a list of errors which ware thrown in application
	So that I can fix issues that occured in the system

Scenario: Administrator has list of throwned exceptions
Given user with email "admin@unit4.com" has "Administrator" role
	And in the database there are exceptions
When he requests for the list of exceptions
Then application returns list of existing exceptions
	And he can check date when exception occured
	And he can check type of occured exception
	And he can check email of user who get the exception

Scenario: Administrator can check exception's details
Given user with email "admin@unit4.com" has "Administrator" role
	And in the database there are exceptions
When he requests for exception's details
Then application returns exception's details
	And he can check date when exception occured
	And he can check type of occured exception
	And he can check email of user who get the exception
	And he can check stack trace of occured exception

Scenario: Access to list of exceptions should be restricted only to administrator
Given User with email "deann.bender@unit4.test" has "Consultant" role
	And in the database there are exceptions
When he requests for the list of exceptions
Then he doesn't have access to data

Scenario: Access to exception's details should be restricted only to administrator
Given User with email "deann.bender@unit4.test" has "Consultant" role
	And in the database there are exceptions
When he requests for exception's details
Then he doesn't have access to data