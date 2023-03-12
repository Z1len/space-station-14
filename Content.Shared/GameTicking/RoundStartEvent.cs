namespace Content.Shared.GameTicking;

public sealed class RoundStartEvent : EntityEventArgs
{
    public int RoundId { get; }

    public RoundStartEvent(int roundId)
    {
        RoundId = roundId;
    }
}
