using System.ComponentModel.DataAnnotations;

namespace ByteartRetail.Web.MVC.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住密码？")]
        public bool RememberMe { get; set; }
    }
}
