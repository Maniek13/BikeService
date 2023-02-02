In progres version apk v 0.9b

Install app from BikeService.apk

login data
login: test
password: 12345

Search tasks(In database are two order numbers: 11BS112345 and 21BS112345)


____________________________________________________________________________________
To use please compile apk with created settings file and configure IIS and DataBase


Run sql to create database (Or update you upgrade server from 0.9a to 0.9b)
Create user in database
Add ConectionString to database on settings.xml file
Publish web service to ISS or ect


Android app:

Add file Settings.js in src/objects with path to WebService like this:
______________________________________
class Settings{
    static SOAPAdress = 'serwer.asmx'
}z`

export default Settings;
_______________________________________

Compile apk and run on device or emulator


Main

<img src="https://user-images.githubusercontent.com/47826375/202868309-54d9a319-cf5e-4820-bb3f-720f82eb5ab3.jpg" width="100"><img src="https://user-images.githubusercontent.com/47826375/202870025-63d24a41-28f3-4d3f-a3b9-3e73ce3912cf.jpg" width="100">

Login 

<img src="https://user-images.githubusercontent.com/47826375/202868312-e45c481c-8593-4d0d-bef1-2d1e8f22f714.jpg" width="100">

Administration panel

<img src="https://user-images.githubusercontent.com/47826375/216471464-55a42f00-12de-4053-bc1f-0d65e2d014fc.jpg" width="100">

Update task 

<img src="https://user-images.githubusercontent.com/47826375/202868316-79d59045-7ab1-414d-b140-01f6ebfc571f.jpg" width="100"><img src="https://user-images.githubusercontent.com/47826375/202868317-c3a039ec-f84e-4fa4-8ee1-7e56547d57a0.jpg" width="100">

Add task 

<img src="https://user-images.githubusercontent.com/47826375/202868319-eddc329d-5636-4c34-ac08-d0946f8c7ee7.jpg" width="100">
