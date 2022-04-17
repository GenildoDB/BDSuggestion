using BDSuggestion.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDSuggestion.ViewModel
{
   public class DepartamentoViewModel : BaseNotifyPropertyChanged
    {
        private Departamentos Departamento_;
        public Departamentos Departamento
        {
            get { return Departamento_; }
            set { SetField(ref Departamento_, value); }
        }
    }
}
