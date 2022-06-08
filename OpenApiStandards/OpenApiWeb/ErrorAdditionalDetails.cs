using System.Collections.Generic;
using System.Text.Json;

namespace OpenApiWeb
{
    public sealed class ErrorAdditionalDetails
    {
        public IDictionary<string, string> ExtraInfo { get; set; } = new Dictionary<string, string>();

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
