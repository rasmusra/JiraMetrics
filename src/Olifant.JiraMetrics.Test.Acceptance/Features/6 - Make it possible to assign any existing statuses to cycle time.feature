@web
Feature: 6 - Make it possible to assign any existing statuses to cycle time 
	To be able to work with cycle times
	As a person involved in Jira project
	I need the exported cycle times presented in a report


Scenario: Present cycle statuses
	Given I am logged in as "Andreas"
	When I navigate to "burn-up" page 
	And I make the statuses visible
	Then I should see default setup for statuses in cycle:
	| Precycle Status | Cycle status           | Postcycle Status            |
	| Open            | System Test            | Deployed In Acceptance Test |
	| Reopened        | Ready for Test         | System Integration Test     |
	|                 | Describing Requirement | System Integration Testing  |
	|                 | Build & Configure      | Resolved                    |
	|                 | Building & Configuring | Closed                      |
	|                 | System Testing         | Acceptance Test             |
	|                 | Describe Requirement   | Acceptance Testing          |
	|                 | Review                 | System Test Done            |
	|                 |                        | Design Architecture         |
	|                 |                        | Designing Architecture      |
	|                 |                        | Implement                   |
	|                 |                        | Implementing                |
	|                 |                        | Test                        |
	|                 |                        | Testing                     |
	|                 |                        | Deployed In Test            |


Scenario: Hide statuses
	Given I am logged in as "Andreas"
	And I navigate to "burn-up" page
	When I hide the statuses
	Then the statuses should be hidden
  

Scenario: Move statuses 
	Given I am logged in as "Andreas"
	And I navigate to "burn-up" page
	When I make the statuses visible
	And I move status "Build & Configure" in "Cycle Statuses" to "Pre Cycle Statuses"
	And I move status "Reopened" in "Pre Cycle Statuses" to "Cycle Statuses"
	And I move status "Describe Requirement" in "Cycle Statuses" to "Post Cycle statuses" 
	And I move status "Closed" in "Post Cycle Statuses" to "Cycle statuses"
	Then I should see following setup for statuses in cycle:
	| Precycle Status   | Cycle status           | Postcycle Status            |
	| Open              | System Test            | Deployed In Acceptance Test |
	| Build & Configure | Ready for Test         | System Integration Test     |
	|                   | Describing Requirement | System Integration Testing  |
	|                   | Building & Configuring | Resolved                    |
	|                   | System Testing         | Acceptance Test             |
	|                   | Review                 | Acceptance Testing          |
	|                   | Reopened               | System Test Done            |
	|                   | Closed                 | Design Architecture         |
	|                   |                        | Designing Architecture      |
	|                   |                        | Implement                   |
	|                   |                        | Implementing                |
	|                   |                        | Test                        |
	|                   |                        | Testing                     |
	|                   |                        | Deployed In Test            |
	|                   |                        | Describe Requirement        |
	 
	 
	 
	 
	 
	 
	 
	 
