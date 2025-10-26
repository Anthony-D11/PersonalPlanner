# Personal Planner - Todo Task Management App

**Technology Stack: AngularJS, ASP.NET, PostgreSQL**

**Note: The site may take a bit long to launch if it hasn't been launched for long time as I'm only using free plans on Render and Vercel. Please be patient as it should be live in a minute or so**

## Motivation

I developed this application in order to prepare myself with knowledge about C# and ASP.NET. I've recently learned about these so I would like to implement them into a real-world application.

I used AngularJS for the frontend as I've not used it for a long time since I first knew about it during my first co-op work term at Canadian Institute for Cybersecurity and a ChatGPT-clone web interface back in 2023

## Note to myself

1. Deploy the client side to Vercel and deploy the server side to Render
2. When deploying angular project to Vercel, make sure the output directory points to the folder containing the html file

## Usage

### Instant method

The site is live at: `https://personal-planner-client.vercel.app`

### Local method

Make sure you have docker installed and it's running by the time you want to run this project locally

1. Clone the project to your local machine
2. Navigate to the project folder
3. `cd server` - Navigate to the server folder of the web application
4. `docker build -t personal-planner-server:latest .` - Build a Docker image for the server side
5. `docker run -d -p 8080:8080 personal-planner-server:latest` - Run a Docker container for the server based on the image built in the previous step
6. `cd ../` and then `cd client` - Navigate to the client folder of the web application
7. `docker build -t personal-planner-client:latest .` - Build a Docker image for the client side
8. `docker run -d -p 4200:4200 personal-planner-server:latest` - Run a Docker container for the client based on the image built in the previous step
9. The site is available at: `http://localhost:4200`

## Demo

![Add Category screen](https://github.com/anthony-d11/PersonalPlanner/blob/master/Demo1.png?raw=true)
![Add Tag screen](https://github.com/anthony-d11/PersonalPlanner/blob/master/Demo2.png?raw=true)
![Add new Todo](https://github.com/anthony-d11/PersonalPlanner/blob/master/Demo3.png?raw=true)
![Modify existing Todo](https://github.com/anthony-d11/PersonalPlanner/blob/master/Demo4.png?raw=true)
