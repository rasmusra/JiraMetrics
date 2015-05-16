@web
Feature: JM1 - Present project progress
	In order to get a grip on project risks 
	As a stakeholder
	I want to see the project progress in a burn-up 
	and the graph should be updated daily


	@no_data_changes
Scenario: 1 - View burn-up
	When I navigate to "burn-up" page
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
	Given I navigate to "burn-up" page
	When I query "Disco" 
	Then I should see a burn-up graph
	And I should see the following values in the graph:
	| Start X | Y values |
	| start   | 0, 1     |

Scenario: 3 - Load JiraMetrics with new issues from Jira
	Given JiraMetrics contains the following issues:
	| Key       | Story points |
	| DISCO-665 | 7            |
	| OFU-1462  | 4            |
	And Jira contains additional issues:
	| Project | Key       | Story Points |
	| DISCO   | DISCO-937 | 1            |
	| DISCO   | DISCO-846 | 5            |
	| OFU     | OFU-2290  | 6            |
	When I navigate to "admin" page
	And I load JiraMetrics with issues from Jira project "Disco"
	And I wait, but not longer than 5 seconds
	Then I should be presented a list of all issues that has been added:
	| issue     | action |
	| DISCO-937 | Added  |
	| DISCO-846 | Added  |


Scenario: 4 - Updating graph with new issues
	Given JiraMetrics contains the following issues:
	| Key       | Story points |
	| DISCO-665 | 1            |
	| OFU-1462  | 4            |
	When I navigate to "admin" page
	And I load JiraMetrics with project "Disco" having the following issue:
	| Project | Key       | Story Points | Status   |
	| DISCO   | DISCO-620 | 7            | Resolved |
	And I navigate to "burn-up" page
	And I query "Disco"
	And I wait, but not longer than 10 second
	Then I should see the following values in the graph:
	| Start X | End X  | Y values                    |
	| start   | y14w51 | 0, 1, 3.5, 10.5, 12.0, 13.0 |


	@wip
Scenario: 5 - Load JiraMetrics with changed issues from Jira
	Given JiraMetrics contain the following issue:
	| Key       | Story points |
	| DISCO-665 | 1            |
	And Jira contains updated issues from '2015-05-15':
	| Project | Key       | Story Points |
	| DISCO   | DISCO-665 | 40           |
	When I navigate to "admin" page
	And I load JiraMetrics with issues from Jira project "Disco"
	And I wait, but not longer than 5 seconds
	Then I should be presented a list of all issues that has been updated:
	| issue     | action  |
	| DISCO-665 | Updated |


Scenario: 6 - Updating graph with changed issues
	Given the system contains the following issues:
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


@no_data_changes
Scenario: 7 - Loading JiraMetrics when no new issues needs to be loaded
	Given JiraMetrics contains all the latest versions of issues in Jira
	When I navigate to "admin" page
	And I load JiraMetrics with issues from Jira project "Disco"
	And I wait, but not longer than 5 second
	Then I should be presented a message "All issues are up-to-date for project Disco"
