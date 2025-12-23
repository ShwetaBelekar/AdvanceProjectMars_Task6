@ManageRequests
Feature: ManageRequestsFeature
As a Project_Mars user
    I should be able to see the skill swap request that I have sent to other users and also be able to see the skill swap requests that i have received from other users
    So that I can manage my requests correctly
    A short summary of the feature:This feature uses an external JSON file to drive the creation




@SingleRecord
Scenario: Manage Request Sent Request and Received Request using external JSON data
	Given  I load the test data from 'Configuration\ManageRequests_TestCases\ManageRequests_SentRequestandReceivedRequest.json' file
	Then I send skill swap request to other user and i login into the user account to check the skill swap request received successfully
