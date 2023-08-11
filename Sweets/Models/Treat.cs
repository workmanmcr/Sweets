using System.Collections.Generic;
using Sweets.Models; 

namespace Sweets.Models  
{
    public class Treat  
    {
        public Treat()
        {
            this.JoinEntities = new HashSet<TreatFlavor>();  
        }

        public int TreatId { get; set; }  
      
        public string Name { get; set; } 
        public int FlavorId { get; set;} 
        public Flavor Flavor { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<TreatFlavor> JoinEntities { get; set; }  
}
}
