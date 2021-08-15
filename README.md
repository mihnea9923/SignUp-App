# SignUp-App

This project represents a simple web application where the user creates an account into a fictive application and then he receives a confirmation email on the address used int signing up.
The components of this application are:
-html client
-nginx load balancer
-web api
-sql database
-rabbitmq queue
-.net console app for sending emails

Each component is represented as a docker container. Below is a graphical representation of the application:
![alt text](https://github.com/mihnea9923/SignUp-App/blob/master/architecture.png)

The flow is the following: 1)user fills in the data and presses "Signup" button which sends a http request to the nginx load balancer, 2)the nginx load balancer forwards the request to one of the api containers, 3)the api adds the account into the database and publishes a message containing user email into a rabbitmq queue, 4)the .net console app subscribes to that queue and for each received message will send a confirmation email back to the user (using gmail smtp server).



<b>Note: For running the app you will need to replace the environment variables "gmailUsername" and "gmailPassword" with an actual gmail account and password. in docker-compose.yaml </b>

<b>For running the app execute the following command: </b>
<p>docker-compose up --scale api=3 </p>



