using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using MeetingManager.Models;


namespace MeetingManager.ViewModel
{
    public class SaveFileMeetingViewModel
    {
        static Model _model;
        public static ObservableCollection<MeetingViewModel> Meeting { get; set; }
        public SaveFileMeetingViewModel(string UserName)
        {
            _model = Model.getInstance();
            var _user = _model.GetUsers();
            var _meeting = _model.GetMeeting();
            Meeting = new ObservableCollection<MeetingViewModel>(_meeting.Select(x => new MeetingViewModel { Meeting = x, User = _user.FirstOrDefault(y => y.ID == x.UserID) }).Where(z => z.User.Name == UserName));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Введите день за который нужно сделать экспорт:");
            Console.ResetColor();
            string Get_Date = Console.ReadLine();
            if (_model.SaveToXMLMeeting(Meeting,Get_Date))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Эксопрт встреч для пользователя '{0}' выполнен успешно.",UserName);
                Console.ResetColor();
            }
        }
    }
}