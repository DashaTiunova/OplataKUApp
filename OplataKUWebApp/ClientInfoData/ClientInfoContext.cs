using Microsoft.EntityFrameworkCore;

namespace ClientInfoData
{
    public class ClientInfoContext : DbContext
    {
        public DbSet<ClientInfo> ClientInfos { get; set; }

        public DbSet<PayInfo> PayInfos { get; set; }
        public ClientInfoContext(DbContextOptions<ClientInfoContext> options) : base(options)
        {

        }
    }
}
