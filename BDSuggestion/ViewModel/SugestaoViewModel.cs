using BDSuggestion.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BDSuggestion.ViewModel
{
    public class SugestaoViewModel : BaseNotifyPropertyChanged
    {
       
        private Departamentos Departamento_;
        public Departamentos Departamento
        {
            get { return Departamento_; }
            set { SetField(ref Departamento_, value); }
        }

        private Sugestoes Sugestoes_;
        public Sugestoes Sugestao
        {
            get { return Sugestoes_; }
            set { SetField(ref Sugestoes_, value); }
        }

    }
}
