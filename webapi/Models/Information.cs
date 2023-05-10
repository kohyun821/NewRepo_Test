using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Information
{
    public int InformationId { get; set; }

    public int MaterialId { get; set; }

    public int EmployeeId { get; set; }

    public bool InformationStatus { get; set; }

    public int InformationAmount { get; set; }

    public DateTime InformationDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Material Material { get; set; } = null!;
}
