using System;
using System.Collections.Generic;
using System.Text;

namespace Spanish_Conjugations.Model
{
    /// <summary>
    /// Model of the verb
    /// </summary>
    public class Verb
    {
        #region Members
        string _infinitive;
        string _subject;
        string _input;      // combination of infinitive and subject
        string _ending;
        string _conjugateOut;
        #endregion

        #region Properties
        /// <summary>
        /// The verb infinitive
        /// </summary>
        public string VerbInfinitive
        {
            get { return _infinitive; }
            set { _infinitive = value; }
        }

        /// <summary>
        /// The verb subject
        /// </summary>
        public string VerbSubject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        /// <summary>
        /// The verb input
        /// </summary>
        public string VerbInput
        {
            get { return _input; }
            set { _input = value; }
        }

        /// <summary>
        /// The verb ending
        /// </summary>
        public string VerbEnding
        {
            get { return _ending; }
            set { _ending = value; }
        }

        /// <summary>
        /// The conjugated form of the verb
        /// </summary>
        public string VerbFinalForm
        {
            get { return _conjugateOut; }
            set { _conjugateOut = value; }
        }
        #endregion
    }
}
