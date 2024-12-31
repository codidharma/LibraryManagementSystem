This page describes all the use case mappings that we identified in the membership context based on the requirements and process level discussions. These are documented using the BDD style specifications

# Onboarding a citizen as a regular patron

Scenario: Onboarding a citizen as a regular patron

Given Maddy visits the library to register as a patron
When the Librarian Mrs. Robinson verifies that Maddy is not already a registered patron
And verifies the address proof
And submits the onboarding application
Then the LMS onboards Maddy as a regular patron
And Issue the membership card with a unique patron id
And notifies Maddy by sending the Patron Id and a link to sign-up on the website

# Onboarding a citizen as a research patron

Scenario: Onboarding a citizen as a research patron

Given Dr. Patel visits the library to register as a patron
When the Librarian Mrs. Robinson verifies that Dr. Patel is not already a registered patron
And verifies the address proof
And verifies the proof of ongoing research
And submits the onboarding application
Then the LMS system onboards Dr. Patel as a research patron
And issues the membership card with unique patron id
And notifies Dr. Patel by sending an email with the Patron Id and a link to sign-up on the website


# Reject onboarding for an existing regular patron

Scenario: Rejecting onboarding of an existing regular patron

Given Greg visits the library to register as a patron
When the Librarian Mrs. Robinson verifies that Greg is already a regular patron
Then the LMS rejects the Patron Membership onboarding request
And notifies Greg by sending an email of rejection with reason

# Reject onboarding for an existing research patron

Scenario: Rejecting onboarding of an existing research patron

Given Lukas visits the library to register as a patron
When the Librarian Mrs. Robinson verifies that Lukas is already a research patron
Then the LMS rejects the Patron Membership onboarding request
And notifies Lukas by sending an email of rejection with reason

# Membership expiring for existing regular patron

Scenario: Membership expiring for existing regular patron
Given the expiry date for the membership for Claire is thirty days away
When the LMS checks the membership expiration log everyday at 8 PM
Then the LMS sends a notification to Claire with a reminder to renew her membership

