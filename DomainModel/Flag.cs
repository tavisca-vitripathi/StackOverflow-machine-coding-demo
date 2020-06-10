namespace DomainModel
{
    public class Flag
    {
        public string Id;
        public string FlaggedBy { get; set; }
        public string ReferenceLink { get; set; }
        public string MoreInfo { get; set; }
        public bool IsApproved { get; set; } = false;
        public Moderator ApprovedBy { get; set; } = null;

    }
}