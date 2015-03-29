using System;
using System.Collections.Generic;
using System.Text;

namespace Spanish_Conjugations.Model
{
    public class TenseDatabase : ObservableObject
    {
        #region Members
        string[] _tenseDisp = { "Present", "Preterite", "Subjunctive", "Conditional", "Nosotros Commands", "Regular Commands" };
        string _select;
        #endregion

        #region Properties
        public string this[int index]
        {
            get { return _tenseDisp[index]; }
        }

        public string[] getTenses
        {
            get { return _tenseDisp; }
        }
        public string SelectedTense
        {
            get { return _select; }
            set
            {
                if (_select != value)
                {
                    _select = value;
                    RaisePropertyChanged("SelectedTense");
                }
            }
        }
        #endregion
    }
 }
