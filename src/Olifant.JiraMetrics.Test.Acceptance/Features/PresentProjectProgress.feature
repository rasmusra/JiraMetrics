@chrome
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


Scenario: View burn-up
	Given I am logged in as "Andreas"
	When I navigate to burn-up page
	Then I should see a burn-up graph within 2 seconds
	And I should see an empty search field
	But the statuses should be hidden


Scenario: Plot issues from query in burn-up
	Given I am logged in as "Andreas"
	And I navigate to burn-up page
	When I search for issues with jql query 'key=OFU-1462' 
	Then I should see a burn-up graph within 2 seconds
	And I should see the following values in the graph:
	| Start X | End X  | Start Y | End Y |
	| start   | y13w34 | 0       | 1.25  |

@reset_after_scenario
Scenario: Plot graph of 1000 Jira issues on web page in 5 secs
	Given I am logged in as "Andreas"
	And I navigate to burn-up page
	And there exists a Jira project called 'Huge project' with 1000 issues where each has story point of 13
	When I query "project = 'Huge project'"	
	Then I should see a burn-up graph within 50 seconds
	And the accumulated story points of all issues should be 13000

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


	@ignore
	# downprioritized for the time being
Scenario: Sixten filters for OFU3 on burn-up
	Given I am logged in as "Sixten"
	When I navigate to burn-up page on the project site
	And I enter "OFU3" in the "Filter on labels" textfield
	Then I should see a burn-up graph with all iterations since OFU3-project started
	And it should only present burn-up for OFU3
