namespace WalletMVC.Models;

public class Transaction {
    public long Id { get; set; }
    public long SourceWalletId { get; set; }
    public long TargetWalletId { get; set; }
    public float Amount { get; set; }
    public string Description { get; set; }
    public string? Secret { get; set; }
}