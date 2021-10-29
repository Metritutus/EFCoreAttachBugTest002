using EFCoreAttachBugTest002.Models;
using System.Collections.Generic;

namespace EFCoreAttachBugTest002.DataSeeding
{
    public partial class SeedData
    {
        public static IEnumerable<MysteryType> MysteryTypes
        {
            get
            {
                yield return new MysteryType()
                {
                    Id = 1,
                    Name = "Mystery1"
                };
                yield return new MysteryType()
                {
                    Id = 2,
                    Name = "Mystery2"
                };
            }
        }
    }
}
