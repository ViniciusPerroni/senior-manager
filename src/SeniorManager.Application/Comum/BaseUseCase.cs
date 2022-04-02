using System;
using System.Threading.Tasks;

namespace SeniorManager.Application.Comum
{
    public abstract class BaseUseCase<T, K, G> where  K : BaseInput where G : GenericOutput<T>
    {
        public async Task<G> Execute(K input) 
        {
            if (!input.IsValid)
            {
                var output = (G) new GenericOutput<T>();
                output.AddErrors(input.Errors);
                return output;
            }

            return await BussinesRole(input);
        }

        internal abstract Task<G> BussinesRole(K input);
    }
}