using LojaGeek.Model.DB.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Model.DB.Repository
{
    public class ComentarioRepository : RepositoryBase<Comentario>
    {
        public ComentarioRepository(ISession session) : base(session) { }
    }
}
