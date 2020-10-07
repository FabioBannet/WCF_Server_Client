using System.ComponentModel.DataAnnotations;

namespace WCF_Library_Server.DB_Context
{
    public class UserRank
    {
        [Key]
        public int RankID { get; set; }

        [Required]
        public string RankName { get; set; }
    }
}
