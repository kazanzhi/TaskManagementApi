using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Authentication
{
    public class LoginModel
    {
        public string UserName{ get; set; }
        public string Password{ get; set; }
    }
}
