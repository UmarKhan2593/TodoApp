namespace Domain.Contracts
{
    public interface ISuspendable
    {
        bool? IsSuspended { get; set; }
    }
}
