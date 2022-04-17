using BDSuggestion.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSuggestion.Services
{
    public class DepartamentoDB
    {
        /// <summary>
        /// Método de Adicionar e Update de Departamentos
        /// </summary>
        /// <param name="Departamento"></param>
        /// <returns>Retorna um inteiro maior que zero se foi adicionado ou modificado alguma coisa</returns>
        public async ValueTask<int> AddUpdate(Departamentos Departamento)
        {
            if (Departamento.Id == 0)
            {
                await App.Entitie.Departamentos.AddAsync(Departamento);
            }
            else
            {
                App.Entitie.Departamentos.Update(Departamento);
            }

            int result = await App.Entitie.SaveChangesAsync();
            App.Entitie.ChangeTracker.Clear();
            return result;
        }

        /// <summary>
        /// Deleta o departamento
        /// </summary>
        /// <param name="id">ID do departamento que será deletado</param>
        /// <returns>Retorna um inteiro maior que zero se o departamento foi deletado com sucesso.</returns>
        public async ValueTask<int> Deletar(int id)
        {
            if (id > 0)
            {
                Departamentos sug = App.Entitie.Departamentos.FirstOrDefault(s => s.Id == id);
                if (sug != null)
                {
                    App.Entitie.Departamentos.Remove(sug);
                    return await App.Entitie.SaveChangesAsync();
                }
            }
            return -1;
        }

        /// <summary>
        /// Busca um departamento por Id
        /// </summary>
        /// <param name="id">ID do departamento que será buscado</param>
        /// <returns>Retorna o departamento ou um departamento padrão com Id = 0</returns>
        public async ValueTask<Departamentos> GetDepart(int id)
        {
            return await App.Entitie.Departamentos.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Busca um departamento por nome
        /// </summary>
        /// <param name="id">ID do departamento que será buscado</param>
        /// <returns>Retorna o departamento ou um departamento padrão com Id = 0</returns>
        public async ValueTask<Departamentos> GetDepart(string nome)
        {
            return await App.Entitie.Departamentos.AsNoTracking().FirstOrDefaultAsync(u => u.Nome.Equals(nome));
        }

        /// <summary>
        /// Lista todos os departamentos
        /// </summary>
        /// <returns>Retorna uma lista de todos os departamentos</returns>
        public async ValueTask<List<Departamentos>> ListarDepartamentos()
        {
            return await App.Entitie.Departamentos.AsNoTracking().ToListAsync();
        }



    }
}
