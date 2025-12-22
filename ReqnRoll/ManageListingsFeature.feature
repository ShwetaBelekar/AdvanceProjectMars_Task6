@ManageListings
Feature: ManageListingsFeature
As a Project_Mars user
    I want to delete a listing
    So that I can remove unwanted listings


@SingleRecord
Scenario: Delete listing from Manage Listings using external JSON data 
	Given I load the listing records that i want to delete from the 'Configuration\ManageListings_TestCases\ManageListings_Deletelisting.json' file
	When I navigate to manage listings
	Then I delete the listing
	Then the listing should be delete successfully

@SingleRecord
Scenario: Edit listing from Manage Listings using external JSON data
Given I load the listing records that i want to delete from the 'Configuration\ManageListings_TestCases\ManageListings_Editlisting.json' file
When I navigate to manage listings
Then I edit the listing
Then the listing should be edited successfully

@SingleRecord
Scenario: View listing from Manage Listings using external JSON data
Given I load the listing records that i want to delete from the 'Configuration\ManageListings_TestCases\ManageListings_Viewlisting.json' file
When I navigate to manage listings
Then I view the listing
Then the listing should be viewed successfully

@SingleRecord
Scenario: Toggle Enable and Disable from Manage Listings using external JSON data
Given I load the listing records that i want to delete from the 'Configuration\ManageListings_TestCases\ManageListings_ToggleDisableandEnable.json' file
When I navigate to manage listings
Then I disable the listing
Then the listing should be disable successfully