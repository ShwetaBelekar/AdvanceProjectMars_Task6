@ProfileDescription
Feature: ProfileDescriptionFeature
As a Project_Mars user
I would like to create, edit and delete ProfileDescription record
So that I can manage Description successfully
A short summary of the feature:This feature uses an external JSON file to drive the creation



@SingleRecord
Scenario: Create valid description using external JSON data
	Given I load the description records from the 'Configuration\ProfileDescription_TestCases\ProfileDescription_CreateValid.json' file
	Given I navigate to Description
	Then I create Description record
	Then I see Description created successfully
