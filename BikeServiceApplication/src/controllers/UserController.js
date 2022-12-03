import NetController from './NetController';

class UserController{

  static async checkIsUser(login, password){
    let body = '<?xml version="1.0" encoding="utf-8"?>\
    <soap12:Envelope xmlns:soap12="http://www.w3.org/2003/05/soap-envelope">\
      <soap12:Body>\
        <LogIn xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://tempuri.org/">\
        <user>\
          <Login>'+login+'</Login>\
          <Password>'+password+'</Password>\
          </user>\
        </LogIn>\
      </soap12:Body>\
    </soap12:Envelope>';

    await NetController.getDataFromSOAP('http://tempuri.org/LogIn', body, 'LogInResult');
  }
}

export default UserController;