import NetController from './TasksController';

class UserController{
    static checkIsUser(login, password){
        return {
            code: 200,
            data: {
                id: 1,
                name: login,
                password: password
            }
        };

        //odblokowac po dodaniu serwera
        let user = {
            Id:0,
            Name: login,
            Password: password
        }

        let net = new NetController('POST', 'checkIsUser', user);

        let res = net.getData();

        return res.data;
    }
}

export default UserController;