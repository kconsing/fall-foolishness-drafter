using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fall_foolishness_drafter.Models
{
    public class player : INotifyPropertyChanged
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }

        public string Team { get; set; }
        public bool Paid { get; set; }


        public player(string firstname = "first", string lastname = "last")
        {
            firstName = firstname;
            lastName = lastname;
            fullName = firstName + " " + lastName;
        }

        public override string ToString()
        {
            return fullName;
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
