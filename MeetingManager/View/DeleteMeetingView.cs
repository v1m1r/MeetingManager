using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingManager.Models;
using MeetingManager.ViewModel;
using MeetingManager.View;

namespace MeetingManager.View
{
    public class DeleteMeetingView
    {
        static Model _model;
        public Meeting Meeting { get; set; }
        public User User { get; set; }

        public DeleteMeetingView(string UserName)
        {
            _model = Model.getInstance();
            Meeting = new Meeting();
            var _meeting = _model.GetMeeting();
            ViewModelUserPersonal.UserListMeetings(UserName);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Для удаления встречи укажите ID ");
            Console.ResetColor();
            Meeting.ID = int.Parse(Console.ReadLine());

            if (_model.DeleteMeeting(Meeting))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(string.Format("Встреча с ID '{0}' Успешно удалена", Meeting.ID));
                Console.ResetColor();
               
            }
            else
                throw new Exception("Во время удаления встречи произошла ошибка");
        }

    }
}
