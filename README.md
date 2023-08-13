#Sweets

#### By _** Mike Workman**_  

#### _This website allows the user to add flavors and treats to a database. To add flavors and treats and user must be logged in using identity._  

---


## Technologies Used

* _C#_
* _.NET_
* _HTML_
* _CSS_
* _SQL Workbench_
* _Entity Framework_
* _MVC_

---
## Description

_This is an MVC application that was built using C# that allows the user to create an account and create treats and flavors that connect using manay to many resltionships. you must register as a userr to be able to view, creat, edit, and delete treats and flavors  ._

---
## Setup and Installation Requirements
This Setup assumes you have GitBash and MySQL Workbench pre-installed. 
<br><small>If you do not have one or both installed, please begin with that setup below. 
<br>If you _do_ have both installed, move onto "initial setup".</small>

<details>
<summary><strong>GitBash and MySQL Workbench Setup</strong></summary>
<ol>
<li>https://git-scm.com/download/  
<li>Download Git and follow the setup wizard. 
<li>https://dev.mysql.com/downloads/workbench/     
<li>Download MySQL Workbench
<li>Follow the setup wizard & create a localhost server on port 3306.
<li>Keep track of your username and password, this will be used in the connection steps of <strong>"SQL Workbench Configuration"</strong>  
</details>
<details>
<summary><strong>Initial Setup</strong></summary>
<ol>
<li>Copy the git repository url: https://github.com/workmanmcr/Sweets.git
<li>Open a terminal and navigate to your Desktop with <strong>cd</strong> command
<li>Run,   
<strong>$ git clone https://github.com/workmanmcr/Sweets.git</strong>
<li>In the terminal, navigate into the root directory of the cloned project folder "Treat.solution".
<li>Navigate to the projects root directory, "Sweets".
<li>Move onto "SQL Workbench Configuration" instructions below to build the necessary database.
<br>
</details>

<details>
<summary><strong>SQL Workbench Configuration</strong></summary>
<ol>
<li>Create an appsetting.json file in the "Factory" directory  
   <pre>Treat.solution
   └── Sweets
    └── appsetting.json</pre>
<li> Insert the following code: <br>

<pre>{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=ryan_defea;uid=[YOUR-USERNAME-HERE];pwd=[YOUR-PASSWORD-HERE];"
  }
}</pre>
<small>*Note: you must include your password in the code block section labeled "YOUR-PASSWORD-HERE".</small><br>
<small>**Note: you must include your username in the code block section labeled "YOUR-USERNAME-HERE".</small><br>
<small>***Note: if you plan to push this cloned project to a public-facing repository, remember to add the appsettings.json file to your .gitignore before doing so.</small>

<li>In root directory of project folder "Sweets", run  
<strong>$ dotnet ef migrations add restoreDatabase</strong>
<li>Then run <strong>$ dotnet ef database update</strong>

<ol> 
  <li>Open SQL Workbench.
  <li>Navigate to "Sweets_Database" schema.
  <li>Click the drop down, select "Tables" drop down.
  <li>Verify the tables, you should see <strong>flavor</strong>, <strong>treat</strong>, & <strong>treatflavor</strong>.
  
</details>

<details>
<summary><strong>To Run</strong></summary>
Navigate to:  Treat.solution
   <pre>
   └── <strong>Sweets</strong></pre>

Run ```$ dotnet restore``` in the terminal.<br>
Run ```$ dotnet run``` in the terminal.
</details>
<br>


---
## Known Bugs

* No known bugs at this time

## License

_None_

Copyright (c) 2023 Mike Workman