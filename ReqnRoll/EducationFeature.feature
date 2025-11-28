@EducationDataDriven
Feature: EducationFeature
As a Project_Mars user
I would like to create, edit and delete education records
So that I can manage education successfully
A short summary of the feature:This feature uses an external JSON file to drive the creation


#Scenario Outline: create education record with valid data
#	Given  I navigate to Education
#	When  I create a '<CollegeUniversityName>' and '<CountryofCollegeUniversity>' and '<Title>' and '<Degree>' and '<YearofGraduation>' education record
#	Then  the record for '<CollegeUniversityName>' and '<CountryofCollegeUniversity>' and '<Title>' and '<Degree>' and '<YearofGraduation>' education should be created successfully 
#	Examples: 
#	| CollegeUniversityName | CountryofCollegeUniversity | Title     | Degree    | YearofGraduation |
#	| Mumbai University     | India                      | PHD       | Economics |             2007 |
#	| Victoria University   | New Zealand                | Associate | Arts      |             2024 |
#	| NYC College           | United States              | MFA       | Science   |             2001 |
#	| Model College         | Switzerland                | M.B.A     | Commerce  |             2020 |
Scenario: Create multiple education records using external JSON data
Given I load the education records from the 'Configuration\Education_TestCases\education_record_scenario.json' file
Given I navigate to Education
When I create and verify all education records
Then the batch creation process should be successful
