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
    
        await NetController.getDataFromSOAP('http://tempuri.org/GetTask', body, 'GetTaskResult');
    }

    static async getTasks(){
        let body = '<?xml version="1.0" encoding="utf-8"?>\
            <soap12:Envelope xmlns:soap12="http://www.w3.org/2003/05/soap-envelope">\
            <soap12:Body>\
                <GetTasks xmlns="http://tempuri.org/">\
                    <user>\
                        <Id>'+User.user.Id+'</Id>\
                        <Login>'+User.user.Login+'</Login>\
                        <Password>'+User.user.Password+'</Password>\
                    </user>\
                </GetTasks>\
            </soap12:Body>\
            </soap12:Envelope>';
    
        await NetController.getDataFromSOAP('http://tempuri.org/GetTasks', body, 'GetTasksResult');
    }


    static async addTask(){
        let body = '<?xml version="1.0" encoding="utf-8"?>\
            <soap12:Envelope xmlns:soap12="http://www.w3.org/2003/05/soap-envelope">\
            <soap12:Body>\
                <AddOrder xmlns="http://tempuri.org/">\
                    <user>\
                        <Id>'+User.user.Id+'</Id>\
                        <AppId>'+User.user.AppId+'</AppId>\
                        <Login>'+User.user.Login+'</Login>\
                        <Password>'+User.user.Password+'</Password>\
                    </user>\
                    <order>\
                        <TaskID>0</TaskID>\
                        <AppID>'+User.user.AppId+'</AppID>\
                        <Header>'+Task.task.Header+'</Header>\
                        <Description>'+Task.task.Description+'</Description>\
                        <State>'+Task.task.State+'</State>\
                    </order>\
                </AddOrder>\
            </soap12:Body>\
            </soap12:Envelope>';

        await NetController.getDataFromSOAP('http://tempuri.org/AddOrder', body, 'AddOrderResult');
    }

    static async updateTask(){
        let body = '<?xml version="1.0" encoding="utf-8"?>\
        <soap12:Envelope xmlns:soap12="http://www.w3.org/2003/05/soap-envelope">\
        <soap12:Body>\
            <EditOrder xmlns="http://tempuri.org/">\
                <user>\
                    <Id>'+User.user.Id+'</Id>\
                    <AppId>'+User.user.AppId+'</AppId>\
                    <Login>'+User.user.Login+'</Login>\
                    <Password>'+User.user.Password+'</Password>\
                </user>\
                <order>\
                    <TaskId>'+Task.task.TaskId+'</TaskId>\
                    <AppId>'+User.user.AppId+'</AppId>\
                    <Header>'+Task.task.Header+'</Header>\
                    <Description>'+Task.task.Description+'</Description>\
                    <State>'+Task.task.State+'</State>\
                    <TaskIdKey>0</TaskIdKey>\
                </order>\
            </EditOrder>\
        </soap12:Body>\
        </soap12:Envelope>';

        await NetController.getDataFromSOAP('http://tempuri.org/EditOrder', body, 'EditOrderResult');
    }

    
    static async deleteTask(){
        let body = '<?xml version="1.0" encoding="utf-8"?>\
        <soap12:Envelope xmlns:soap12="http://www.w3.org/2003/05/soap-envelope">\
        <soap12:Body>\
            <DeleteOrder xmlns="http://tempuri.org/">\
                <user>\
                    <Id>'+User.user.Id+'</Id>\
                    <AppId>'+User.user.AppId+'</AppId>\
                    <Login>'+User.user.Login+'</Login>\
                    <Password>'+User.user.Password+'</Password>\
                </user>\
                <orderId>'+Task.task.TaskId+'</orderId>\
            </DeleteOrder>\
        </soap12:Body>\
        </soap12:Envelope>';

        await NetController.getDataFromSOAP('http://tempuri.org/DeleteOrder', body, 'DeleteOrderResult');
    }

    static Sort(property, type){
        return Array.from(this.tasksList).sort(this.dynamicSort(property, type));
    }

    static dynamicSort(property, order) {
        return function (a,b) {
            var result;
            if(typeof a[property] === 'string'){

                result = a[property].localeCompare(b[property]);
            }
            else{
                result = (a[property] < b[property]) ? -1 : (a[property] > b[property]) ? 1 : 0;
            }
            return result * order;
        }
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