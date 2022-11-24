import Task from '../objects/Task';
import User from '../objects/User';
import NetController from './TasksController';

class TasksController{
    static tasksList = [];

    //return task from net controller
    static getTask(taskNumber){
        let res = {
            code: 200,
            data: {
                id: taskNumber,
                header: 'Tytuł',
                description: 'Opis',
                state: 1
            }   
        };

        res = {
            code: 404,
            data: {
                message: 'error test'
            }   
        }

        /*
        odblokować po dodaniu serwera
        let net = new NetController('GET', 'getTask?id=' + taskNumber);
        let res = net.getData();
        */

        return res;    
    }

    //return list of tasks from net controller
    static getTasks(){
        let res = {
            code: 200,
            data: [
                {
                    Id: 1,
                    Header: 'Tytuł',
                    Description: 'Opis',
                    State: 3
                },
                {
                    Id: 2,
                    Header: 'Rowe Trek, przegląd',
                    Description: 'Lorem Ipsum jest tekstem stosowanym jako przykładowy wypełniacz w przemyśle poligraficznym. Został po raz pierwszy użyty w XV w. przez nieznanego drukarza do wypełnienia tekstem próbnej książki. Pięć wieków później zaczął być używany przemyśle elektronicznym, pozostając praktycznie niezmienionym. Spopularyzował się w latach 60. XX w. wraz z publikacją arkuszy Letrasetu, zawierających fragmenty Lorem Ipsum, a ostatnio z zawierającym różne wersje Lorem Ipsum oprogramowaniem przeznaczonym do realizacji druków na komputerach osobistych, jak Aldus PageMaker',
                    State: 1
                },
                {
                    Id: 3,
                    Header: 'Nowy tytuł',
                    Description: 'Coś do zrobienia',
                    State: 2
                },
                {
                    Id: 4,
                    Header: 'Tytuł2',
                    Description: 'Opis2',
                    State: 1
                },
                {
                    Id: 5,
                    Header: 'Tytuł3',
                    Description: 'Opis3',
                    State: 4
                }
            ]
        };

        /*
        odblokować po dodaniu serwera
        let net = new NetController('POST', 'getTasks', User.user);
        let res = net.getData();
        */

        return res; 
    }

    //return id from net controller
    static addTask(){
        let nrOfTasks = this.objectLength(this.tasksList) + 1;

        let res = {
            code: 200,
            data: {
                id: nrOfTasks,
                header: Task.task.Header,
                description: Task.task.Description,
                state: 1
            }   
        };

        /*odblokować po dodaniu serwera
        let parameters ={
            user: User.user,
            task: Task.task
        }
        let net = new NetController('POST', 'getTasks', parameters);
        let res = net.getData();
        */

        return res;
    }

    //return true from net controller
    static updateTask(){
        let res = {
            code: 200,
            data: {
                id: Task.task.Id,
                header: Task.task.Header,
                description: Task.task.Description,
                state: 1
            }   
        };

        /*odblokować po dodaniu serwera
        let parameters ={
            user: User.user,
            task: Task.task
        }

        let net = new NetController('PUT', 'getTasks/?id='+Task.task.Id, parameters);
        let res = net.getData();
        */

        return res;
    }

    static objectLength( object ) {
        var length = 0;
        for( var key in object ) {
            if( object.hasOwnProperty(key) ) {
                ++length;
            }
        }
        return length;
    };
}

export default TasksController;