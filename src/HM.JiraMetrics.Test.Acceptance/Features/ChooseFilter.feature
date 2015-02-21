Feature: Choose Filter
	In order to configure what cycles to get data for
	As a Kanban number cruncher
	I want to be able to choose start and end of cycle period


Scenario: Should be given default values for start date interval
	When I start the program
	Then I should see default dates for Start date interval as:
	| Min date   | Max date   |
	| 2000-01-01 | 2050-01-01 |


	@ignore
Scenario: Different start and end date combinations 
	Given program is started
	And I have entered the following query: "Issues started before and after 2014-07-01"
	And I have chosen the start and end of cycle as "Test" 
	And I enter the Start date interval as:
	| Min date   | Max date   |
	| 2014-07-01 | 2014-10-31 |
	When I click the "Cycle time report" button
	Then I should be able to see the following cycle times in the report: 
	| Key      | Summary  | Started date        | Closed date         | Cycle Time | Story Points | Original estimate |
	| OFU-2377 | UI fixes | 2014-10-03 10:22:31 | 2014-10-08 09:24:16 | 4,96       | 1,5          |                   |
	But I should not see "OFU-1462" in the report


	@ignore
Scenario Outline: Include issues not done
	Given program is started
	And I have entered the following query: "Key=<Key>"
	And I have chosen the start and end of cycle as "Development" 
	And I "uncheck" the checkbox "Exclude issues that are not done"
	When I click the "Cycle time report" button
	Then I should see "<Key>" in the report

	Examples:
	| Scenario description     | Key       |
	| Development not done yet | DISCO-729 |


	@ignore
Scenario Outline: Exclude issues not done
	Given program is started
	And I have entered the following query: "Key=<Key>"
	And I have chosen the start and end of cycle as "Development" 
	And I "check" the checkbox "Exclude issues that are not done"
	When I click the "Cycle time report" button
	Then I should not see "<Key>" in the report

	Examples:
	| Scenario description     | Key       |
	| Development not done yet | DISCO-729 |


	@ignore
Scenario Outline: Exclude issues not started
	Given program is started
	And I have entered the following query: "Key=<Key>"
	And I have chosen the start and end of cycle as "Development" 
	When I click the "Cycle time report" button
	Then I should not see "<Key>" in the report

	Examples:
	| Scenario description                  | Key      |
	| Opened but closed before work started | SCSC-974 |


	@ignore
Scenario Outline: Include issue that matches filter on done date
	Given program is started
	And I have entered the following query: "Key=DISCO-620"
	And I have chosen the start and end of cycle as "Development" 
	And I "check" the checkbox "Exclude issues that are not done"
	And I enter the Done date interval as:
	| Min date       | Max date       |
	| <Min end date> | <Max end date> |
	When I click the "Cycle time report" button
	Then I should see "DISCO-620" in the report

	Examples:
	| Scenario description             | Min end date | Max end date |
	| min date same day as done date   | 2014-10-20   | 2014-11-07   |
	| max date one day after done date | 2014-10-10   | 2014-10-21   |


	@ignore
Scenario Outline: Filter out issue that does not match filter on done date
	Given program is started
	And I have entered the following query: "Key=DISCO-620"
	And I have chosen the start and end of cycle as "Development" 
	And I "check" the checkbox "Exclude issues that are not done"
	And I enter the Done date interval as:
	| Min date       | Max date       |
	| <Min end date> | <Max end date> |
	When I click the "Cycle time report" button
	Then I should not see "DISCO-620" in the report

	Examples:
	| Scenario description               | Min end date | Max end date |
	| min date one day after done date   | 2014-10-21   | 2014-11-07   |
	| max date one same day as done date | 2014-10-10   | 2014-10-20   |
	


