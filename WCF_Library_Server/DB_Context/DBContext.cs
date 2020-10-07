namespace WCF_Library_Server.DB_Context
{
    using System.Data.Entity;

    public class DBContext : DbContext
    {
        public DBContext() : base("name=Users")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserRank> UserRanks { get; set; }
    }
}