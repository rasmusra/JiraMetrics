Feature: Get issues
	In order to get a sense of how long time it takes to fix issues
	As a person involved in Jira project
	I need ways to see cycle times 


Scenario: Get all defects from DISCO
	Given program is started
	And I have entered the following query: "key in (DISCO-766,DISCO-767,DISCO-636)"
	And I have chosen start label "started" and start and end of cycle as "Full process"
	When I click the "Cycle time report" button
	Then I should be presented all defects in a text file
	And I should be able to see the following cycle times in the report:
	| Scenario description                   | Key       | Summary                                              | Started date        | Closed date         | Cycle time | Story Points | Original estimate |
	| Started when defect was created        | DISCO-766 | Removed articles on manufacturing option not working | 2014-09-12 13:46:36 | 2014-09-16 13:53:42 | 4,00       | 2,0          |                   |
	| Started a few days after creation date | DISCO-767 | SIT - Notice error exception in PMT                  | 2014-09-16 15:35:12 | 2014-09-18 11:04:26 | 1,81       | 0,5          |                   |
# More than one started date             
# Closed but reopened
 

Scenario Outline: Different start and end date combinations 
	Given program is started
	And I have entered the following query: "Key=<Key>"
	And I have chosen start label "started" and start and end of cycle as "Full process"
	When I click the "Cycle time report" button
	Then I should be able to see the following cycle times in the report:
	| Key   | Summary   | Started date   | Closed date   | Cycle Time   | Story Points   | Original estimate |
	| <Key> | <Summary> | <Started date> | <Closed date> | <Cycle Time> | <Story Points> |                   |

	Examples:
	| Scenario description                          | Key      | Summary                                                                                    | Started date        | Closed date         | Cycle Time | Story Points |
	| Started, but without the started-flag         | OFU-676  | Re-filtering page leaves you on same "page number" as previously - should go to first page | 2013-02-25 09:41:34 | 2013-02-27 16:49:34 | 2,30       | 0,5          |
	| Started, with some time in arch design        | OFU-2377 | UI fixes                                                                                   | 2014-09-25 08:30:53 | 2014-10-08 09:24:16 | 13,04      | 1,5          |
	| Moved to TCC when done                        | OFU-1462 | Update date filter functionality and look in Order list page                               | 2013-08-16 16:02:06 | 2013-08-23 04:25:53 | 6,52       | 1,0          |
	| Directly moved from open to testing           | OFU-2193 | Sprint test- Pending preadvice not displayed on Pending tab                                | 2014-06-09 13:42:37 | 2014-06-10 07:49:41 | 0,75       | 0,0          |
	| Staerted before the "started" label was added | OFU-2290 | Incorrect position of the Advanced Filter header                                           | 2014-08-27 09:08:33 | 2014-12-18 07:45:20 | 112,94     | 0,5          |


Scenario Outline: Issues that should be filtered out 
	Given program is started
	And I have entered the following query: "Key=<Key that should be filtered out>"
	And I have chosen start label "<Start label>" and start and end of cycle as "<Cycle time>"
	And I "check" the checkbox "Exclude issues that are not done"
	When I click the "Cycle time report" button
	Then I should not see "<Key that should be filtered out>" in the report

	Examples:
	| Scenario description                  | Cycle time   | Start label | Key that should be filtered out |
	| Opened but closed before work started | Dev          |             | SCSC-974                        |
	| Development not done yet              | Dev          |             | DISCO-729                       |
	| Started but reopened                  | Full process | started     | DISCO-838                       |
	| In requirement phase                  | Full process | started     | OFU-2299                        |


@ignore
Scenario: Handle failed query
	Given program is started
	And I have entered the following query: "THIS IS A REALLY BAD QUERY"
	Then I should be presented an error message
	And I should be directed back to query window