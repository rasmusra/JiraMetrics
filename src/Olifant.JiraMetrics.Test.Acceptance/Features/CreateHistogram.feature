Feature: Create Histogram
	In order to get histogram from all data
	As a Kanban number cruncher
	I need automated ways to generate data for the histogram 

Scenario: Assigning delta for histogram 
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
