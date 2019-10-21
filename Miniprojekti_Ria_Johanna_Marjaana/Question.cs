using System;
using System.Collections.Generic;
using System.Text;

namespace Quizz
{
    class Question
    {
        private string kysymys;
        public string Kysymys { get; set; }

        private string vastaus;
        public string Vastaus { get; set; }

        private string playerVastaus;

        public string PlayerVastaus
        {
            get { return playerVastaus; }
            set { playerVastaus = value; }
        }

        private string selite;

        public string Selite
        {
            get { return selite; }
            set { selite = value; }
        }



        public Question(string kysymys, string vastaus)
        {
            Kysymys = kysymys;
            Vastaus = vastaus;
        }

        public Question(string kysymys, string vastaus, string selite)
        {
            Kysymys = kysymys;
            Vastaus = vastaus;
            Selite = selite;
        }


        public Question()
        {

        }

        public Question(Question kysymys1, string vastaus)
        {
            this.vastaus = vastaus;
        }
    }
}
