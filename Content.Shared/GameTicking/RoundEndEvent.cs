namespace Content.Shared.GameTicking;

public sealed class RoundEndEvent : EntityEventArgs
{
    public int RoundId { get; }

    public RoundEndEvent(int roundId)
    {
        RoundId = roundId;
    }
}
