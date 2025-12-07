@Certifications
Feature: CertificationFeature
As a Project_Mars user
I would like to create, edit and delete certification records
So that I can manage certification successfully
A short summary of the feature:This feature uses an external JSON file to drive the creation



@MultipleRecords
Scenario: Create multiple valid certification records using external JSON data
Given I load the certification records from the 'Configuration\Certification_TestCases\Certification_ValidRecord.json' file
Given I navigate to Certification
When I create and verify all certification records
Then the batch should be created successfully


@SingleRecord
Scenario: Delete existing certification records using external JSON data
Given I load the certification records from the 'Configuration\Certification_TestCases\Certification_Deleteexistingrecord.json' file
Given I navigate to Certification
When I see certification records
When I delete the existing certification record
Then I should see a message that record deleted successfully
