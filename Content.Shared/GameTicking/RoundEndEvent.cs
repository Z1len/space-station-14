namespace Content.Shared.GameTicking;

public sealed class RoundEndEvent : EntityEventArgs
{
    public int RoundId { get; }
    public TimeSpan Duration { get; }
    public RoundEndEvent(int roundId, TimeSpan duration)
    {
        Duration = duration;
        RoundId = roundId;
    }
}
