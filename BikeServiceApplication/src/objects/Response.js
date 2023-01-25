class Response{
    static response = {
        code:0,
        data: ''
    }
    static getDate(onEndLoading) {
        onTime = setInterval(() => {
        if(this.response.code !== 0){
          if(this.response.code === 1){
            onEndLoading("if");
          }
          else{
            onEndLoading("else");
          }
          clearInterval(onTime);
        }
      }, 100);
    }
}

export default Response;