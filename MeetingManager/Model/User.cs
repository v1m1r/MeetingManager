using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Models
{
    public class User //Пользователь
    {
        public int ID { get; set; }
        public string Name { get; set; } //Имя
        public string Surname { get; set; }//Фамилия
        public string JobTitile { get; set; }//Должность
    }
}
