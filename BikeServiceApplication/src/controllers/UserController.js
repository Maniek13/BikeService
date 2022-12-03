import NetController from './NetController';

class UserController{

    //return id from 
    static checkIsUser(login, password){
        let res = {
            code: 200,
            data: {
                id: 1
            }
        };

        let body = '<?xml version="1.0" encoding="utf-8"?>\
        <soap12:Envelope xmlns:soap12="http://www.w3.org/2003/05/soap-envelope">\
          <soap12:Body>\
            <LogIn xmlns="http://tempuri.org/">\
              <password>string</password>\
              <login>string</login>\
            </LogIn>\
          </soap12:Body>\
        </soap12:Envelope>'
        
        NetController.getDataFromSOAP('http://tempuri.org/LogIn', body);

       
        /*odblokowac po dodaniu serwera
        let net = new NetController('POST', 'checkIsUser', user);
        let res = net.getData();
        */
        
        return res;
    }
}

export default UserController;