
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

            //zrtobic czyms innym
            reader.addEventListener("loadend", function() {
        
                return{
                    code: response.status,
                    data: {
                        message: reader.result
                    }
                }  
            })
            reader.readAsText(myBlob); 
        }))
        .catch((error) => {
               return {
                   code: 500,
                   data: 'server error'
               }
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