using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public bool? DepartmentStatus { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
