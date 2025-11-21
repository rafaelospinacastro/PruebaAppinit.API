using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAppinit.Domain.Entities
{
    public class Round
    {
        public int Id { get; set; }                 
        public Guid GameId { get; set; }            

        public string P1Move { get; set; } = "";    
        public string P2Move { get; set; } = "";

        public string Outcome { get; set; } = "";   
        public int RoundNumber { get; set; }        

        public Game Game { get; set; }
    }
}
