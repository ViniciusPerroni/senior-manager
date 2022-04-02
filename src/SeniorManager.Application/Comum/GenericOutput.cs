using System.Collections.Generic;

namespace SeniorManager.Application.Comum
{
    public class GenericOutput<T>
    {
        public GenericOutput()
        {
            Errors = new List<string>();
        }

        public GenericOutput(T data) : this()
        {
            Data = data;
        }

        public T Data { get; set; }
        public IList<string> Errors { get; set; }
        public bool Ok { get { return Errors.Count == 0; } }

        public void AddError(string error)
        {
            Errors.Add(error);
        }
        public void AddErrors(IList<string> errors)
        {
            ((List<string>)Errors).AddRange(errors);
        }
    }
}