Feature: Team Sign Up
    
Scenario: Team Sign Up 
    Given a team tries to sign up
    When the team name is in use
    Then an error should be returned
    
        