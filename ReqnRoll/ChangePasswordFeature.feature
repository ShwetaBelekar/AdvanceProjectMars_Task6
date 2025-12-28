@ChangePassword
Feature: ChangePasswordFeature
As a Project_Mars user
I should be able to change password
So that I can manage my login successfully
A short summary of the feature:This feature uses an external JSON file to drive the creation



@SingleRecord
Scenario: I change password using external JSON data
	Given I load the change password records from the 'Configuration\ChangePassword_TestCases\Password_ChangePassword.json' file
	When I navigate to change password feature
	Then I create new password
	Then The new password should be created successfully
	Then I verify and successfully signin with new password
	Then I navigate to change password feature again and set password back to original password
	Then I see the success message
