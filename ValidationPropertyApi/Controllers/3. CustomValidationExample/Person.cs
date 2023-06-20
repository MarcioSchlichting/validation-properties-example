using System.ComponentModel.DataAnnotations;

namespace ValidationPropertyApi.Controllers.CustomValidationExample;

public class Person
{
    [MinimumLength(5)]
    public string Name { get; set; }
    
    public int Age { get; set; }
    
    public string EmailAddress { get; set; }
}

public class MinimumLengthAttribute : ValidationAttribute
{
    private readonly int _minimumLength;

    public MinimumLengthAttribute(int minimumLength)
    {
        _minimumLength = minimumLength;
    }
    
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        { 
            if (value.ToString().Length < _minimumLength)
                return new ValidationResult($"The {nameof(value)} has a minimum length is {_minimumLength}.");
        }

        return ValidationResult.Success;
    }
}