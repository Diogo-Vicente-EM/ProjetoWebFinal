using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;


namespace AlunoWeb.Models
{
    public class RepositorioAluno : Repositorio<Aluno>
    {
        private string strConn = @"DataSource=localhost;Port=3054; Database=C:\BancoDeDados-Diogo\DBESTAGIO.FDB; username = sysdba; password = masterkey";
        public FbConnection conn;

        public override void AdicionaAluno(Aluno aluno)
        {
            conn = new FbConnection(strConn);
            conn.Open();
            string sqlIncluir = $"INSERT INTO TBALUNO (ALUMATRICULA, ALUNOME,ALUSEXO,ALUNASCIMENTO,ALUNOCPF) VALUES(" +
               $"{aluno.Matricula},'{aluno.AlunoNome}',{(int)aluno.Sexo},'{aluno.DataNascimento.ToString("dd.MM.yyyy")}','{aluno.CPF}');";
            FbCommand cmd = new FbCommand(sqlIncluir, conn);
            cmd.ExecuteNonQuery();
           
            conn.Close();
        }
        public override void RemoveAluno(Aluno aluno)
        {
            conn = new FbConnection(strConn);
            conn.Open();
            string mSQL = $"DELETE from TBALUNO Where  ALUMATRICULA =  {aluno.Matricula};";
            FbCommand cmd = new FbCommand(mSQL, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public override void AlterarAluno(Aluno aluno)
        {
            conn = new FbConnection(strConn);
            conn.Open();

            string sql = $"update TBALUNO  set " +
                $"ALUMATRICULA = {aluno.Matricula}," +
                $"ALUNOME = '{aluno.AlunoNome}'," +
                $"ALUSEXO = {(int)aluno.Sexo}," +
                $"ALUNASCIMENTO = '{aluno.DataNascimento.ToString("dd.MM.yyyy")}'," +
                $"ALUNOCPF = '{aluno.CPF}'" +
                $"WHERE ALUMATRICULA = {aluno.Matricula};";
            FbCommand cmd = new FbCommand(sql, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }
        public override IEnumerable<Aluno> BuscarTodosAlunos()
        {
            conn = new FbConnection(strConn);
            conn.Open();
            string mSQL = $"Select * from TBALUNO ;";
            FbCommand cmd = new FbCommand(mSQL, conn);
            cmd.ExecuteNonQuery();
            FbDataReader dr = cmd.ExecuteReader();

            List<Aluno> alunos = new List<Aluno>();
            while (dr.Read())
            {              
                Aluno aluno = new(Convert.ToInt32(dr[0]), dr[1].ToString(), (EnumeradorSexo)dr[2], Convert.ToDateTime(dr[3]));
                aluno.CPF = dr[4].ToString();
                
                alunos.Add(aluno);
            }
            alunos = alunos.OrderBy(a => a.Matricula).ToList();
            dr.Close();
            conn.Close();
            return alunos;
        }
        public override IEnumerable<Aluno> Buscar(Expression<Func<Aluno, bool>> predicate)
        {
            //List<Aluno> Alunos = (List<Aluno>)BuscarTodosAlunos().Where( predicate.Compile());
            return BuscarTodosAlunos().Where(predicate.Compile()); 
        }
        public Aluno BucarPorId(int id)
        {
            return Buscar(a => a.Matricula == id).First();
        }
        public IEnumerable<Aluno> ConsultarContendoNome(string parteNome)
        {
            var consulta = BuscarTodosAlunos().Where(a => a.AlunoNome.Contains(parteNome)).ToList();
            return consulta;
        }
        public IEnumerable<Aluno> ConsultarContendoMatricula(int parteMatricula)
        {
            var consulta = BuscarTodosAlunos().Where(a => a.Matricula.ToString().Contains(parteMatricula.ToString())).ToList();
            return consulta;
        }
    }
}
