@ManageListings
Feature: ManageListingsFeature
As a Project_Mars user
    I should be able to view, edit, delete my listings
    So that I can manage my listings correctly
    A short summary of the feature:This feature uses an external JSON file to drive the creation


@SingleRecord
Scenario: Delete listing from Manage Listings using external JSON data 
	Given I load the test data from 'Configuration\ManageListings_TestCases\ManageListings_Deletelisting.json' file
	When I navigate to manage listings
	Then I delete the listing
	Then the listing should be delete successfully

@SingleRecord
Scenario: Edit listing from Manage Listings using external JSON data
Given I load the test data from 'Configuration\ManageListings_TestCases\ManageListings_Editlisting.json' file
When I navigate to manage listings
Then I edit the listing
Then the listing should be edited successfully

@SingleRecord
Scenario: View listing from Manage Listings using external JSON data
Given I load the test data from 'Configuration\ManageListings_TestCases\ManageListings_Viewlisting.json' file
When I navigate to manage listings
Then I view the listing
Then the listing should be viewed successfully

@SingleRecord
Scenario: Toggle Enable and Disable from Manage Listings using external JSON data
Given I load the test data from 'Configuration\ManageListings_TestCases\ManageListings_ToggleDisableandEnable.json' file
When I navigate to manage listings
Then I disable the listing
Then the listing should be disable successfully

@SingleRecord
Scenario: Send Request button is disabled for own listings in Manage Listings feature using external JSON data
    Given I load the test data from 'Configuration\ManageListings_TestCases\ManageListings_SendRequest.json' file
    When I navigate to manage listings
    When I view a listings
    Then the Send Request button should be disabled
