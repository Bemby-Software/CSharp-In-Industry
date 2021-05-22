Feature: Git Hub Issue Transfers
    
    Scenario: issue transfer message on queue
        Given there is team in the database
        And there is a issue transfter message on the queue
        When the transfer function runs
        Then a new issue is created on github
        And the issue transfer count for the user is updated