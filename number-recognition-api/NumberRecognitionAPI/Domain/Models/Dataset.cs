namespace Domain.Models
{
    public class Dataset : BaseEntity
    {
        public string Label { get; set; }
        public byte[] ImageMatrix { get; set; }
        public bool IsTest { get; set; }
    }
}
