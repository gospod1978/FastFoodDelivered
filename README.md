Fast Food Delivered
ASP .NET Core 3.1 Web Application Project

C# Web Development Path at Software University, Bulgaria

https://fastfooddeliverd.azurewebsites.net

ABOUT my web project:

Fast Food Delivered (FFD) is an online orders and delivered food.
The platform meets clients’ drivers and restaurants for only one click.

Integrate Multilanguage - English and Bulgarian

Database
Microsoft SQL Server along with Entity Framework Core were used to create and store the values. 
The database schema consists of the following main entities:
1. Users     
2. Roles     
3. Addresses    
4. Areas     
5. Categories
6. Cities    
7. Comments  
8. Couriers     
9. Documents 
10. Emails
11. LocationObjects     
12. Locations    
13. Orders    
14. Photos
15. Pictures  
16. Posts     
17. Purchases    
18. Streets   
19. Restaurants 
20. UserDatas 
21. Vehicles  
22. WorkingAreas

https://github.com/gospod1978/FastFoodDelivered/blob/master/src/Web/AspNetCoreTemplate.Web/wwwroot/files/shemaDB.png

Backend

The web project contains:

4 different areas: Identity, Administration, Courier, Restaurant

18  services

19 controllers

81 views

The first time when you made registration - Project create User:

UserName: Admin

User email: admin@admin.com

Password: admin2020

You need this user to have full access to all code!

Features
Guest

This web platform allows a guest to the website to view and find all Menu, to search by category, by restaurants, by area.
A guest can also contact the support of the website, can also apply to Courier and apply to Restaurant. 

Users

Signed in user has seen details  Menu, can also apply to Courier and apply to restaurant. Need to create Address to Delivered. Users can see history of Orders.

Courier 

All in User + add vehicle, add working area. This is very important when system choose which courier to take the order. To see all document which Admin send to them, only see and downloads. Change status order – accept, delivering, delivered to the customer. 

Restaurant

Create Menu, change status to prepared and delivered by courier. Change promotion. Change Working, if is not working all Menu whit this Restaurant User stop to see. See all document and money which they need to take at FFD.

Admin

All functionality User, Courier, Restaurant +
Create City, create Area, Upload documents, Delete documents Approve User to be Courier or Restaurant. 
Area is very important because he help to calculate delivery price and time to delivery. Every user has Location this area when we need to deliver the food. Every Restaurant has Location, this is the distance Client – Restaurant. System search first Courier in Area Restaurant, if there don’t have then next to the area Restaurant. Only Courier can change the Working Location, when accept the order and delivered to Client he has change working location to be location Client.

Technologies Used
This website is designed and runs using the main technologies below:

- C#
- ASP.NET Core 3.1
- Entity Framework Core 3.1
- MS SQL Server
- Bootstrap 4
- JavaScript
- HTML5
- CSS
- MS Visual Studio 2019
- MS SQL Server Management Studio 2017
- Microsoft Azure
- SendGrid


This website has been created solely for educational purposes.

Mihail Gospodinov
 
Mobile :+356 79790197 / +359897000024
