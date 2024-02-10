using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Models
{
    /// <summary>
    /// Meeting - описывает сущность врстречи
    /// </summary>
    public class Meeting //
    {
        public int ID { get; set; } //ID первичный ключ (Primary Key)
        public int UserID { get; set; } //ID пользователя в системе (Foreign Key)
        public DateTime StartTime { get; set; } // Время начала 
        public DateTime EndTime { get; set; } //Дата окончания
        public int UserNotification { get; set; }//Время за, за которое нужно уведомить пользователя
        
    }
}
