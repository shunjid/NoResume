# NoResume

<p align="left">
  <br>
  <a href="https://cloud.docker.com/repository/docker/shunjid/no-resume/builds">
    <img src="https://img.shields.io/docker/cloud/automated/shunjid/no-resume" alt="docker-automated">
  </a>
  <a href="https://cloud.docker.com/repository/docker/shunjid/no-resume/builds">
    <img src="https://img.shields.io/docker/cloud/build/shunjid/no-resume" alt="build-pass">
  </a>
  <a href="https://github.com/shunjid/NoResume">
    <img src="https://img.shields.io/github/languages/code-size/shunjid/NoResume" alt="code-size">
  </a>
  <a>
   <img src="https://img.shields.io/github/languages/count/shunjid/NoResume" alt="languages">
  </a>
  <a>
   <img src="https://img.shields.io/github/release/shunjid/NoResume" alt="release">
  </a>	
	
  <a href="https://actions-badge.atrox.dev/shunjid/NoResume/goto"><img alt="Build Status" src="https://img.shields.io/endpoint.svg?url=https%3A%2F%2Factions-badge.atrox.dev%2Fshunjid%2FNoResume%2Fbadge%3Fref%3Dmaster&style=flat" /></a>
  </a>

</p>


<img src="wwwroot/Screens.png" alt="Screen"/>

### Donate online

<div>
	<a href="https://www.buymeacoffee.com/shunjid">
		<img alt="Buy Me A Coffee" src="https://www.buymeacoffee.com/assets/img/custom_images/yellow_img.png" style="height: auto !important; width: auto !important;" />
	</a>
</div>



## Objectives

Main objectives of implementing this project are listed below to better understand what are the goals -

* Generating real time working profile of Software Engineers based on their works they have done on different platforms
* Comparing profile of Software Engineers with others
* Working with Application programming interface (API) 
* Authenticated and Verified resume resources
* Finding Quality Software Engineers
* Prediction of what technologies are going to be popular in the near future

## Target Users

Though this is going to be a general purpose service and so any one can use it but mainly I have targeted some users like :

* CSE/Software Engineering Graduates
* ACM ICPC Problem solvers
* Open source developers
* IT Companies who are hiring


## Functional Features
	
+ **Private Area**
    * Creating personal account for everyone
    * Signing in
    * Initiating Short Biography, Job Availability, Current Location
    * Initiating GitHub, Codeforces, UVA online Judge username/handlers
    * Privacy for different platforms




+ **Public Area**
    * Effort Summary base rating of Codeforces, GitHub & UVA
    * Chart representation of what the programmer has worked on those 3 platforms like:
        - Pie & Donough Chart
        - Bar Chart
        - Line & Animation
    * Comparison with other developers
    * Prediction of next popular Framework on a country or worldwide to better understand for newly enrolled CSE/SWE students or who are interested in this context to help them understand what language or framework should work with.
 

## Non-Functional Features

+ **Material User Interface**
    * Realistic visual cues inspired by ‘Paper and Ink’ design
    * Elements live in defined spaces with a clear hierarchy
    * Simple but smooth component transition with Charts
    
+ **Performance & Usability**
    * Though the main context of this application is dependent on the API provider performance, It will be loading user data as fast as possible after API responses
    * Application will be much more user friendly that any new user can easily understand what to do and where to go

+ **Security**
    * Security will be another major concern after design from the very ground up
    * User authentication,Front-end User Input Validation, Role based authorization, Audit & Logging, Exception handling & Cryptography will be concerned in the context of security for the application


## Resources 
* Framework: Dotnet Core 2.2
* CSS Framework : Materialize CSS 1.0.0
* Javascript Frameworks/Libraries
   * jQuery 3.4.1
   * ChartJS
   * Froala text editor
* Object Relational Mapping: Entity Framework Core 3.1.0
* External NuGet Packages
   * Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 2.2.0
   * Microsoft.AspNetCore.Identity --version 2.2.0
   * Microsoft.EntityFrameworkCore.Design --version 3.1.0
   * Microsoft.EntityFrameworkCore.Sqlite --version 3.1.0
   * Microsoft.EntityFrameworkCore.SqlServer --version 3.1.0
   * Microsoft.VisualStudio.Web.CodeGeneration.Design --version 3.1.0
   * Microsoft.AspNetCore.StaticFiles --version 2.2.0
