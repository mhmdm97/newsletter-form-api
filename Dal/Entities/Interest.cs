namespace newsletter_form_api.Dal.Entities
{
    public class Interest : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Subscriber> Subscribers { get; set; } = [];
    }
}