Feature: ManageQueries
	In order to get reports out fast
	As a manager with not that much time
	I need ways to re-use old jql queries


Scenario: Reuse jql between sessions
	Given program is started
	And there exists jql queries in the combobox:
	| jql                                                    |
	| search/jql=key=DISCO-776&expand=changelog              |
	| search/jql=project=OFU&expand=changelog&maxResults=500 |
	And I select first query and modify it to: "search/jql=key=DISCO-777&expand=changelog"
	And the checkbox "Save query" is "checked"
	When I restart the program
	Then the combobox should contain:
	| jql                                                    |
	| search/jql=key=DISCO-776&expand=changelog              |
	| search/jql=project=OFU&expand=changelog&maxResults=500 |
	| search/jql=key=DISCO-777&expand=changelog              |


Scenario: Discard jql between sessions
	Given program is started
	And there exists jql queries in the combobox:
	| jql                                                    |
	| search/jql=key=DISCO-776&expand=changelog              |
	| search/jql=project=OFU&expand=changelog&maxResults=500 |
	And the checkbox "Save query" is "unchecked"
	And I select first query and modify it to: "search/jql=key=DISCO-777&expand=changelog"
	When I restart the program
	Then the combobox should contain:
	| jql                                                    |
	| search/jql=key=DISCO-776&expand=changelog              |
	| search/jql=project=OFU&expand=changelog&maxResults=500 |
