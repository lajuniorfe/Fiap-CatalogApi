using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.Dominio.Base.Repository
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        IList<T> ObterDados();
        T ObterPorId(Guid id);
        void Cadastrar(T entidade);
        void Alterar(T entidade);
        void Deletar(Guid id);
    }
}
