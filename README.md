# Mammal March Madness API

A JWT-authenticated RESTful API written in C#/.NET Core 2.2 for use by client applications in the annual [Mammal March Madness](http://mammalssuck.blogspot.com/2019/02/march-mammal-madness-2019.html) tournament. If you have feedback or would like to contribute, please reach out. I'm still learning and would love to hear from you.

## Current Features
* New user registration
* SHA256 password hashing
* JSON Web Tokens (JWT) generation
* JWT expiration and refresh
* GET:
  * Animals
  * Battle Participants
  * Users
  * Battle Results
  * Animals by Category

## In Progress
* Service to generate a fully seeded bracket

## TODO
* GET bracket picks for individual users
* Scoring service
* POST, PUT, DELETE functionality