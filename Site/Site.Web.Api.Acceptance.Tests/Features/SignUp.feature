Feature: Team Sign Up
    
Scenario: Team name already in use 
    Given a team exists with the name 'Cool Coders'
    When a team tries to use the same name again
    Then an bad request response should be returned
    And it should have a code 'team_name_in_use'
    And a the reason 'The team name is already in use'
    And should be friendly to the user