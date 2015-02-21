@ignore
Feature: JM-7 Create Value added time report
	To find waste in the process
	As a person involved in Jira project
	I need the value-added-times presented in a report


Scenario Outline: Show Value Added Time header in report
	Given program is started
	And I have entered the following query: "<Query>"
	And I have chosen the start and end of cycle as "<Cycle period>" 
	And I enter the Start date interval as:
	| Min date   | Max date   |
	| <Min date> | <Max date> |
	When I click the "Value added time report" button
	Then I should be able to see the following title header in the report: "Value Added Time Report, cycle : <Cycle period>, jql : <Query>, filters : [StartDateFilter(<Min date>,<Max date>)]"

	Examples:
	| Scenario description | Query        | Cycle period | Min date   | Max date   |
	| Happy scenario       | Key=DUMMYKEY | Planning     | 2000-01-01 | 2020-12-01 |


@ignore
Scenario: Get value Added Time info with cycles in full process 
	Given program is started
	And I have entered the following query: "key in (SCSC-1000,DISCO-789,DISCO-846,DISCO-937)"
	And I have chosen start label "started" and start and end of cycle as "Full process"
	When I click the "Value added time report" button
	Then I should be presented the following value added times in the report:
	| Key       | Summary                      | Cycle | Value | Cycle description                | Start date       |
	| SCSC-1000 | Show project progress on web | 1     | 0     | Status = Open                    | 2014-11-14 15:21 |
	|           |                              | 2     | 0     | Label = started                  | 2014-11-14 15:25 |
	|           |                              | 3     | 0     | Status = Implement               | 2014-11-19 07:36 |
	|           |                              | 4     | 1     | Status = Implementing            | 2014-11-19 07:37 |
	|           |                              | 5     | 0     | Status = Open                    | 2014-11-19 07:37 |
	|           |                              | 6     | 0     | Status = Implement               | 2014-11-20 13:11 |
	|           |                              | 7     | 1     | Status = Implementing            | 2014-11-20 13:12 |
	|           |                              | 8     | 1     | Status = Review                  | 2014-12-03 17:36 |
	|           |                              | 9     | 0     | Status = Test                    | 2014-12-03 10:52 |
	|           |                              | 10    | 0     | Label = Blocked started          | 2014-12-03 11:00 |
	|           |                              | 11    | 0     | Label = started                  | 2014-12-03 08:20 |
	|           |                              | 12    | 1     | Status = Testing                 | 2014-12-03 12:37 |
	|           |                              | 13    | 0     | Status = System Test Done        | 2014-12-03 06:30 |
	|           |                              | 14    | 0     | Status = System Integration Test | 2015-01-08 14:34 |

@ignore
Scenario: Get value-added time info with cycles in test phase
	Given program is started
	And I have entered the following query: "key in (SCSC-1000,DISCO-789,DISCO-846,DISCO-937)"
	And I have chosen the start and end of cycle as "Test" 
	When I click the "Value added time report" button
	Then I should be presented the following value added times in the report:
	| Key       | Summary                      | Cycle | Value | Cycle description         | Start date       |
	| SCSC-1000 | Show project progress on web | 1     | 0     | Status = Test             | 2014-12-05 10:52 |
	|           |                              | 2     | 0     | Label = Blocked started   | 2014-12-08 11:00 |
	|           |                              | 3     | 0     | Label = started           | 2014-12-09 08:20 |
	|           |                              | 4     | 1     | Status = Testing          | 2014-12-09 12:37 |
	|           |                              | 5     | 0     | Status = System Test Done | 2014-12-11 06:30 |
	

