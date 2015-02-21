Feature: Create report
	For being able to work with cycle times
	As a person involved in Jira project
	I need the exported cycle times presented in a report


Scenario Outline: Show header in report
	Given program is started
	And I have entered the following query: "<Query>"
	And I have chosen the start and end of cycle as "<Cycle period>" 
	And I enter the Start date interval as:
	| Min date   | Max date   |
	| <Min date> | <Max date> |
	And I "check" the checkbox "Exclude issues that are not done"
	When I click the "Generate report" button
	Then I should be able to see the following header in the report: "cycle : <Cycle period>, jql : <Query>, filters : [StartDateFilter(<Min date>,<Max date>), WorkDoneFilter]" 

	Examples:
	| Scenario description | Query        | Cycle period | Min date   | Max date   |
	| Happy scenario       | Key=DUMMYKEY | Planning     | 2000-01-01 | 2020-12-01 |

