using Catalogo.Dominio.Base;
using Catalogo.Dominio.BibliotecaJogos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.Dominio.Bibliotecas
{
    public class Biblioteca: EntityBase
    {
        public Guid UsuarioId { get; private set; }
        public ICollection<BibliotecaJogo> BibliotecaJogos { get; private set; }

        protected Biblioteca()
        {
            BibliotecaJogos = new List<BibliotecaJogo>();
        }

        public Biblioteca(Guid usuarioId)
        {
            UsuarioId = usuarioId;
            BibliotecaJogos = new List<BibliotecaJogo>();
        }


        public void AdicionarJogo(Guid jogoId)
        {
            if (BibliotecaJogos.Any(x => x.JogoId == jogoId))
                return;

            BibliotecaJogos.Add(new BibliotecaJogo
            {
                BibliotecaId = Id,
                JogoId = jogoId,
                DataAquisicao = DateTime.UtcNow
            });
        }
    }
}


