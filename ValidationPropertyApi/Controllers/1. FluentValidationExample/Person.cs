using FluentValidation;

namespace ValidationPropertyApi.Controllers.ModelStateExample;

/// <summary>
/// Person with Fluent Validation
/// </summary>
public class Person
{
    public string Name { get; set; }
    
    public int Age { get; set; }
    
    public string EmailAddress { get; set; }
}

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(person => person.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required")
            .Length(5, 20)
            .WithMessage("Name must be between 5 and 20 characters");

        RuleFor(person => person.Age)
            .NotNull()
            .NotEmpty()
            .WithMessage("Age is required")
            .InclusiveBetween(18, 100)
            .WithMessage("Age should be between 18 and 100");

        RuleFor(person => person.EmailAddress)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email address");
    }
}