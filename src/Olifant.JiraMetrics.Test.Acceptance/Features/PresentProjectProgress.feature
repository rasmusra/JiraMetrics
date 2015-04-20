@web
Feature: Present project progress
	In order to get a grip on project risks 
	As a stakeholder
	I want to see the project progress in a burn-up 
	and the graph should be updated daily


Background:
	Given a team member named "Andreas"
	And a project lead named "Sixten"
	And a stakeholder named "Berit"
	And a system named "JiraMetrics"
	And a project named "JiraMetrics"


	@no_data_changes
Scenario: View burn-up
	Given I am logged in as "Andreas"
	When I navigate to burn-up page
	And I wait, but not longer than 1 second
	Then I should see a burn-up graph
	And I should see a dropdown with selectable projects:
	| Project name |
	| OFU          |
	| Disco        |
	| SCSC         |
	But the statuses should be hidden


	@no_data_changes
Scenario: Plot issues from query in burn-up
	Given I am logged in as "Andreas"
	And I navigate to burn-up page
	When I query "Disco" 
	Then I should see a burn-up graph
	And I should see the following values in the graph:
	| Start X | End X  | Start Y | End Y |
	| start   | y14w51 | 0       | 12.5  |

	@reset_after_scenario
Scenario: Plot graph of 1000 Jira issues on web page in 5 secs
	Given I am logged in as "Andreas"
	And I navigate to burn-up page
	And there exists a Jira project called 'Huge project' with 1000 issues where each has story point of 13
	When I query "Huge project"	
	And I wait, but not longer than 10 second
	Then I should see a burn-up graph
	And the accumulated story points of all issues should be 13000.0

	@ignore
Scenario: Filter burn-up on dates
	Given I am logged in as "Andreas"
	When I navigate to burn-up page on the project site
	And I enter the following start- and end-dates:
	| Start date | End date      |
	| 2014-07-01 | 2014-12-01 |
	And click button "show graph"
	Then I should see a burn-up graph with values:
	| Start X    | End X      | Start Y | End Y                    |
	| 2014-07-01 | 2014-12-01 | 0       | 163 (not determined yet) |


	@ignore
Scenario: Email link to graph
	Given I am logged in as "Andreas"
	And I see a burn-up graph with values:
	| Start value at x-axis | End-value at x-axis | Start-value at y-axis | End-value at y-axis      |
	| 2014-07-01            | 2014-12-01          | 0                     | 163 (not determined yet) |
	And I email the url of the graph to "Sixten"
	When "sixten" clicks on the link
	Then "Sixten" should see a burn-up graph on a web-page with values:
	| Start value at x-axis | End-value at x-axis | Start-value at y-axis | End-value at y-axis      |
	| 2014-07-01            | 2014-12-01          | 0                     | 163 (not determined yet) |
	And "Sixten" should see the following start- and end-dates:
	| Start date | End date   |
	| 2014-07-01 | 2014-12-01 |
