using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DekanatWebExample.Models
{
    public enum EducationForms { Ochnoe, Zaochnoe }

    public class Group
    {
        public int ID { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        [Range(1,6)]
        public int Year { get; set; }

        /// <summary>
        /// Форма обучения
        /// </summary>
        public EducationForms EducationForm { get; set; }

        /// <summary>
        /// Образовательная программа
        /// </summary>
        [StringLength(100, MinimumLength = 3)]
        public string EducationProgram { get; set; }

        /// <summary>
        /// Номер на потоке
        /// </summary>
        [Range(1,1000)]
        public int Number { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        // programm abbreviation-YearNumber(Form) Example: PIEE-31(Z)
        // TODO separate ed. program with abbreviation method
        public string Abbreviation => string.Join("", EducationProgram.Split(new [] { ' ', '-' })
                                                   .Select(w => w.Substring(0, 1))) +
                $"-{Year}{Number}({EducationForm.ToString()[0]})";

        public override string ToString()
        {
            return Abbreviation;
        }
    }
}