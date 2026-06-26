using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.Infra.Logger
{
    public class CorrelationIdGenerator : ICorrelationIdGenerator
    {
        private static string _correlationId;

        public string Get() => _correlationId;

        public void Set(string correlationId) => _correlationId = correlationId;
    }
}
