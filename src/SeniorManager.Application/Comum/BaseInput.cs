using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SeniorManager.Application.Comum
{
    public abstract class BaseInput
    {
        [JsonIgnore]
        internal bool IsValid
        {
            get
            {
                Validate();
                return Errors.Count == 0;
            }
        }
        [JsonIgnore]
        internal IList<string> Errors { get; set; }
        
        internal abstract void Validate();
    }
}