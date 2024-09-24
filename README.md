# AbpTask
Web API for managing halls reservations

# Features

## Authentication
To get access of Halls methods you have to pass authentication

### Register

#### Request
Post method for register request by email and password
![image](https://github.com/user-attachments/assets/d9aa1d0e-a182-4eca-9c1f-eed59dc081ba)

#### Responses
![image](https://github.com/user-attachments/assets/0950a9d9-bb6c-4e8f-a829-5e6bea8e4ff8)


### Login

#### Request
Post method for login by email and password
![image](https://github.com/user-attachments/assets/b354718b-e579-40cf-9b58-a73094a2be98)

#### Responses
![image](https://github.com/user-attachments/assets/d37d5ec3-2186-4650-9ff2-1f34dba80cac)


## Halls
You can create, reserve, remove and serching halls by this methods
![image](https://github.com/user-attachments/assets/9c07c6d2-6bed-4aac-9324-102b42d4bb27)

### Create
Creates new hall and returns id of it if all conditions satisfied, otherwise - returns ProblemDetails

#### Request
![image](https://github.com/user-attachments/assets/4cef66d9-44a9-4f32-9e6d-63cacffb43cd)

#### Responses
![image](https://github.com/user-attachments/assets/a4f758cd-05c0-4d66-b28e-02e179181db8)
![image](https://github.com/user-attachments/assets/8537ad6f-4d4d-486b-acda-c942d013cd8e)

### Remove
Removes hall by id if hall not found returns ProblemDetails

#### Request
![image](https://github.com/user-attachments/assets/5e9169fd-33dc-4bf6-b5c7-57da0b878121)

#### Responses
![image](https://github.com/user-attachments/assets/27293ce5-3ca0-43ad-819d-1bd1609e32d5)
![image](https://github.com/user-attachments/assets/08fc4d4d-d3a9-4581-a9e6-a75871fecfd3)

### Reserve
Reserves hall by id and given startd datetime and duration in hours, returns total cost of reservation if success, otherwise - returns ProblemDetails

#### Request
![image](https://github.com/user-attachments/assets/ec16ba41-ddb2-4b99-b5c4-a6fe602f761b)

#### Responses
![image](https://github.com/user-attachments/assets/fbfae783-2cea-4bc5-bba6-7f3c5b3ec975)
![image](https://github.com/user-attachments/assets/2ecbf153-4b63-482c-9c63-dbe4936a876b)

### Update
Update properies of hall by given id if success, otherwise - returns ProblemDetails

#### Request
![image](https://github.com/user-attachments/assets/5f8405b5-9225-4db3-b087-94c383fc3980)

#### Responses
![image](https://github.com/user-attachments/assets/2ddf63cc-78cb-4638-bad1-3e2db0839b3d)
![image](https://github.com/user-attachments/assets/164d24e4-573a-44e0-8e5c-b4b75ce54920)

### Search
Searches halls by capacity and free reservation time by given start datetime and duration in hours if no one sitisfied condition returns empty collection

#### Request
![image](https://github.com/user-attachments/assets/f5499b55-b242-4e09-b578-6e990939edac)

#### Responses
![image](https://github.com/user-attachments/assets/1f3d3294-897a-4b45-8b5b-671caa80c79d)
![image](https://github.com/user-attachments/assets/fffb54df-13b8-40be-b634-b6a0beb6bad2)








 









 





