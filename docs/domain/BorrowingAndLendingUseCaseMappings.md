

# Regular patron successfully borrows a book

Scenario: Regular patron successfully borrows a book

Given Maddy wants to borrow the 'Count of Monte Cristo'
And he has less than two books borrowed at the time
When Mrs. Robinson starts the checkout process
Then the LMS checks the availability of the book
And as the book is available and has no active hold requests, assigns it to Maddy as borrowed
And Sets the return due date for the book for fourteen days from today
And sends an email notification to Maddy with details of the borrowal

# Regular patron tries to borrow a book after exceeding borrowing limit

Scenario: Regular patron tries to borrow a book after exceeding borrowing limit

Given Maddy wants to borrow the book 'Pride and Prejudice'
And he has already borrowed two books at the time
When Mrs Robinson starts the checkout process
Then the LMS rejects the request
And provides an error message about exceeding borrowal limit
And sends an email notification to Maddy about rejected checkout

# Regular patron tries to borrow a book when he has overdue books

Scenario: Regular patron tries to borrow a book when he has overdue books

Given Maddy wants to borrow the book 'Pride and Prejudice'
And he already has 'The Count of Monte Cristo' overdue
When Mrs. Robinson starts the checkout process
Then the LMS rejects the request
And provides an error message about the overdue book
And sends an email notification to Maddy about the rejected checkout

# Research patron successfully borrows a book

Scenario: Research patron successfully borrows a book

Given Dr Patel wants to borrow the 'Clean Architecture'
And he has less than ten books borrowed at the time
When Mrs. Robinson starts the checkout process
Then the LMS checks the availability of the book
And as the book is available and has no active hold requests, assigns it to Dr. patel as borrowed
And Sets the return due date for the book for thirty days from today
And sends an email notification to Maddy with details of the borrowal

# Research patron tries to borrow a book after exceeding borrowing limit

Scenario: Research patron tries to borrow a book after exceeding borrowing limit

Given Dr. Patel wants to borrow the 'Just Enough Architecture'
And he has already borrowed ten books at the time
When Mrs Robinson starts the checkout process
Then the LMS rejects the request
And provides an error message about exceeding borrowal limit
And sends an email notification to Dr. patel about the rejected checkout

# Research patron tries to borrow a book when he has overdue books

Scenario: Research patron tries to borrow a book when he has overdue books

Given Dr. Patel wants to borrow the 'Just Enough Architecture'
And he already has 'Clean Architecture' overdue
When Mrs. Robinson starts the checkout process
Then the LMS rejects it
And provides an error message about the overdue book
And sends an email notification to Dr.Patel


# Patron returns a book on or before due date

Scenario: Patron returns a book on or before due date

Given Maddy has borrowed the 'Count of Monte Cristo'
And he returns the book on or before the due date
Then the LMS marks the book to be available
And clears the book from patron's borrowed list

# Patron returns a book later than the due date

Scenario: Patron returns a book later than due date

Given Maddy has borrowed the 'Count of Monte Cristo'
And he returns the book later than the due date
When Mrs Robinson accepts the overdue book
Then the LMS calculates the late fine based on the delay
And records the fine against the patrons account
And restricts further borrowal till the fine is paid
And marks the book as available
And clears the book from Patron's borrowed list


# Patron pays the fine for overdue book

Scenario: Patron pays the fine for the overdue book

Given Maddy has accrued a fine of $10 due to delay in returning books
And wants to pay the fine
When Mrs. Robinson starts the process
Then the LMS works with the payment provider to process fee
And once the fee is paid marks the fine as paid on the patron account
And removes the borrowal restriction

# Regular patron places a hold request on an unavailable book

Scenario: Regular patron places a hold on an unavailable book
Given Mr Maddy wants to borrow the 'The Three Musketeers' which is not available at the time
And has less than two active holds
When Maddy places a hold request
Then LMS adds Maddy's hold request to the end of the hold queue
And then sends a notification to Maddy about the successful hold request


# Regular patron tries placing a hold when they already have maximum active hold requests

Scenario: Regular patron tries placing a hold when they already have maximum active hold requests

Given Maddy wants to borrow the 'Art of War' which is unavailable at the time
And he already has two active holds on his account
When Maddy tries to submit the hold request
Then LMS rejects the hold request
Amd provides an error message to denote that already maximum active hold requests are present
And sends a failure email notification to Maddy

# Regular patron tries placing a hold when they have overdue books

Scenario: Regular patron tries placing a hold when they have overdue books

Given Maddy wants to borrow the 'Art of War' which is unavailable at the time
And Maddy has an overdue book
When he tries placing a hold request
Then LMS system rejects the hold request
And provides an error message to return the overdue book
And sends a failure email notification to Maddy

# Regular patron tries placing a hold when they have pending fines

Scenario: Regular patron tries placing a hold when they have pending fines

Given Maddy wants to borrow the 'Art of War' which is unavailable at the time
And Maddy has an pending fines to be cleared
When he tries placing a hold request
Then LMS system rejects the hold request
And provides an error message to clear the pending fines
And sends a failure email notification to Maddy

# Research patron places a hold request on an unavailable book

Scenario: Regular patron places a hold on an unavailable book
Given Dr Patel wants to borrow the 'Clean Code' which is not available at the time
And has less than five active holds
When he places a hold
Then LMS adds Dr. Patel's hold request to the end of the hold queue
And then sends a notification to Dr. Patel about the successful hold request

# Research patron tries placing a hold when they already have maximum active hold requests

Scenario: Research patron tries placing a hold when they already have maximum active hold requests

Given Dr. Patel wants to borrow the 'Clean Architecture' which is unavailable at the time
And he already has five active holds on his account
When he tries to submit the hold request
Then LMS rejects the hold request
Amd provides an error message to denote that already maximum active hold requests are present
And sends a failure email notification to Dr. Patel

# Research patron tries placing a hold when they have overdue books

Scenario: Regular patron tries placing a hold when they have overdue books

Given Dr. Patel wants to borrow the 'Clean Architecture' which is unavailable at the time
And he has an overdue book
When he tries placing a hold request
Then LMS system rejects the hold request
And provides an error message to return the overdue book
And sends a failure email notification to Dr. Patel

# Research patron tries placing a hold when they have pending fines

Scenario: Research patron tries placing a hold when they have pending fines

Given Dr. Patel wants to borrow the 'Clean Coder' which is unavailable at the time
And he has an pending fines to be cleared
When he tries placing a hold request
Then LMS system rejects the hold request
And provides an error message to clear the pending fines
And sends a failure email notification to Dr. Patel

# Notifying Patrons about available book on hold request
Scenario: Notifying Patrons about available book on hold request

Given a patron returns a boook
And the LMS finds an active hold request in the queue
Then the LMS notifies the first patron in the hold request queue
And links the book to the active hold request
And sets the hold expiry date two days after for regular patron or seven days after for research patron
And sends a notification to patron

# Notifying next in line patron on expiry of active hold request of another patron
Scenario : Notifying next in line patron on expiry of active hold request of another patron

Given Maddy has active hold request on 'The 1001 Arabian Nights'
When the active hold request of Maddy expires after two days of holding the book
Then LMS finds the next in line patron Sadie
And links the book to Sadie's hold request
And sets the hold expiry date two days after for regular patron or seven days after for research patron
And sends a notification to patron
