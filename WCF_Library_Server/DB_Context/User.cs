using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ServiceModel;

namespace WCF_Library_Server.DB_Context
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public bool Gender { get; set; }

        // поля с данным модификатором обязательны для ввода
        [Required]
        public byte[] HashedPassword { get; set; }

        [Required, ForeignKey("Rank")]
        public int RankRefId { get; set; }
        public UserRank Rank { get; set; }


        // данного поля не будет в моделе
        [NotMapped]
        public OperationContext operationContext { get; set; }
    }
}
