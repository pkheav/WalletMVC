using Microsoft.EntityFrameworkCore;

namespace WalletMVC.Models;

public class WalletContext : DbContext
{
    public WalletContext(DbContextOptions<WalletContext> options)
        : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; } = null!;
}