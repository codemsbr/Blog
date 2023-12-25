using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.AuthVm
{
    public class RegisterVM
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "İstifadəçi adınızı daxil edin!!!"), MaxLength(24)]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(ConfirmPassword)), RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{4,}$", ErrorMessage = "Wrong input for password")]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{4,}$", ErrorMessage = "Wrong input for password")]
        public string ConfirmPassword { get; set; }
    }
}
