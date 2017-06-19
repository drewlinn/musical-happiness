# C# Week 4 Code Review: Band Tracker Application

#### A program that allows users to create, track and manage Bands and the Venues they play at. 6/9/17

#### By **Andrew Dalton**

## Description

A website created with C# and HTML where a user can create a list of Bands and the Venues the play out, and so, organize shows.


### Specifications
| Behavior | Input | Output |
| :------- | :---- | :----- |
| User can create Venues | "The Colosseum" | "Success!" |
| User can read a Venue | "Click Hyperlink" | "The Colosseum" |
| User can view a list of Venues | "View All" | "The Colosseum", "In the Venue", "Dave's Bar" |
| User can update their Venues | "Edit the Colosseum" | "Success!" |
| User can delete Venues | "Delete this venue" | "Success!"
| A Venue can have multiple Bands | "Tom Waits", "Leonard Cohen", "Nick Drake"
| User can find a specific Venue | "The Colosseum" | Displays the Colosseum and it's artists. |
| User can create Bands | "Tom Waits" | "Success!" |
| User can read a Band | "Click Hyperlink" | "Tom Waits" |
| User can read a list of Bands| "View All" | "The Doors", "Pixies", "Radiohead" |
| User can update Bands | "Edit The Doors" | "Success!"
| User can delete Bands | "Delete this band" | "Success!" |
| User can find a specific Band | "Tom Waits" | Displays the artist and their venues. |
| A Band can have multiple Venues | "Pixies" | "In the Venue", "The Colosseum", "Dave's Bar" |
| User can create a Show, which includes one Band and one Venue | "Radiohead", "The Colosseum" |
| User can read a Show | "Radiohead", "The Colosseum", 2018/9/12 |
| User can read a list of Shows | "View All Shows" | Radiohead, Tom Waits, Animal Collective |
| User can update a Show | "Edit 11/12/18" | "11/14/18" |
| User can delete a Show | "Delete this show" | "Success!"
| User can find a specific show | "Radiohead" | "03/23/19", "The Colosseum" |

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

##Database and Tables

CREATE DATABASE band_tracker;
GO

USE band_tracker;
GO


CREATE TABLE bands(id INT IDENTITY(1,1), name VARCHAR(255), genre VARCHAR(255));
GO

CREATE TABLE venues(id INT IDENTITY(1,1), name VARCHAR(255), address VARCHAR(255));
GO

CREATE TABLE shows(id INT IDENTITY(1,1), band_id INT, venue_id INT, date DATETIME));
GO

## Known Bugs
* List of shows and individual show pages do not include artist or venue names yet. Need to finish testing and writing GetBand and GetVenue methods for our Show Object to make this possible.

The lack of a venues_bands table is not an oversight. Shows IS our join table.


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
