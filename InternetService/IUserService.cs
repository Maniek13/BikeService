using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace SOAPService
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        UserResponde CheckIsUser(UserRequest userRequest);

    }

    [DataContract]
    public class UserResponde
    {
        int _UserId = 0;

        [DataMember]
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
    }

    [DataContract]
    public class UserRequest
    {
        string _Login = "";
        string _Pasword = "";

        [DataMember]
        public string Login
        {
            get { return _Login; }
            set { _Login = value; }
        }

        [DataMember]
        public string Password
        {
            get { return _Pasword; }
            set { _Pasword = value; }
        }
    }
}
