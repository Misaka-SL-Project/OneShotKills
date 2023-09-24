using Exiled.API.Interfaces;
using System.ComponentModel;

namespace OneShotKills;

public sealed class Config : IConfig
{
     public bool IsEnabled { get; set; } = true;
     public bool Debug { get; set; } = false;
     [Description("Whether or not doctor should instant kill")]
     public bool Scp049InstaKill { get; set; } = false;
     [Description("Return larry to how he used to be: 1 hit = sent to pocket dimension")]
     public bool EnableOldLarry { get; set; } = true;
}
