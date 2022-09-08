using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlunoWeb.Models
{
    public class Aluno : IEntidade
    {
        [Display(Name = "Matricula*")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Contém Caracteres Não Permetidos")]
        [Range(1, 999999999, ErrorMessage = "Tamanho Permitido é de 1 até 999999999")]
        public int Matricula { get; set; }

        [Display(Name = "Nome*")]
        [StringLength(99)]
        [RegularExpression(@"^[a-zA-Z'\s]*$", ErrorMessage = "Contém Caracteres Não Permetidos")]
        public string AlunoNome { get; set; }

        public EnumeradorSexo Sexo { get; set; }

        [Display(Name = "Data de Nascimento*")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        [Display(Name = "CPF (Opcional)")]
        [StringLength(11)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Contém Caracteres Não Permetidos")]
        public string? CPF { get; set; }
        public Aluno(int numMatricula, string alunoNome, EnumeradorSexo Sexo, DateTime dataNascimento)
        {
            Matricula = numMatricula;
            AlunoNome = alunoNome;
            DataNascimento = dataNascimento;
            this.Sexo = Sexo;
        }
        public override bool Equals(object? obj)
        {
            return obj is Aluno aluno &&
                   Matricula == aluno.Matricula &&
                   AlunoNome == aluno.AlunoNome &&
                   CPF == aluno.CPF;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Matricula, AlunoNome, CPF);
        }
        public override string? ToString()
        {
            return base.ToString();
        }
    }

    public interface IEntidade
    {
    }

}
