using BDSuggestion.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDSuggestion.Models
{
  public  class CalcularTotais : BaseNotifyPropertyChanged
    {
        private int GTotal_;
        public int GTotal
        {
            get { return GTotal_; }
            set { SetField(ref GTotal_, value); }
        }

        private int GAceitas_;
        public int GAceitas
        {
            get { return GAceitas_; }
            set { SetField(ref GAceitas_, value); }
        }
        private int GNegadas_;
        public int GNegadas
        {
            get { return GNegadas_; }
            set { SetField(ref GNegadas_, value); }
        }
    }
}
