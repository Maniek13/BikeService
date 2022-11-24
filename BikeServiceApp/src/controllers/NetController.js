import Task from '../objects/Task'

class NetController{
    let url = '';
    let parameters = {};
    let method = '';
    let methods = {'GET', 'POST', 'PUT', 'DELETE'};

    constructor(method, callingFunctionUrl, parameters){
        this.method = method;
        this.parameters = parameters;
        this.url = url + '/' + callingFunctionUrl;
    }

    async function getData() {
        if(!methods.includes(this.method)){
            return {
                code: -1,
                data: 'Wrong method'
            }
        }

        const requestOptions = {};
        const formData = new FormData()

        if(this.method = 'GET' || this.method = 'DELETE'){
            requestOptions = {
                method: this.method,
                headers: {
                    Accept: 'application/json',
                    'Content-Type': 'application/json'
                  },
            };
        }
        else{
            for (var el in this.parameters){
                formData.append(el, this.parameters[i]);
            }

            requestOptions = {
                method: this.method,
                body: formData,
                headers: {
                    Accept: 'application/json',
                    'Content-Type': 'application/json'
                  },
            };
        }

        try{
         await fetch(adres, requestOptions)
            .then(response => response.json()
            .then(data => ({
                data: data
            }))
            .then(res => {
                else {
                    return{
                        code: res.data.status,
                        data: res.data.message
                    }
                }    
            }));
        }
        catch(err){
            Responde
            return {
                code: 500,
                data: 'server error'
            }
        }
    }
}

export default TasksController;