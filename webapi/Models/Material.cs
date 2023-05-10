using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Material
{
    public int MaterialId { get; set; }

    public string MaterialName { get; set; } = null!;

    public int MaterialAmount { get; set; }

    public int MaterialTotal { get; set; }

    public bool? MaterialStatus { get; set; }

    public virtual ICollection<Information> Information { get; set; } = new List<Information>();
}
