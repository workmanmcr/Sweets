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
        public List<Flavor> Flavors { get; set; }  
        public virtual ICollection<TreatFlavor> JoinEntities { get; set; }  
}
}
