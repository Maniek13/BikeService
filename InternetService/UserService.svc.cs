using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace SOAPService
{
    public class UserService : IUserService
    {

        public UserResponde CheckIsUser(UserRequest userRequest)
        {
            if (userRequest == null)
            {
                throw new ArgumentNullException("no user");
            }

            UserResponde user = new UserResponde();

            user.UserId = 1;

            return user;
        }
    }
}
