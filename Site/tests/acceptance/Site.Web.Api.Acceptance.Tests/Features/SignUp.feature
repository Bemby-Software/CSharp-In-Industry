Feature: Team Sign Up
    
Scenario: Team name already in use 
    Given a team exists with the name 'Cool Coders'
    When a team tries to use the same name again
    Then a bad request response should be returned
    And it should have a code 'team_name_in_use'
    And the reason should be 'The team name is already in use'
    And it should be friendly to the user
    
    
Scenario: No partiticipants in a team
    Given a team with the name 'Cool Coders'
    And No Participants
    When the team tries to sign up
    Then a bad request response should be returned
    And it should have a code 'participants_required'
    And the reason should be 'A team cannot be created without any participants'
    And it should be friendly to the user
    
Scenario: Participant forname empty
    Given a team with the name 'Cool Coders'
    And participants with the details
    | Forename | Surname | Email                |
    |          | Jones   | fred.jones@gmail.com |
    | Joe      | Bloggs  | joe.bloggs@gmail.com | 
    When the team tries to sign up
    Then a bad request response should be returned
    And it should have a code 'forename_required'
    And the reason should be 'A participant must have a forename'
    And it should be friendly to the user
    
Scenario: Participant surname empty
    Given a team with the name 'Cool Coders'
    And participants with the details
      | Forename | Surname | Email                |
      | Fred     |         | fred.jones@gmail.com |
      | Joe      | Bloggs  | joe.bloggs@gmail.com | 
    When the team tries to sign up
    Then a bad request response should be returned
    And it should have a code 'surname_required'
    And the reason should be 'A participant must have a surname'
    And it should be friendly to the user
    
Scenario: Participant empty email
    Given a team with the name 'Cool Coders'
    And participants with the details
      | Forename | Surname | Email                |
      | Fred     | Jones   | fred.jones@gmail.com |
      | Joe      | Bloggs  |                      | 
    When the team tries to sign up
    Then a bad request response should be returned
    And it should have a code 'invalid_email'
    And the reason should be 'The email provided is invalid'
    And it should be friendly to the user
    
Scenario: Participant invalid email
    Given a team with the name 'Cool Coders'
    And participants with the details
      | Forename | Surname | Email                |
      | Fred     | Jones   | fred.jones@gmail.com |
      | Joe      | Bloggs  | fred"adgr@set@.com   | 
    When the team tries to sign up
    Then a bad request response should be returned
    And it should have a code 'invalid_email'
    And the reason should be 'The email provided is invalid'
    And it should be friendly to the user
    
    
Scenario: Participant email in use
    Given a team with the name 'Cool Coders'
    And participants with the details are already signed up
      | Forename | Surname | Email                |
      | Fred     | Jones   | fred.jones@gmail.com |
      | Joe      | Bloggs  | joe.bloggs@gmail.com |
    Given another team with the name 'Coding Gurus'
    And participants with the details 
      | Forename | Surname | Email                |
      | Fred     | Jones   | fred.jones@gmail.com |      
    When the team tries to sign up
    Then a bad request response should be returned
    And it should have a code 'email_in_use'
    And the reason should be 'The email provided is already in use.'
    And it should be friendly to the user
    
Scenario: Succesful team sign up
    Given a team with the name 'Cool Coders'
    And participants with the details
      | Forename | Surname | Email                |
      | Fred     | Jones   | fred.jones@gmail.com |
      | Joe      | Bloggs  | joe.blogs@gmail.com  | 
    When the team tries to sign up
    Then a ok response is returned
    And the tokens should have been sent to the participants
    And the records should reside in the database