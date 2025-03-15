﻿using LMS.Common.Domain;

namespace LMS.Modules.Membership.API.Common.Domain;

internal sealed class PatronType : Enumeration
{
    public static readonly PatronType Regular = new(1, "Regular");
    public static readonly PatronType Research = new(2, "Research");

    private PatronType(int id, string name) : base(id, name)
    { }
}
