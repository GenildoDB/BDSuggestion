using BDSuggestion.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSuggestion.Services
{
    public class SugestaoDB
    {
        /// <summary>
        /// Adiciona ou modificada uma sugestão
        /// </summary>
        /// <param name="Sugestao"></param>
        /// <returns>Retorna um inteiro maior que zero se foi adicionado ou alterado alguma coisa.</returns>
        public async Task<int> AddUpdate(Sugestoes Sugestao)
        {
            if (Sugestao.Id == 0)
            {
                await App.Entitie.Sugestoes.AddAsync(Sugestao);
            }
            else
            {
                //Altera o nome do colaborador caso ele altere o nome dele em alguma sugestão
                if (!Sugestao.Colaborador.Equals(App.Colaborador))
                {
                    var colabs = await App.Entitie.Sugestoes.Where(p => p.Colaborador.Equals(App.Colaborador)).ToListAsync();
                    foreach (var colab in colabs)
                    {
                        colab.Colaborador = Sugestao.Colaborador;
                        if(colab.Id == Sugestao.Id)
                        {
                            colab.Sugestao = Sugestao.Sugestao;
                            colab.Justificativa = Sugestao.Justificativa;
                            colab.Departamento = Sugestao.Departamento;
                        }
                    }
                }
                else
                {
                    var colab = await App.Entitie.Sugestoes.FirstOrDefaultAsync(p => p.Id == Sugestao.Id);
                    colab.Sugestao = Sugestao.Sugestao;
                    colab.Justificativa = Sugestao.Justificativa;
                    colab.Departamento = Sugestao.Departamento;
                }

                //App.Entitie.Sugestoes.Update(Sugestao);
            }

            int result = 0;
            try
            {
                result = await App.Entitie.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", string.Format("{0}\n{1}", ex.Message, ex.StackTrace), "OK");
            }

            App.Entitie.ChangeTracker.Clear();

            return result;
        }

        /// <summary>
        /// Deleta uma sugestão por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna um inteiro maior que zero se foi removido a sugestão</returns>
        public async Task<int> Delete(int id)
        {
            if (id > 0)
            {
                try
                {
                    Sugestoes sug = App.Entitie.Sugestoes.FirstOrDefault(s => s.Id == id);
                    if (sug != null)
                    {
                        App.Entitie.Sugestoes.Remove(sug);
                        return await App.Entitie.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Erro", string.Format("{0}\n{1}", ex.Message, ex.StackTrace), "OK");
                }
            }
            return -1;
        }

        /// <summary>
        /// Busca uma sugestão por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna uma sugestão</returns>
        public async Task<Sugestoes> GetSugestao(int id)
        {
            return await App.Entitie.Sugestoes.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Lista todas as sugestões
        /// </summary>
        /// <returns>Retorna uma lista de sugestões</returns>
        public async Task<List<Sugestoes>> ListarSugestoes()
        {
            return await App.Entitie.Sugestoes.AsNoTracking().ToListAsync();
        }
        /// <summary>
        /// Lista todas as sugestões do colaborador
        /// </summary>
        /// <param name="Colaborador"></param>
        /// <returns>Retorna uma lista com todas as sugestões do colabador</returns>
        public async Task<List<Sugestoes>> ListarSugestoes(string Colaborador)
        {
            return await App.Entitie.Sugestoes.AsNoTracking().Where(p => p.Colaborador.Equals(Colaborador)).ToListAsync();
        }
    }
}
