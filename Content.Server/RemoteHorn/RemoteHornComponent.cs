namespace Content.Server.RemoteHorn
{
    [RegisterComponent]
public class RemoteHornComponent : Component
{
    [DataField("sound")] public string Sound = string.Empty;
}
}
