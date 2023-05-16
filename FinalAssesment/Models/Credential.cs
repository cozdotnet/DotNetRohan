using System;
using System.Collections.Generic;

namespace FinalAssesment.Models;

public partial class Credential
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

public class LoginCred
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}

