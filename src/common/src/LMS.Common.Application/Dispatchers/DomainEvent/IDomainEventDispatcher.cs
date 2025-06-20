﻿using System.Reflection;
using LMS.Common.Domain;

namespace LMS.Common.Application.Dispatchers.DomainEvent;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IDomainEvent domainEvent, Assembly assembly, CancellationToken cancellationToken = default);
}
