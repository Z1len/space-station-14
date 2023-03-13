using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Content.Server.Maps;
using Content.Shared.CCVar;
using Content.Shared.GameTicking;
using Robust.Shared.Configuration;
namespace Content.Server.RoundNotifications;

public sealed class RoundNotificationsSystem : EntitySystem
{
    [Dependency] private readonly IConfigurationManager _configurationManager = default!;
    [Dependency] private readonly IGameMapManager _mapManager = default!;

    private readonly HttpClient _httpClient = new();
    private string _webhook_url = String.Empty;
    public override void Initialize()
    {
      	SubscribeLocalEvent<RoundStartEvent>(OnStarting);
        SubscribeLocalEvent<RoundEndEvent>(OnEnded);
        _configurationManager.OnValueChanged(CCVars.DiscordNotificationWebhook, value => _webhook_url = value, true);

    }

    private void OnStarting(RoundStartEvent e)
    {
        if(string.IsNullOrEmpty(_webhook_url))
            return;
        var curMap = _mapManager.GetSelectedMap();
        var text = Loc.GetString("discord-start-round",
            ("id", e.RoundId),
#pragma warning disable CS8602
            ("map", curMap.MapName)
#pragma warning restore CS8602
        );
        var payload = new WebhookPayload()
        {
            Content = text
        };
        SendDiscordMessage(payload);
    }

    private void OnEnded(RoundEndEvent e)
    {
        if(string.IsNullOrEmpty(_webhook_url))
            return;
        var payload = new WebhookPayload()
        {
            Content = Loc.GetString("discord-end-round", ("id",e.RoundId))
        };
        SendDiscordMessage(payload);
    }

    private async void SendDiscordMessage(WebhookPayload webhookPayload)
    {

        var req = await _httpClient.PostAsync(_webhook_url,
            new StringContent(JsonSerializer.Serialize(webhookPayload),Encoding.UTF8,"application/json"));
    }

    private struct WebhookPayload
    {
        [JsonPropertyName("content")]
        public string Content { get; set; } = "";
        public WebhookPayload()
        {

        }
    }
}
