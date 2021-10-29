using System.Collections.Generic;

namespace EFCoreAttachBugTest002.Models
{
    public class Holder
    {
        public Holder()
        {
            Examples = new HashSet<Example>();
        }

        public int Id { get; set; }

        public virtual ICollection<Example> Examples { get; set; }
    }
}
