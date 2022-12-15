import Task from '../objects/Task';
import User from '../objects/User';
import NetController from './NetController';

class TasksController{
    static tasksList = [];

    static async getTask(taskNumber){
        let body = '<?xml version="1.0" encoding="utf-8"?>\
            <soap12:Envelope xmlns:soap12="http://www.w3.org/2003/05/soap-envelope">\
            <soap12:Body>\
                <GetTask xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://tempuri.org/">\
                <taskIDKey>'+taskNumber+'</taskIDKey>\
                </GetTask>\
            </soap12:Body>\
            </soap12:Envelope>';
    
        await NetController.getDataFromSOAP('http://tempuri.org/GetTask ', body, 'GetTaskResult');
    }

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

        return res; 
    }


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

        return res;
    }

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