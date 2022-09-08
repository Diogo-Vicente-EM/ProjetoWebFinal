using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;


namespace AlunoWeb.Models
{
    public abstract class Repositorio<T> where T : IEntidade
    {
        public virtual void AdicionaAluno(T objeto)
        {
        }
        public virtual void RemoveAluno(T objeto)
        {
        }
        public virtual void AlterarAluno(T objeto)
        {
        }
        public virtual IEnumerable<T> Buscar(Expression<Func<T, bool>> predicate)
        {
            return Enumerable.Empty<T>();
        }
        public virtual IEnumerable<T> BuscarTodosAlunos()
        {
            return Enumerable.Empty<T>();
        }
    }
}
