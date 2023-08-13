using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sweets.Models
{
  public class Treat
  {
    public int TreatId { get; set; }
    
    public string Name { get; set; }
    public Flavor Flavor { get; set; }
    public List<TreatFlavor> JoinEntities { get;}
    public ApplicationUser User { get; set; }
  }
}