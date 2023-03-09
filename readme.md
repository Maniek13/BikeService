In progres version apk v 0.9b1

To tests:

1. Install app from BikeService.apk
login data
login: test
password: 12345

Search tasks(In database are two order numbers: 11BS112345 and 21BS112345)

2. Control panel (UWP) open from AppPackages folder 
login: test
password: 12345

-------------------------------------------------------------------------------------------------------------
Server:
1. To use please compile apk with created settings file and configure IIS and DataBase
2. Run sql to create database (Or update you upgrade server from 0.9a to 0.9b)
3. Run sql CreateIndex to set indexes 
4. Run sql CreateAdministrationDB
5. Create user in database
6. Add ConectionString to database on settings.xml file
7. Publish web service to ISS or ect

Android app:
1. Add file Settings.js in src/objects with path to WebService like this:
______________________________________
class Settings{
    static SOAPAdress = 'serwer.asmx'
}z`

export default Settings;
_______________________________________

2. Compile apk and run on device or emulator (v0.9b1)

Control panel:

1. Working on windows 10+
2. Open from AppPackages folder

--------------------------------------------------------------------------------------------------------------

Android App
Main

<img src="https://user-images.githubusercontent.com/47826375/202868309-54d9a319-cf5e-4820-bb3f-720f82eb5ab3.jpg" width="100"><img src="https://user-images.githubusercontent.com/47826375/202870025-63d24a41-28f3-4d3f-a3b9-3e73ce3912cf.jpg" width="100">

Login 

<img src="https://user-images.githubusercontent.com/47826375/202868312-e45c481c-8593-4d0d-bef1-2d1e8f22f714.jpg" width="100">

Administration panel

<img src="https://user-images.githubusercontent.com/47826375/216843390-88495e78-c85a-4a92-8f5c-ac1c689e74ae.jpg" width="100"><img src="https://user-images.githubusercontent.com/47826375/216843378-682236e4-6e0b-4298-8fd9-47979f0c4b98.jpg" width="100">

Update task 

<img src="https://user-images.githubusercontent.com/47826375/202868316-79d59045-7ab1-414d-b140-01f6ebfc571f.jpg" width="100"><img src="https://user-images.githubusercontent.com/47826375/202868317-c3a039ec-f84e-4fa4-8ee1-7e56547d57a0.jpg" width="100">

Add task 

<img src="https://user-images.githubusercontent.com/47826375/202868319-eddc329d-5636-4c34-ac08-d0946f8c7ee7.jpg" width="100">


Administration panel

Login

<img src="https://user-images.githubusercontent.com/47826375/224117508-3bed6972-f1d2-492e-a6a4-d59f7442c399.png" width="100">

Main

<img src="https://user-images.githubusercontent.com/47826375/224117755-33d6eca3-5ba4-4b06-bda3-8d964394dc40.png" width="100">

Add/Edit

<img src="https://user-images.githubusercontent.com/47826375/224117818-2494fddf-f8bb-4f93-82f8-cf245e9aeee2.png" width="100">

