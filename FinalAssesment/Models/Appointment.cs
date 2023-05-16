using System;
using System.Collections.Generic;

namespace FinalAssesment.Models;

public partial class Appointment
{
    public int AppId { get; set; }

    public int? UserId { get; set; }

    public string? UserName { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Credential? User { get; set; }
}

public class AppointmentUpdate
{
    public string? UserName { get; set; }

    public DateTime CreatedDate { get; set; }
}
