using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingManager.Models;

namespace MeetingManager
{
    
    class Program
    {
      static void Main(string[] args)
        {
            new ViewModelBase();  //Инициализация первоначального списка пользователей и встреч в памяти

            int userInput = 0;
            do
            {
                userInput = DisplayMenu();
                if (userInput == 1) { Clear(); ViewModelBase.ViewAllMeeting();}
                if (userInput == 2) { Clear(); ViewModelBase.AddUSer(); }
                if (userInput == 3) { Clear(); ViewModelBase.ViewAllUsers(); }
                if (userInput == 4) { Clear(); ViewModelBase.LoginUserAccount(); }
                if (userInput == 5) { Environment.Exit(0); }
            } while (userInput != 6);
           
            Console.ReadLine();
        }
        static public int DisplayMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Программа для организации встреч. Для просмотра введите один из доступных пунктов меню");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("1.  Просмотр всего расписания");
            Console.WriteLine("____");
            Console.WriteLine("2.  Добавить нового пользователя");
            Console.WriteLine("____");
            Console.WriteLine("3.  Просмотр всех пользователей");
            Console.WriteLine("____");
            Console.WriteLine("4.  Войти в систему");
            Console.WriteLine("____");
            Console.WriteLine("5.  Выйти из программы");
            var result = Console.ReadLine();
            int MenuItem=0;
            try
            {
                MenuItem = Convert.ToInt32(result);
            }
            catch(System.FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка!!! Для выбора пункта меню используйте цифры!");
                Console.ResetColor();
            }
            return MenuItem;
        }

        static void Clear()
        { Console.Clear(); }
    }
}
