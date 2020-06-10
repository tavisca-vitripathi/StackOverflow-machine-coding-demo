namespace DomainModel
{
    public class Moderator : User
    {
        public override Role UserRole() => Role.Moderator;
    }
}
