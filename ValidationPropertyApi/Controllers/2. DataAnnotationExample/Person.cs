using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ValidationPropertyApi.Controllers.DataAnnotationExample;

/// <summary>
/// Person with Data annotations
/// </summary>
public class Person
{
    [Required]
    [StringLength(maximumLength: 20, MinimumLength = 5)]
    public string Name { get; set; }
    
    [Required]
    [Range(minimum: 18, maximum: 100, ErrorMessage = "Age should be between 18 and 100")]
    public int Age { get; set; }
    
    [EmailAddress]
    public string EmailAddress { get; set; }
}