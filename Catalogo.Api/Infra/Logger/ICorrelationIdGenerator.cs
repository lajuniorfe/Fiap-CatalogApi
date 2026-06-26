using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.Infra.Logger
{
    public interface ICorrelationIdGenerator
    {
        string Get();
        void Set(string correlationId);
    }
}
