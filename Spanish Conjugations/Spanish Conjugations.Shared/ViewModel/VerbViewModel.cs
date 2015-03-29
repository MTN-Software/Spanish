using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Spanish_Conjugations;
using Spanish_Conjugations.Model;
using Windows.UI.Popups;
using System.Windows.Input;

namespace Spanish_Conjugations.ViewModel
{
    class VerbViewModel : ObservableObject
    {
        #region Construction
        /// <summary>
        /// Constructs the default instance of the viewModel
        /// </summary>
        public VerbViewModel()
        {
            _verb = new Verb
            {
                VerbInfinitive = "Verb",
                VerbSubject = "Enter",
                VerbInput = "Enter Verb",
                VerbEnding = "unknown",
                VerbFinalForm = "unkown"
            };


            _sTenses = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                _sTenses.Add(_database[i]);
            }
        }
        #endregion

        #region Members
        Verb _verb;
        List<string> _sTenses;
        private TenseDatabase _database = new TenseDatabase();
        ObservableCollection<TenseViewModel> _tenses = new ObservableCollection<TenseViewModel>();
        TenseViewModel _currentTense;
        string _selectedTense;
        #endregion

        #region Properties
        public Verb Verb
        {
            get { return _verb; }
            set { _verb = value; }
        }

        public string VerbSubject
        {
            get { return Verb.VerbSubject; }
            set
            {
                // This should actually find the subject
                try
                {
                    Verb.VerbSubject = Verb.VerbInput.Substring(0, Verb.VerbInput.IndexOf(' '));
                    RaisePropertyChanged("VerbSubject");
                }
                catch (Exception ex)
                {
                    //Verb.VerbSubject = ex.Message;
                    RaisePropertyChanged("VerbSubject");
                }
            }
        }

        public string VerbInfinitive
        {
            get { return Verb.VerbInfinitive; }
            set
            {
                // This should find the infinitive
                try
                {
                    int indexOfSpace = Verb.VerbInput.IndexOf(' ');
                    Verb.VerbInfinitive = VerbInput.Substring(indexOfSpace + 1, Verb.VerbInput.Length - (indexOfSpace + 1));
                    RaisePropertyChanged("VerbInfinitive");
                }
                catch (Exception ex)
                {
                    //Verb.VerbInfinitive = ex.Message;
                    RaisePropertyChanged("VerbInfinitive");
                }
            }
        }
        public string VerbEnding
        {
            get { return Verb.VerbEnding; }
            set
            {
                try
                {
                    Verb.VerbEnding = CalcVerbEnding(Verb);
                    RaisePropertyChanged("VerbEnding");
                }
                catch (Exception ex)
                {
                    RaisePropertyChanged("VerbEnding");
                }
            }
        }


        public string VerbFinalForm
        {
            get { return Verb.VerbFinalForm; }
            set
            {
                try
                {
                    // Conjugation Logic
                    string root = Verb.VerbInfinitive.Substring(0, Verb.VerbInfinitive.Length - 2);
                    string helping = string.Empty;
                    string append = string.Empty;
                    string carryOutVerb = string.Empty;
                    string replaceInf = string.Empty;
                    switch (SelectTense)
                    {
                        case "Present":
                            msgShow();
                            //append = PresentConj(Verb.VerbSubject.ToLower());
                            break;
                        case "Preterite":
                            msgShow();
                            //replaceInf = PasseConj(root).Item1;
                            //helping = PasseConj(root).Item2;
                            break;
                        case "Subjunctive":
                            msgShow();
                            //replaceInf = ImperfectConj(Verb.VerbEnding.ToLower());
                            break;
                        case "Conditional":
                            msgShow();
                            //helping = ProcheConj(append);
                            //root = Verb.VerbInfinitive;
                            //append = string.Empty;
                            break;
                        case "Nosotros Commands":
                            msgShow();
                            //append = FuturSimpleConj(append);
                            //carryOutVerb = string.Concat(Verb.VerbSubject, " ", Verb.VerbInfinitive, append);
                            break;
                        case "Regular Commands":
                            msgShow();
                            //append = ConditionalConj(append);
                            //carryOutVerb = string.Concat(Verb.VerbSubject, " ", Verb.VerbInfinitive, append);
                            break;
                        default:
                            msgShow();
                            //append = PresentConj(append);
                            break;
                    }

                    if (carryOutVerb == string.Empty)       // sets carryOutVerb's value if not already set
                    {
                        if (helping == string.Empty)        // if there isn't any helping verb
                        {
                            if (replaceInf == string.Empty)
                            {
                                carryOutVerb = string.Concat(Verb.VerbSubject, " ", root, append);
                            }
                            else
                            {
                                carryOutVerb = string.Concat(Verb.VerbSubject, " ", replaceInf);
                            }
                        }
                        else                                // if there is a helping verb
                        {
                            if (replaceInf == string.Empty)
                            {
                                carryOutVerb = string.Concat(Verb.VerbSubject, " ", helping, root, append);
                            }
                            else
                            {
                                carryOutVerb = string.Concat(Verb.VerbSubject, " ", helping, " ", replaceInf);
                            }
                        }
                    }
                    
                    Verb.VerbFinalForm = carryOutVerb;
                    RaisePropertyChanged("VerbFinalForm");
                }
                catch (Exception ex)
                {
                    msgBox(ex.Message, "Well, this isn't good");
                    RaisePropertyChanged("VerbFinalForm");
                }
            }
        }


