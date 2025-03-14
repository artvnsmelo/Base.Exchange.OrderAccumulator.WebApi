namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Settings;

public class QuickFixSettings
{
    public string Config = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, "Config", "acceptor.cfg");
}
