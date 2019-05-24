using System.ComponentModel.DataAnnotations;

namespace Mailarn.Data.Construct
{
    public class EmailList
    {
        [Key]
        public int Id { get; set; }
        public int U_Id { get; set; }
        public int Emlist_Id { get; set; }
        public string List_Name { get; set; }
        public string List_Dtls { get; set; }
        public string Eml_Id { get; set; }
    }
}
