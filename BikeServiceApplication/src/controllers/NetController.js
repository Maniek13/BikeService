import Response from '../objects/Response';
import Settings from '../objects/Settings';
import { XMLParser } from 'fast-xml-parser';

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

    static async getDataFromSOAP(SOAPAction, body, XMLElement){
        requestOptions = {
            method: 'POST',
            body: body,
            headers: {
                Accept: '*/*',
                'SOAPAction': SOAPAction,
                'Content-Type': 'text/xml; charset=utf-8'
              },
        };

        await fetch(Settings.SOAPAdress, requestOptions)
        .then(response => response.blob()
        .then(myBlob => {
            let reader = new FileReader();
            reader.addEventListener("loadend", function() {

            if(response.status !== 200){
                XMLElement = 'error';
            };

            let res = NetController.serializeXML(reader.result, XMLElement,);

            if(response.status !== 200){
                res.code = response.status;
            };

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

    static serializeXML(xml, element){
        let x = xml.indexOf('<resultCode>');
        let y = xml.indexOf('</resultCode>');

        let response = {
            code: Number(xml.slice(x+12, y)),
            data: ''
        }

        if(element === 'error' || response.code === -1){
            let x = xml.indexOf('<message>');
            let y = xml.indexOf('</message>');
            
            response.data = {
                message: xml.slice(x+9, y)
            }
        }
        else{
            let x = xml.indexOf('<Data>');
            let y = xml.indexOf('</Data>');
            let xmlObj = xml.slice(x+6, y)

            const parser = new XMLParser();
            response.data = parser.parse(xmlObj);
        }
        return response;
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
