using System.ComponentModel.DataAnnotations;

namespace Mailarn.Data.Construct
{
    public class Email
    {
        [Key]
        public int Id { get; set; }
        public int U_Id { get; set; }
        public int Eml_Id { get; set; }
        public int Emlist_Id { get; set; }
        public string Email_Add { get; set; }
        public string F_Name { get; set; }
        public string L_Name { get; set; }
    }
}
