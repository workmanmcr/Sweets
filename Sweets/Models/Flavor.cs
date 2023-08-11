using System.Collections.Generic;

namespace Sweets.Models
{
  public class Flavor
  {
    public Flavor()
    {
      this.JoinEntities = new HashSet<TreatFlavor>();
    }

    public int FlavorId { get; set; }
    public string FlavorName { get; set; }  
    public List<Treat> Treats { get; set; } 
    public string Name { get; set; }
    public ApplicationUser User { get; set; }
    public virtual ICollection<TreatFlavor> JoinEntities { get; set; }
  }
}