        public string VerbInput
        {
            get { return Verb.VerbInput; }
            set
            {
                if (Verb.VerbInput != value)
                {
                    Verb.VerbInput = value;
                    RaisePropertyChanged("VerbInput");
                    //QuickConjugate(); // Obsolete
                }
            }
        }

        public ObservableCollection<TenseViewModel> Tenses
        {
            get { return _tenses; }
            set { _tenses = value; }
        }

        public List<string> VerbTenses
        {
            get { return _sTenses; }
        }
        public TenseViewModel SelectedTense
        {
            get { return _currentTense; }
            set
            {
                if (_currentTense != value)
                {
                    _currentTense = value;
                    RaisePropertyChanged("SelectedTense");
                }
            }
        }

        public string SelectTense
        {
            get { return _selectedTense; }
            set
            {
                if (_selectedTense != value)
                {
                    _selectedTense = value;
                    RaisePropertyChanged("SelectTense");
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Calculates the verb ending (i.e. er, re, ir)
        /// </summary>
        /// <param name="Verb">Verb to calculate the ending of</param>
        /// <returns>The last two letters of the Verb</returns>
        public string CalcVerbEnding(Verb Verb)
        {
            int length = Verb.VerbInput.Length;
            return Verb.VerbInput.Substring(length - 2, 2);
        }

        /// <summary>
        /// If a feature is not yet fully implemented, use this as a band-aid to cover
        /// </summary>
        /// <remarks>
        /// Use as sparingly as possible
        /// </remarks>
        private async void msgShow()
        {
            MessageDialog msg = new MessageDialog("Sorry, that operation is not supported yet!", "Yeah, about that...");
            await msg.ShowAsync();
        }

        /// <summary>
        /// Custom Message box
        /// </summary>
        /// <param name="message">The message you want to display</param>
        /// <param name="title">The title of the Message box</param>
        private async void msgBox(string message, string title)
        {
            MessageDialog msg = new MessageDialog(message, title);
            await msg.ShowAsync();
        }
        #endregion

        #region Commands
        /// <summary>
        /// Updates the Verb Subject
        /// </summary>
        void UpdateVerbSubjectExecute()
        {
            VerbSubject = string.Format("{0}", _verb.VerbSubject);
        }
        public ICommand UpdateVerbSubject
        {
            get
            {
                return new RelayCommand(UpdateVerbSubjectExecute, new Func<bool>(() => { return true; }));
            }
        }

        
        // This might not work
        void UpdateVerbInfinitiveExecute()
        {
            VerbInfinitive = string.Format("{0}", _verb.VerbInfinitive);
        }
        public ICommand UpdateVerbInfinitive
        {
            get
            {
                return new RelayCommand(UpdateVerbInfinitiveExecute, new Func<bool>(() => { return true; }));
            }
        }


        void UpdateVerbInputExecute()
        {
            VerbInput = string.Format("{0}", _verb.VerbInput);
        }
        public ICommand UpdateVerbInput
        {
            get
            {
                return new RelayCommand(UpdateVerbInputExecute, new Func<bool>(() => { return true; }));
            }
        }


        void UpdateVerbEndingExecute()
        {
            VerbEnding = string.Format("{0}", _verb.VerbEnding);
        }
        public ICommand UpdateVerbEnding
        {
            get
            {
                return new RelayCommand(UpdateVerbEndingExecute, new Func<bool>(() => { return true; }));
            }
        }


        void UpdateVerbFinalFormExecute()
        {
            VerbFinalForm = string.Format("{0}", _verb.VerbFinalForm);
        }
        public ICommand UpdateVerbFinalForm
        {
            get
            {
                return new RelayCommand(UpdateVerbFinalFormExecute, new Func<bool>(() => { return true; }));
            }
        }
        void UpdateSelectedTenseExecute()
        {
            SelectedTense = _currentTense;
        }

        #endregion
    }
}
