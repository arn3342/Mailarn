using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailarn.Data.Construct
{
    public class User
    {
        [Key]
        [MinLength(3)]
        public int U_Id { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string CompanyName { get; set; }
    }
}
