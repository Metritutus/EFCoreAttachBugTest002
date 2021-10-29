using System.Collections.Generic;

namespace EFCoreAttachBugTest002.Models
{
    public class MysteryType
    {
        public MysteryType()
        {
            Examples = new HashSet<Example>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Example> Examples { get; set; }
    }
}
