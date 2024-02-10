using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MeetingManager.Models;
using MeetingManager.ViewModel;

namespace MeetingManager.ViewModel
{
   public class EditMeetingView
    {
        static Model _model;
        public Meeting Meeting { get; set; }
        public User User { get; set; }

        public EditMeetingView(string UserName)
        {
            _model = Model.getInstance();
            Meeting = new Meeting();
            var _meeting = _model.GetMeeting();
            ViewModelUserPersonal.UserListMeetings(UserName);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Для редактирования встречи укажите ID ");
            Console.ResetColor();
            Meeting.ID = int.Parse(Console.ReadLine());

            Console.Write("Укажите время начала: ");
            Meeting.StartTime = DateTime.Parse(Console.ReadLine());

            if (DateTime.Now > Meeting.StartTime)
            {
                throw new Exception(string.Format("Дата начала должна не должна быть ранее {0}", DateTime.Now.ToString()));
            }

            Console.Write("Укажите время окончания: ");
            Meeting.EndTime = DateTime.Parse(Console.ReadLine());

            Console.Write("Время за которое нужно уведомить пользователя: ");
            Meeting.UserNotification = Int32.Parse(Console.ReadLine());
          

            if (_model.EditMeeting(Meeting))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(string.Format("Встреча с ID '{0}' успешно отредактирована", Meeting.ID));
                Console.ResetColor();

            }
            else
                throw new Exception("Во время редактирования встречи произошла ошибка");
        }
    }
}