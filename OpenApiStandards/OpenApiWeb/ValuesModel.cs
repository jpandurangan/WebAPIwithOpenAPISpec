using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenApiWeb
{
    public class ValuesModel:IValidatableObject
    {
        [Required]
        public string Name { get; set; }

        [Range(60000,70000)]
        public int Zip { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            if (Name == "Keefe" && Zip == 69999)
            {
                results.Add(new ValidationResult("Name does not exist in Zip", new[] {nameof(Name), nameof(Zip)}));
            }

            return results;
        }
    }
}
