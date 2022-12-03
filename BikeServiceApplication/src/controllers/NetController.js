import Response from '../objects/Response';

class NetController{
    static url = '';
    static parameters = {};
    static method = '';
    static methods = ['GET', 'POST', 'PUT', 'DELETE'];

    constructor(method, callingFunctionUrl, parameters){
        this.method = method;
        this.parameters = parameters;
        this.url = this.url + '/' + callingFunctionUrl;
    }

    async getData() {
        if(!methods.includes(this.method)){
            return {
                code: -1,
                data: 'Wrong method'
            }
        }

        const requestOptions = {};
        const formData = new FormData()

        if(this.method === 'GET' || this.method === 'DELETE'){
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

        await this.getFromServer(adres, requestOptions); 
    }


    static async logIn(login, password){
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

        await NetController.getDataFromSOAP('http://tempuri.org/LogIn', body);
    }

    static async getDataFromSOAP(SOAPAction, body){
        requestOptions = {
            method: 'POST',
            body: body,
            headers: {
                Accept: '*/*',
                'SOAPAction': SOAPAction,
                'Content-Type': 'text/xml; charset=utf-8'
              },
        };

        await fetch('http://178.235.60.107:7500/BikeWebService.asmx', requestOptions)
        .then(response => response.blob()
        .then(myBlob => {
            let reader = new FileReader();
            reader.addEventListener("loadend", function() {
                let res =  {
                    code: response.status,
                    data: {
                        message: reader.result
                    }
                }
                Response.response = res;
            })
            reader.readAsText(myBlob); 
        }))
        .catch((error) => {
            let res =  {
                code: 500,
                data: {
                    message: 'server error'
                }
            }  
            Response.response = res;
        });

        
    }

    async getFromServer(adres, requestOptions){
        try{
            await fetch(adres, requestOptions)
               .then(response => response.json()
               .then(data => ({
                   data: data
               }))
               .then(res => {
                   return{
                       code: res.data.status,
                       data: res.data.message
                   }
               }));
           }
           catch(err){
               return {
                   code: 500,
                   data: 'server error'
               }
           }
    }

   
}

export default NetController;