using Catalogo.Dominio.Base.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.Dominio.Bibliotecas.Repository
{
    public interface IBibliotecaRepository : IBaseRepository<Biblioteca>
    {
        Biblioteca ObterPorUsuario(Guid idUsuario);

    }
}
