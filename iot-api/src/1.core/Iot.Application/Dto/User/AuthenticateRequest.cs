using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Application.Dto.User
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }
        //public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
