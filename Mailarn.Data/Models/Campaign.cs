namespace Mailarn.Data
{
    public class Campaign
    {
        public int camp_Id { get; set; }
        public int u_Id { get; set; }
        public bool camp_ACStat { get; set; }
        public int eml_Id { get; set; }
        public int tmp_Id { get; set; }
        public int sentmail_count { get; set; }
        public int ld_count { get; set; }
        public int sc_rate { get; set; }
    }
}
