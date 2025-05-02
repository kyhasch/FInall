public interface IUpgradeable
{
    bool IsUpgraded { get; }
    void Upgrade();
    int GetSellValue();
}
