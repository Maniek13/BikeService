import NetController from './TasksController';

class UserController{

    //return id from 
    static checkIsUser(login, password){
        let res = {
            code: 200,
            data: {
                id: 1
            }
        };

        /*odblokowac po dodaniu serwera
        let net = new NetController('POST', 'checkIsUser', user);
        let res = net.getData();
        */

        return res;
    }
}

export default UserController;