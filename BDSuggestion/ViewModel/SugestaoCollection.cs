using BDSuggestion.Models;
using BDSuggestion.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSuggestion.ViewModel
{
    public class SugestaoCollection : BaseNotifyPropertyChanged
    {
        private ObservableCollection<SugestaoViewModel> ListaSugestao_;
        public ObservableCollection<SugestaoViewModel> ListaSugestao
        {
            get { return ListaSugestao_; }
            set { SetField(ref ListaSugestao_, value); }
        }

        private CalcularTotais Totais_;
        public CalcularTotais Totais
        {
            get { return Totais_; }
            set { SetField(ref Totais_, value); }
        }
        private ObservableCollection<Departamentos> ListaDepart_;
        public ObservableCollection<Departamentos> ListaDepart
        {
            get { return ListaDepart_; }
            set { SetField(ref ListaDepart_, value); }
        }

        private readonly bool SomenteColab;
        public SugestaoCollection(bool SomenteColab = false, bool CarregarSugsInicio = true)
        {
            this.SomenteColab = SomenteColab;
            ListaSugestao = new ObservableCollection<SugestaoViewModel>();
            ListaDepart = new ObservableCollection<Departamentos>();
            Totais = new CalcularTotais();

            if (CarregarSugsInicio)
                CarregarSugestaoGlobal();
        }

        public async void CarregarSugestaoGlobal()
        {
            // Task.Run(async () =>
            //{

            //});
            ListaSugestao.Clear();
            ListaDepart.Clear();
            Totais.GTotal = 0;


            /* uma lista de sugestões pré definidas. 
             * O que pode ser trocado por uma lista vinda de uma API
             * para exibir sugestões de outros usuários.
             */
            List<SugestaoViewModel> listSugs = new List<SugestaoViewModel>
               {
                   new SugestaoViewModel()
                   {
                       Sugestao = new Sugestoes()
                       {
                           Colaborador = "Davi Ferreira",
                           Sugestao = "Dividir por setores a ordem de pagamento por data, para facilitar e ajudar na folha de pagamento dos funcionários e prestadores de serviços da empresa.",
                           Justificativa = "Horário de deposito do pagamento muito tarde.",
                           Id = 9999,
                           Status = 1,
                           Departamento = "Financeiro"
                       },
                       Departamento = new Models.Departamentos()
                       {
                           Id = 1,
                           Nome = "Financeiro"
                       }

                   },
                   new SugestaoViewModel()
                   {
                       Sugestao = new Sugestoes()
                       {
                           Colaborador = "Maria dos Santos",
                           Sugestao = "Organizar melhor os materiais que entram e saem da empresa, alguns ficam perdidos dentro do almoxarifado e fica dificil achar.",
                           Justificativa = "Almoxarifado muito desorganizado!",
                           Id = 9999,
                           Status = 1,
                           Departamento = "Almaxarifado"
                       },
                       Departamento = new Models.Departamentos()
                       {
                           Id = 2,
                           Nome = "Almoxarifado"
                       }

                   },
                   new SugestaoViewModel()
                   {
                       Sugestao = new Sugestoes()
                       {
                           Colaborador = "Bruna Oliveira",
                           Sugestao = "Pagar adiantado os prestadores de serviços para quando precisar deles eles não se recusarem a fazer o serviço, pois alguns estão atrasando a nossa demanda.",
                           Justificativa = "Atraso na demanda.",
                           Id = 9999,
                           Status = 2,
                           Departamento = "Financeiro"
                       },
                       Departamento = new Models.Departamentos()
                       {
                           Id = 1,
                           Nome = "Financeiro"
                       }

                   },
                   new SugestaoViewModel()
                   {
                       Sugestao = new Sugestoes()
                       {
                           Colaborador = "Gabriel Alves",
                           Sugestao = "Colocar os materiais pequenos em potes de vidro grande para ser fácil de achar e pegar.",
                           Justificativa = "Nenhum",
                           Id = 9999,
                           Status = 0,
                           Departamento = "Almaxarifado"
                       },
                       Departamento = new Models.Departamentos()
                       {
                           Id = 2,
                           Nome = "Almoxarifado"
                       }
                   }
               };

            SugestaoDB dB = new SugestaoDB();
            DepartamentoDB db2 = new DepartamentoDB();

            try
            {
                List<Sugestoes> sugs = null;

                //Escolhe entre carregar sugestões de todos ou somente do colaborador atual.
                if (SomenteColab)
                {
                    listSugs.Clear();
                    sugs = await dB.ListarSugestoes(App.Colaborador);
                }
                else
                    sugs = await dB.ListarSugestoes();

                var depart = await db2.ListarDepartamentos();

                //Carrega a lista de derpartamentos que será exibida no filtro do controle Picker
                ListaDepart.Add(new Departamentos() { Nome = "Ver todos" });
                foreach (var d in depart)
                    ListaDepart.Add(d);

                Departamentos dep = null;
                foreach (var su in sugs)
                {
                    dep = depart.FirstOrDefault(p => p.Nome.Equals(su.Departamento));
                    listSugs.Add(new SugestaoViewModel() { Sugestao = su, Departamento = dep });
                }

                //Inverte a ordem para exibir as sugestões que forem adicionadas recentemente para serem exibidas primeiro
                listSugs.Reverse();

                foreach (var s in listSugs)
                    ListaSugestao.Add(s);

                Calcular();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", string.Format("{0}\n{1}", ex.Message, ex.StackTrace), "OK");
            }
        }

        /// <summary>
        /// Calcula o total de sugestões, sugestões aceitas e negadas
        /// </summary>
        private void Calcular()
        {
            Totais.GTotal = ListaSugestao.Count;
            Totais.GAceitas = ListaSugestao.Count(p => p.Sugestao.Status == 1);
            Totais.GNegadas = ListaSugestao.Count(p => p.Sugestao.Status == 2);
        }

    }
}
