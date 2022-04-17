using System;
using System.Collections.Generic;
using System.Text;

namespace BDSuggestion.Models
{
    public class Sugestoes
    {
        public int Id { get; set; }
        public string Colaborador { get; set; }
        public string Departamento { get; set; } //SEM LIGAÇÃO DIRETA POR ID DO DEPARTAMENTO, POIS "QUEBRARIA" TUDO, CASO O DEPARTAMENTO FOSSE EXCLUÍDO DO BANCO DE DADOS.
        public string Sugestao { get; set; }
        public string Justificativa { get; set; }
        public int Status { get; set; }
    }
}
