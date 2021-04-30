Feature: Sign in
    
    
    Scenario: User succesful sign in
        Given a user with the email 'test@test.com' is signed up
        When a user signs in with there details
        Then the api returns a 200 status code
        
        
    Scenario: User unsuccesful sign in
        Given a user with the email 'test@test.com' and the token value of 'some-token-value' tries to sign in
        When they are not signed up
        Then a bad request response should be returned
        And it should have a code 'invalid_credentials'
        And the reason should be 'The credentials provided do not match our records.'
        And it should be friendly to the user