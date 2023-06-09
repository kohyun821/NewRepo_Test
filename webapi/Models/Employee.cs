﻿using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Information> Information { get; set; } = new List<Information>();
}
