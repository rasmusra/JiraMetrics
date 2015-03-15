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
	And a system named "OrderTracking"
	And a project named "OFU3"
	And a project site named "OrderTracking on Epic"


Scenario: View burn-up
	Given I am logged in as "Andreas"
	When I navigate to burn-up page
	Then I should see a burn-up graph
	And I should see an empty search field
	But the statuses should be hidden


Scenario: Plot burn-up
	Given I am logged in as "Andreas"
	And I navigate to burn-up page
	When I search for issues with jql query 'key=DISCO-838' 
	Then I should see a burn-up graph with values:
	| Start X    | End X      | Start Y | End Y                    |
	| 2014-07-01 | 2014-12-01 | 0       | 163 (not determined yet) |

Scenario: Plot graph of 1000 Jira issues on web page in 5 secs
	Given I am logged in as "Andreas"
	And I navigate to burn-up page
	And there exists a Jira project called 'Huge project' with 1000 issues
	When I query "project = 'Huge project'"
	Then I should be presented a histogram of issues
	And I should not need to wait more than 5 seconds

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
