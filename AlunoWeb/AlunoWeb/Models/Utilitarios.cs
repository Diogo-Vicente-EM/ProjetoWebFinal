using AlunoWeb.Controllers;



namespace AlunoWeb.Models
{
    public abstract class Utilitarios
    {
        public static bool ValidaCPF(string cpf)
        {
            string valor = cpf.Replace(".", "");

            valor = valor.Replace("-", "");
            valor = valor.Replace(" ", "");

            if (valor.Length != 11)

                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)

                if (valor[i] != valor[0])

                    igual = false;

            if (igual || valor == "12345678909")

                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                try
                {
                    numeros[i] = int.Parse(
                      valor[i].ToString());
                }
                catch (FormatException)
                {

                    return false;
                }
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];
            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)

            {
                if (numeros[10] != 0)

                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;

        }
        public static string FormatCPF(string CPF)
        {
            CPF = CPF.Replace(".", "").Replace("-", "").Replace(" ", "").Replace("/", "");

            return Convert.ToUInt64(CPF).ToString(@"000\.000\.000\-00");
        }

        public static bool ValidarAlunoAlterar(Aluno aluno, RepositorioAluno repositorio, int id)
        {
            var regrasException = new RegrasException<Aluno>();

            try
            {
                DateTime dataAtual = DateTime.Now;
                if (aluno.DataNascimento.Year < 1850)
                {

                    return false;
                }
                if (aluno.DataNascimento > dataAtual)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            var alunoCpf = repositorio.BuscarTodosAlunos().Where(a => a.CPF.Replace("-", "").Replace(".", "") == aluno.CPF).Any();
            if (alunoCpf && aluno.Matricula != id)
            {
                regrasException.AdicionarErroPara(x => x.CPF, "Já existe esse numero de CPF !");
                throw regrasException;
            }
            if (aluno.CPF == null)
            {
                aluno.CPF = "";
            }
            else
            {
                foreach (Aluno alu in repositorio.BuscarTodosAlunos())
                {
                    if (alu.CPF.Replace("-", "").Replace(".", "") == aluno.CPF && alu.Matricula != id)
                    {
                        regrasException.AdicionarErroPara(x => x.CPF, "Já existe esse numero de CPF !");
                        throw regrasException;
                    }
                }
                aluno.CPF = (CPF)aluno.CPF;
                if (aluno.CPF == "Invalido")
                {
                    regrasException.AdicionarErroPara(x => x.CPF, "CPF Invalido !");
                    throw regrasException;
                }
                else
                {
                    aluno.CPF = FormatCPF(aluno.CPF);
                }
            }
            
            return true;
        }

        public static bool ValidarAluno(Aluno aluno, RepositorioAluno repositorio)
        {
            var regrasException = new RegrasException<Aluno>();

            var alunos = repositorio.BuscarTodosAlunos().Where(a => a.Matricula == aluno.Matricula).Any();
            if (alunos == true)
            {
                regrasException.AdicionarErroPara(x => x.Matricula, "Ja existe esse numero de matricula");
                throw regrasException;
            }

            DateTime dataAtual = DateTime.Now;
            if (aluno.DataNascimento.Year < 1850)
            {
                regrasException.AdicionarErroPara(x => x.DataNascimento, "Data deve ser maior que 1850");
                throw regrasException;
            }
            if (aluno.DataNascimento > dataAtual)
            {
                regrasException.AdicionarErroPara(x => x.DataNascimento, "Data não pode ser maior que data atual");
                throw regrasException;
            }

            if (aluno.CPF != null)
            {
                var alunos1 = repositorio.BuscarTodosAlunos().Where(a => a.CPF.Replace("-", "").Replace(".", "") == aluno.CPF).Any();
                aluno.CPF = (CPF)aluno.CPF;
                if (aluno.CPF != "Invalido")
                {

                    if (alunos1 == true)
                    {
                        regrasException.AdicionarErroPara(x => x.CPF, "Já existe esse numero de CPF !");
                        throw regrasException;
                    }
                    else
                    {
                        aluno.CPF = FormatCPF(aluno.CPF);
                    }
                }
                else
                {
                    regrasException.AdicionarErroPara(x => x.CPF, "CPF Invalido !");
                    throw regrasException;
                }
            }
            else
            {
                aluno.CPF = "";
            }
            return true;
        }

    }
}


