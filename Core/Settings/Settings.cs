using Core.Interfaces;

namespace Core.Settings;

public class Settings: ISettings
{
    public static Settings AllSettings { get; private set; }
    public SqlSetting SqlSetting { get; set; }
    public RulesSetting RulesSetting { get; set; }

    public static void Set(Settings settings)
    {
        AllSettings = settings;
    }
}