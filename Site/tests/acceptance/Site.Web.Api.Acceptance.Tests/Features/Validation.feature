Feature: Validation Features
    
    
    Scenario: Particpants email checked and in use
        Given a participant is signed up with the email 'test@test.com'
        When a participant tries to use email 'test@test.com'
        Then a bad request response should be returned
        And it should have a code 'email_in_use'
        And the reason should be 'The email provided is already in use.'
        And it should be friendly to the user
    
    Scenario: Particpants email checked and is not in use
        Given a participant is signed up with the email 'test1@test.com'        
        When a participant tries to use email 'test@test.com'
        Then the api returns a 200 status code
        
    Scenario: Particpants email invalid         
        When a participant tries to use email 'tes12343413241234t1234test.com'
        Then a bad request response should be returned
        And it should have a code 'invalid_email'
        And the reason should be 'The email provided is invalid'
        And it should be friendly to the user
        
    Scenario: Particpants email valid         
        When a participant tries to use email 'test@test.com'
        Then a ok response is returned
        
    Scenario: Team name checked and in use
        Given a team signs up with the name 'Cool Coders'
        When a team tries to sign up with the name 'Cool Coders'
        Then the api returns true with the 200 status code
        
    Scenario: Team name checked and not in use
        Given a team signs up with the name 'Quick Coders'
        When a team tries to sign up with the name 'Cool Coders'
        Then the api returns false with the 200 status code
        