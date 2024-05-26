Feature: Checkout System
The restaurant serves starters, mains and drinks, and the set cost for each is:
Starters cost £4.00, 
Mains cost £7.00, 
Drinks cost £2.50
Drinks have a 30% discount when ordered before 19:00

    Scenario: Calculate bill for a group with starters, mains, and drinks
        Given a table of people
        When table orders 4 starters, 4 mains, and 4 drinks at 18:00 o'clock
        Then the endpoint should return a bill with the correct total amount

    Scenario: Update bill for a table adding to their order later in time
        Given a table of people
        When table orders 1 starters, 2 mains, and 2 drinks at 18:00 o'clock
        Then the endpoint should return a bill with the correct total amount
        When table orders 0 starters, 2 mains, and 2 drinks at 20:00 o'clock
        Then the endpoint should return a bill with the correct total amount

    Scenario: Adjust bill for a table canceling part of the order
        Given a table of people
        When table orders 1 starters, 1 mains, and 4 drinks at 18:00 o'clock
        Then the endpoint should return a bill with the correct total amount
        When table cancels 0 starters, 0 mains, and 2 drinks at 18:50 o'clock
        Then the endpoint should return a bill with the correct total amount
