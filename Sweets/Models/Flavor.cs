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
    public int TreatID { get; set; }
    public Treat Treat { get; set; }
    public ApplicationUser User { get; set; }
    public virtual ICollection<TreatFlavor> JoinEntities { get; set; }
  }
}