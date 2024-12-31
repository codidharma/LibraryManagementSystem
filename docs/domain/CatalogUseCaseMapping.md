This page describes all the use case mappings that we identified in the catalog context based on the requirements and process level discussions. These are documented using the BDD style specifications

# Adding a new regular type book

Scenario: Adding a new regular book to the catalog

Given the librarian Mrs. Robinson purchases five copies of a 'The Count of Monte Cristo'
When she adds a book with an ISBN number, author and publication details as a regular type book
Then LMS registers the book as a regular type book and add adds five copies of the book
And assigns locations to the copies in the regular rack



# Adding an instance of existing regular book
Scenario: Adding a new copy of existing book

Given the librarian Mrs. Robinson purchases a new copy of 'The Count of Monte Cristo'
When she adds the copy of the book
Then LMS registers the book copy against the existing book
And assigns a location to the copy in the regular rack

# Adding a new research type book

Scenario: Adding a new research book to the catalog

Given the librarian Mrs. Robinson purchases one copy of 'Data Structures and Algorithms in Python'
When she adds a book with an ISBN number, author and publication details as a research type book
Then LMS registers the book as a research type book and add adds one copy of the book
And assigns locations to the copies in the research rack



# Adding an instance of existing research book

Scenario: Adding a new copy of existing book

Given the librarian Mrs. Robinson purchases a new copy of 'Data Structures and Algorithms in Python'
When she adds the copy of the book
Then LMS registers the book copy against the existing book
And assigns a location to the copy in the research rack

# Searching books by Author

Scenario : Searching books by author

Given Maddy wants to search all the books present in catalog that are written by Rudyard Kipling
When Maddy submits his query
Then LMS provides a list of all the books present in the catalog that are written by Rudyard Kipling with their availability status and rack number if the book is available

# Searching books by Title

Scenario: Searching books by title

Given Maddy wants to search for the title 'Pride and Prejudice'
When Maddy submits his query
Then LMS provides all possible matches for the query with the book availability status and rack number if the books is available