Login Registration Application in Asp.Net Core to demonstrate vulnerabilities 

Requirements:
.Net 6
Visual Studio
Microsoft SQL Server

The application can be used to demonstrate the following vulnerabilities
SQL Injection
    In the branch, using admin and ‘ OR ‘1 = 1’ as inputs for Username and Password fields, the application grants direct unauthorized access to a user’s data from the database.
    fix involves using EntityFramework instead of raw query.
Shoulder Surfing
    The password input field of this branch does not conceal the input.
    The fix is to use type="password" in the Index.cshtml file.
Unrestricted File Upload
    The file input of this branch does not restrict the type of files permitted for user profile pictures.
    The fix comes in 3 phases:
        Updating the View to accept PNG files.
        Adding a function to the Controller to check the content type.
        Adding a function to check the file signature.
Hard Coded Credentials
    To use this vulnerability, comment sensitive credentials (I used vm username password and database connection string) to a View.
    The fix is to remove these comments.
Weak password policy
    The vulnerability lies in not allowing passwords to be more than 5 characters long.
    BurpSuite and the list of 50 common 5-letter passwords can be set up to demonstrate how a brute-force attack can occur.
    The fix is setting up a better password policy.
