using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SweetShop.ViewModels
{
  public class LoginViewModel
  {
    [Required]
    [Display(Name = "UserName")]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
  }
}