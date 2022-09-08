using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlunoWeb.Models
{
    public struct CPF
    {
        public string? cpf;
        private CPF(string valorInformado)
        {
            cpf = Utilitarios.ValidaCPF(valorInformado) ? valorInformado : "Invalido";
        }
        public static implicit operator string(CPF cpf) => cpf.cpf;
        public static explicit operator CPF(string valor) => new(valor);
    }
}
