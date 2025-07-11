using Domain.Common.Models;

namespace Domain.Menu.Events;

public record MenuCreated(Menu menu): IDomainEvent;