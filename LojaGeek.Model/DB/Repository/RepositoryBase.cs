using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Repository
{
    public class RepositoryBase<T> where T : class
    {
        protected ISession Session { get; set; }

        public RepositoryBase(ISession session)
        {
            Session = session;
        }

        public void Delete(T entity)
        {
            try
            {
                Session.Clear();

                var transaction = Session.BeginTransaction();

                Session.Delete(entity);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir", ex);
            }
        }

        public T SaveOrUpdate(T entity)
        {
            try
            {
                Session.Clear();

                var transaction = Session.BeginTransaction();

                Session.SaveOrUpdate(entity);

                transaction.Commit();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir ou atualizar", ex);
            }
        }

        public IList<T> FindAll()
        {
            try
            {
                return Session.CreateCriteria(typeof(T)).List<T>();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar todos", ex);
            }
        }

        public T FindById(Guid id)
        {
            try
            {
                return Session.Get<T>(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar pelo id", ex);
            }
        }
    }
}
