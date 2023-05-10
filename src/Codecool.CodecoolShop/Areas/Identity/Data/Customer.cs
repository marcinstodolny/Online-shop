using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Codecool.CodecoolShop.Areas.Identity.Data
{
    public class Customer : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvachar(20)")]
        public string FirstName { get; set; }
    }
}
