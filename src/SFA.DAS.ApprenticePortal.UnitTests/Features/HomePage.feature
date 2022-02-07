@outerApi
Feature: HomePage
	As an apprentice 
	I need to have a home page showing the over status of my current apprenticeship
	And the menu links so I can navigate the portal 

Scenario: The registration was just matched
	Given the apprentice is authenticated
	And there is an unmatched account
	When accessing the home page with the notification "ApprenticeshipDidNotMatch"
	Then the response status should be Ok
	And the notification should be "ApprenticeshipDidNotMatch"

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
	And the notification should be "ApprenticeshipStopped"
	And the just stopped information message should be visible
	And the employer name should be correct
	And the course name should be correct

Scenario: The stopped apprenticeship exists, but has never been viewed
	Given the apprentice is authenticated
	And there is a single stopped apprenticeship which hasn't ever been viewed
	When accessing the home page
	Then the response status should be Ok
	And the notification should be "ApprenticeshipStopped"
	And the just stopped information message should be visible
	And the employer name should be correct
	And the course name should be correct

Scenario: The apprenticeship exists, and has been viewed since being stopped
	Given the apprentice is authenticated
	And there is a single stopped apprenticeship and it has been viewed since being stopped
	When accessing the home page
	Then the response status should be Ok
	And the just stopped information message should not be visible

Scenario: The registration is stopped and was just matched
	Given the apprentice is authenticated
	And there is a single stopped apprenticeship which hasn't been viewed since being stopped
	When accessing the home page with the notification "ApprenticeshipDidNotMatch"
	Then the response status should be Ok
	And the notification should be "ApprenticeshipStopped"
