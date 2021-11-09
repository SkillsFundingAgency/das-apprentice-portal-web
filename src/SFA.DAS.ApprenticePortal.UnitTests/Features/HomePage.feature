@outerApi
Feature: HomePage
	As an apprentice 
	I need to have a home page showing the over status of my current apprenticeship
	And the menu links so I can navigate the portal 

Scenario: The apprenticeship exists but has not been confirmed
	Given the apprentice is authenticated
	And there is a single incomplete apprenticeship
	When accessing the home page
	Then the response status should be Ok
	And the apprenticeship status should show "INCOMPLETE"

Scenario: The apprenticeship exists and has not been confirmed
	Given the apprentice is authenticated
	And there is a single confirmed apprenticeship
	When accessing the home page
	Then the response status should be Ok
	And the apprenticeship status should show "COMPLETE"

Scenario: The apprenticeship exists and has been stopped
	Given the apprentice is authenticated
	And there is a single stopped apprenticeship
	When accessing the home page
	Then the response status should be Ok
	And the apprenticeship status should show "STOPPED"

Scenario: The apprenticeship exists, but has not been viewed since being stopped
	Given the apprentice is authenticated
	And there is a single stopped apprenticeship which hasn't been viewed since being stopped
	When accessing the home page
	Then the response status should be Ok
	And the just stopped information message should be visible

Scenario: The apprenticeship exists, and has been viewed since being stopped
	Given the apprentice is authenticated
	And there is a single stopped apprenticeship and it has been viewed since being stopped
	When accessing the home page
	Then the response status should be Ok
	And the just stopped information message should not be visible
