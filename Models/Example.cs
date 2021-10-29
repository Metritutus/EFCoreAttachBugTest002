namespace EFCoreAttachBugTest002.Models
{
    public class Example
    {
        public int Id { get; set; }
        public int? IntegerValue { get; set; }

        public int? MysteryTypeId { get; set; }

        public virtual Holder Holder { get; set; } // Removing this navigation property seems to fix the behaviour.
        public virtual MysteryType MysteryType { get; set; }
    }
}
