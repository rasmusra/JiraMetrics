@web
Feature: 2 - Plot graph of 1000 Jira issues on web page
	In order to avoid falling asleep before graph is rendered
	As a project lead
	It should be possible to load a lot of issues within reasonalbe time


Scenario: Plot Burn Up For Huge Project
	Given I am logged in as "Andreas"
	And I navigate to burn-up page
	And there exists a Jira project called 'Huge project' with 1000 issues where each has story point of 13
	When I query "Huge project"	
	And I wait, but not longer than 10 second
	Then I should see a burn-up graph
	And the accumulated story points of all issues should be 13000.0
