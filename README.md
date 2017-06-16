# C# Week 4 Code Review: Band Tracker Application

#### A program that allows users to create, track and manage Bands and the Venues they play at. 6/9/17

#### By **Andrew Dalton**

## Description

A website created with C# and HTML where a user can create a list of Bands and the Venues the play out, and so, organize shows.


### Specifications
| Behavior | Input | Output |
| :------- | :---- | :----- |
| User can create Venues |
| User can read one or more Venues |
| User can update their Venues |
| User can delete Venues |
| A Venue can have many Bands |
| User can find a specific Venue |
| User can create Bands |
| User can read one or more Bands |
| User can update Bands |
| User can delete Bands |
| User can find a specific Band |
| A Band can have multiple Venues |

## Setup/Installation Requirements

1. To run this program, you must have a C# compiler (try [Mono](http://www.mono-project.com)).
2. Install the [Nancy](http://nancyfx.org/) framework to use the view engine. Follow the link for installation instructions.
3. Clone this repository.
4. Open SSMS (SQL Server Management Studio which you can Download here [https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms])
5. Select File > Open > File and select your .sql file.
6. If the database does not already exist, add the following lines to the top of the script file "CREATE DATABASE [your_database_name]" "GO"
7. Save the file.
8. Click ! Execute.
9. Verify that the database has been created and the schema and/or data imported.
10. Open the command line (try using the Windows Powershell [https://msdn.microsoft.com/en-us/powershell/mt173057.aspx]) and navigate into the repository. Use the command "dnx kestrel" to start the server.
11. On your browser, navigate to "localhost:5004" and enjoy!

## Known Bugs
*

## Technologies Used
* C#
  * Nancy Framework
  * Razor View Engine
  * ASP.NET Kestrel HTTP server
  * xUnit
  * SQL and SSMS Database Software

* HTML

## Support and contact details

_If you notice any issues regarding my page or my code, please contact me at expandrew@gmail.com._

### License

*{This software is licensed under the GPL license}*

Copyright (c) 2017 **_{Andrew Dalton, Epicodus}_**
