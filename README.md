# Mammal March Madness API

A JWT-authenticated RESTful API written in C#/.NET Core 2.2 for use by client applications in the annual [Mammal March Madness](http://mammalssuck.blogspot.com/2019/02/march-mammal-madness-2019.html) tournament. If you have feedback or would like to contribute, please reach out. I'm still learning and would love to hear from you.

### Current Features
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

### In Progress
* Service to generate a fully seeded bracket

### TODO
* GET bracket picks for individual users
* Scoring service
* POST, PUT, DELETE functionality
* Create SQL file for inserting test data

## Development

### Set up your environment

Prerequisites:
* C# compatible IDE
* .NET Core 2.2 SDK and runtime
* PostgreSQL

Instructions:
1. Fork or clone this repository
1. Open solution file and restore packages
1. Create database tables using included 'mmm_bracket_postgres_create.sql' file
1. Configure user secrets for:
    * Database connection string
       - ```
          { Database: 
            {
              ConnectionString: ""
            }
          }
    * JWT secrets
      - ```
        { JWTSettings:
	      { 
            SecretKey: "random secure string here",
	        Issuer: "MMM_Bracket",
	        Audience: "MMM_Bracket_API"
          }
        }
1. Build and run solution
1. Authentication instructions:
    - Send POST request to /api/authentication/register with JSON body to register new user:
      ``` 
      {
        "username": "username",
        "password": "password"
      }
    - Include the returned JWT as bearer token for authenticating requests
    - Send POST request to /api/authentication/refresh with JSON body (including expired access token and valid refresh token) to refresh an expired token:
      ```
      {
      "accessToken": "",
      "refreshToken": ""
      }
