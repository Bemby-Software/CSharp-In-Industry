Feature: Participant Actions
    
    Scenario: Gets valid participant without team
        Given a participant with the token 'token' 
        And the details is signed up
          | Forename | Surname | Email                |
          | Fred     | Jones   | fred.jones@gmail.com |
        When that participant tries to get there details
        Then the api returns a 200 status code
        And the correct participant details
        
    Scenario: Gets valid participant with team
        Given a participant with the token 'token' 
        And the details is signed up
          | Forename | Surname | Email                |
          | Fred     | Jones   | fred.jones@gmail.com |
        When that participant tries to get there details including the team
        Then the api returns a 200 status code
        And the correct participant details
        And the team information is returned