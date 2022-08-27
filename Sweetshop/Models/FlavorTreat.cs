using System.Collections.Generic;

namespace SweetShop.Models
{
  public class FlavorTreat
  { 
    public FlavorTreat()
    {
      this.JoinEntities = new HashSet<FlavorTreat>();
    }

    public int FlavorTreatId { get; set; }
    public int TreatId { get; set; }
    public int FlavorId { get; set; }
    public virtual Treat Treat { get; set; }
    public virtual Flavor Flavor { get; set; }

    public virtual ICollection<FlavorTreat> JoinEntities { get; set; }
  }
}