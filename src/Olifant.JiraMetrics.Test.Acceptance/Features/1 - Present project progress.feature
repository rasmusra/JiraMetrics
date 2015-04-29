@web
Feature: 1 - Present project progress
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
Scenario: 1 - View burn-up
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
Scenario: 2 - Plot issues from query in burn-up
	Given I am logged in as "Andreas"
	And I navigate to burn-up page
	When I query "Disco" 
	Then I should see a burn-up graph
	And I should see the following values in the graph:
	| Start X | End X  | Start Y | End Y |
	| start   | y14w51 | 0       | 12.5  |

@wip
Scenario: 3 - Load JiraMetrics with new issues from Jira
	Given I am logged in as "Sixten"
	And the system contains the following issues:
	| Key       | Story points |
	| DISCO-620 | 3            |
	| OFU-1462  | 4            |
	And Jira contains additional issues:
	| Project | Key       | Story Points |
	| Disco   | DISCO-665 | 5            |
	| OFU     | OFU-2290  | 6            |
	When I navigate to admin page
	And I choose to load JiraMetrics with project "Disco"
	Then I should be presented a list of issues been added:
	| issue      | comment |
	| DISCO-2299 | Added!  | 


Scenario: 4 - Updating graph with new issues
	Given I am logged in as "Sixten"
	And the system contains the following issues:
	| project | issue      |
	| Disco   | DISCO-1462 |
	| OFU     | OFU-676    |
	And I have the following values in the burn-up graph:
	| Start X | End X  | Start Y | End Y |
	| start   | y14w51 | 0       | 12.5  |
	When the following issue is added to the system from Jira:
	| project | issue      |
	| Disco   | DISCO-2299 |
	And I should see the following values in the graph:
	| Start X | End X  | Start Y | End Y |
	| start   | y14w51 | 0       | 12.5  |


Scenario: 5 - Load JiraMetrics with changed issues from Jira
	Given I am logged in as "Sixten"
	And the system contains the following issues:
	| project | issue      |
	| Disco   | DISCO-1462 |
	| OFU     | OFU-676    |
	And Jira contains additional issues:
	| project | issue      |
	| Disco   | DISCO-2299 |
	| OFU     | OFU-2067   | 
	When I navigate to admin page
	And I choose to load JiraMetrics with project "Disco"
	Then I should be presented a list of updated issues:
	| issue      | comment  |
	| DISCO-2299 | Updated! | 


Scenario: 6 - Updating graph with changed issues
	Given I am logged in as "Sixten"
	And the system contains the following issues:
	| project | issue      | status       | Story Points |
	| Disco   | DISCO-1462 | Closed       | 3            |
	| Disco   | DISCO-2299 | Implementing | 4            |
	| OFU     | OFU-676    | Closed       | 5            |
	And I choose project "Disco" to get the following values in the burn-up graph:
	| Start X | End X  | Start Y | End Y |
	| start   | y14w51 | 0       | 3     |
	When the following issue is updated to the system from Jira:
	| project | issue      | status |
	| Disco   | DISCO-2299 | Closed |
	Then I should be able to see the following values in the burn-up graph:
	| Start X | End X  | Start Y | End Y |
	| start   | y14w51 | 0       | 7.0  |


Scenario: 7 - Loading JiraMetrics when no new issues needs to be loaded
	Given I am logged in as "Sixten"
	And the system contains the following issues:
	| project | issue      |
	| Disco   | DISCO-1462 |
	| OFU     | OFU-676    |
	And Jira contains issues:
	| project | issue      |
	| Disco   | DISCO-1462 |
	| OFU     | OFU-2067   | 
	When I navigate to admin page
	And I choose to load JiraMetrics with project "Disco"
	Then I should be presented a message "All issues are up-to-date for project "Disco"


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
