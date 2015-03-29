﻿using System;
using System.Collections.Generic;
using System.Text;
using Spanish_Conjugations;
using Spanish_Conjugations.Model;

namespace Spanish_Conjugations.ViewModel
{
    /// <summary>
    /// This is pretty useless, but somehow the program breaks without it.
    /// Anyways, it's the ViewModel to the TenseDatabase class
    /// </summary>
    public class TenseViewModel
    {
        #region Construction
        public TenseViewModel()
        {
            _tense = new TenseDatabase();
        }
        #endregion

        #region Members
        public TenseDatabase _database = new TenseDatabase();
        //ObservableCollection<VerbViewModel> _verbs = new ObservableCollection<VerbViewModel>();
        //int _index = 0;
        TenseDatabase _tense;
        #endregion

        #region Properties
        public string[] Tense
        {
            get { return _tense.getTenses; }
            //set { _database = value; }
        }

        public TenseDatabase Tenses
        {
            get { return _tense; }
            set { _tense = value; }
        }
        #endregion
    }
}
