using System;
using System.ComponentModel.DataAnnotations;
namespace Mailarn.Data.Construct
{
    public class Campaign
    {
        [Key]
        public int Id { get; set; }
        public int U_Id { get; set; }
        public int Camp_Id { get; set; }
        public string Camp_Name { get; set; }
        public string Camp_Dtls { get; set; }
        public DateTime Camp_Date { get; set; }
        public string Camp_ACStat { get; set; }
        public string Eml_Subject { get; set; }
        public string Emlist_Id { get; set; }
        public string Sender_Email { get; set; }
        public int Tmp_Id { get; set; }
        public string Tmp_Name { get; set; }
        public string Tmp_Body { get; set; }
        public int Sentmail_Count { get; set; }
        public int Ld_Count { get; set; }
        public int Succ_Rt { get; set; }
        public int Completed { get; set; }
    }
}