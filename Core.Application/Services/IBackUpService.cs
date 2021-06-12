using Core.Domain.Entities;

namespace Core.Application.Services
{
    public interface IBackUpService
    {
        /// <summary>
        /// It perform a full sql server backup 
        /// </summary>
        /// <param name="salva"></param>
        void FullBackUpAsync(Salvas salva);
    }
}