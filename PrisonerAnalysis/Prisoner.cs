using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonerAnalysis
{
    public class Prisoner
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DOB { get; set; }
        public string? Race { get; set; }
        public string? Sex { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public static Prisoner FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Prisoner prisoner = new Prisoner();
            if (!values[0].StartsWith("Total") && !values[0].StartsWith("Printed"))
            {              
                prisoner.Id = values[0];
                prisoner.Name = values[1] + values[2];
                prisoner.DOB = Convert.ToDateTime(values[3]);
                prisoner.Race = values[4];
                prisoner.Sex = values[5];
                prisoner.BookingDate = Convert.ToDateTime(values[6]);
                DateTime tempDate;
                if (DateTime.TryParse(values[7], out tempDate))
                    prisoner.ReleaseDate = tempDate;               
            }
            return prisoner;

        }

        public bool IsReleased
        {
            get { return ReleaseDate.HasValue; }             
        }

        public int DayInPrison
        {
            get {
                if (!BookingDate.HasValue)
                {
                    return 0; 
                }
                if (ReleaseDate.HasValue)
                {
                    TimeSpan diff = ReleaseDate.Value - BookingDate.Value;
                    return (int)Math.Floor(diff.TotalDays);
                }
                else
                {
                    TimeSpan diff = DateTime.Now - BookingDate.Value;
                    return (int)Math.Floor(diff.TotalDays);
                }    
            }
        }      
    }
}
