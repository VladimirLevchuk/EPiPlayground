Environment
===========

 1. *%windir%\system32\drivers\etc\hosts* file contains
>127.0.0.1 	local.alloy.com local.alloy.no

 2. IIS site configured to look at *.\Alloy85* folder
 3. IIS Application Pool configured to use NET Framework 4.0 and Integrated Pipeline
 4. SQL Server Express 2012 instance named '*.\SQL_2012*' installed (*)

(*) - for different instance names connection string config should be changed appropriately
