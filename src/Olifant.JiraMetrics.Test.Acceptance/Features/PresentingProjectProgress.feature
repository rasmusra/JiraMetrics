@chrome
Feature: Presenting project progress
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


Scenario: JM-2 View burn-up controls
	Given I am logged in as "Andreas"
	When I navigate to burn-up page on the project site, with jql "key in (SCSC-1000,DISCO-789,DISCO-846,DISCO-937)"
	Then I should see a burn-up graph with all iterations since OFU2-project started
	And I should see the following controls:
	| Type    | Name           | Value |
	| textbox | jql            |       |
	| button  | generate_graph |       |

Scenario: JM-3 Filter burn-up on dates
	Given I am logged in as "Andreas"
	When I navigate to burn-up page on the project site
	And I enter the following start- and end-dates:
	| Start date | End date      |
	| 2014-07-01 | 2014-12-01 |
	And click button "show graph"
	Then I should see a burn-up graph with values:
	| Start X    | End X      | Start Y | End Y                    |
	| 2014-07-01 | 2014-12-01 | 0       | 163 (not determined yet) |

Scenario: JM-4 Email link to graph
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
