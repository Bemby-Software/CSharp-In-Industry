Feature: Validation Features
    
    
    Scenario: Particpants email checked and in use
        Given a participant is signed up with the email 'test@test.com'
        When a participant tries to use email 'test@test.com'
        Then the api returns true with the 200 status code
    
    Scenario: Particpants email checked and is not in use
        Given a participant is signed up with the email 'test1@test.com'        
        When a participant tries to use email 'test@test.com'
        Then the api returns false with the 200 status code
        
    Scenario: Team name checked and in use
        Given a team signs up with the name 'Cool Coders'
        When a team tries to sign up with the name 'Cool Coders'
        Then the api returns true with the 200 status code
        
    Scenario: Team name checked and not in use
        Given a team signs up with the name 'Quick Coders'
        When a team tries to sign up with the name 'Cool Coders'
        Then the api returns false with the 200 status code
        