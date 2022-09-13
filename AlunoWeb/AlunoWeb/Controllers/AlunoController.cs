using AlunoWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlunoWeb.Controllers
{
    public class AlunoController : Controller
    {
        RepositorioAluno repositorio = new();
        public IActionResult Index(string ConteudoPesquisa)
        {
            var alumoVM = new AlunoViewModel
            {
                Alunos = repositorio.BuscarTodosAlunos().ToList()
            };

            if (!string.IsNullOrEmpty(ConteudoPesquisa)) {
                //string matriculaPesquisa = ConteudoPesquisa;
                List<Aluno> alunos;
                try
                {
                    int inteiro = Convert.ToInt32(ConteudoPesquisa);
                    alunos = repositorio.ConsultarContendoMatricula(inteiro).ToList();
                }
                catch (Exception)
                {
                    alunos = repositorio.ConsultarContendoNome(ConteudoPesquisa).ToList();
                }
                alumoVM.Alunos = alunos;
                return View(alumoVM);
            }
  
            return View(alumoVM);
        }
        public IActionResult Adicionar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(int matricula, string AlunoNome, EnumeradorSexo sexo, DateTime dataNascimento, string? cpf)
        {
           Aluno aluno = new(matricula, AlunoNome, sexo, dataNascimento);
            aluno.CPF = cpf;
            try
            {
                if (cpf == null)
                {
                    if (Utilitarios.ValidarAluno(aluno, repositorio))
                    {
                        repositorio.AdicionaAluno(aluno);
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    if (Utilitarios.ValidarAluno(aluno, repositorio))
                    {
                        aluno.CPF = Utilitarios.FormatCPF(cpf);
                        repositorio.AdicionaAluno(aluno);
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (RegrasException ex)
            {
                ex.CopiarErrosPara(ModelState);
            }
            return View(aluno);
        }
        public IActionResult Excluir(int id)
        {
            var aluno = repositorio.BucarPorId(id);
            return View(aluno);
        }
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public IActionResult ExcluirConfirmar(int id)
        {
            var aluno = repositorio.BucarPorId(id);
            repositorio.RemoveAluno(aluno);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Editar(int id)
        {
            var aluno = repositorio.BucarPorId(id);
            aluno.CPF = aluno.CPF.Replace(".", "").Replace("-", "");
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int matricula, string AlunoNome, EnumeradorSexo sexo, DateTime dataNascimento, string? cpf)
        {

            Aluno aluno = new(matricula, AlunoNome, sexo, dataNascimento);
            aluno.CPF = cpf;
            try
            {
                if (Utilitarios.ValidarAlunoAlterar(aluno, repositorio, matricula))
                {
                    repositorio.AlterarAluno(aluno);
                    return RedirectToAction(nameof(Index));
                }

            }catch (RegrasException ex)
            {
                ex.CopiarErrosPara(ModelState);
            }
            return View(aluno);
        }

    }
}
