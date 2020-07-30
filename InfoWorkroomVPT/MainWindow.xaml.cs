using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace InfoWorkroomVPT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Workroom> ListRoom = new List<Workroom>();
        public int counter = 0;
        private DispatcherTimer timer = null;

        #region Не трогать
        /*Как это работает не знаю, но он скрывает кнопку закрытия окна*/
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        /*Конец непонятного кода - но лучше не трогать*/

        private void Window_StateChanged(object sender, EventArgs e)
        {
            WindowState = WindowState.Maximized;
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            StartTimer();
            InitializeList();
        }

        void InitializeList()
        {
            AddToList("МАСТЕРСКАЯ: ИНФОРМАЦИОННЫЕ КАБЕЛЬНЫЕ СЕТИ 1", "vptLogo.png", "Мастерская позволит обучающимся овладеть знаниями по созданию инфраструктуры практически для всех видов телекоммуникационных сетей, пониманием требований стандартов отрасли и конкретными навыками, которые обучающийся будет получать на практике.");
            AddToList("МАСТЕРСКАЯ: ПРОГРАММНЫЕ РЕШЕНИЯ ДЛЯ БИЗНЕСА", "vptLogo.png", "Целью мастерской является разработка, модифицирование и документирование информационных систем. Обучающиеся получат знания, которые позволят найти работу в любых компаниях в качестве разработчиков программного обеспечения.");
            AddToList("МАСТЕРСКАЯ: СЕТЕВОЕ И СИСТЕМНОЕ АДМИНИСТРИРОВАНИЕ", "vptLogo.png", "Cетевое и системное администрирование подразумевает под собой обеспечение штатной работы персональных компьютеров, офисных серверов, сетевых коммуникаций. На практике обучающиеся получат знания по проектированию локальных сетей, администрированию локальных вычислительных сетей.");
            AddToList("МАСТЕРСКАЯ: РАЗРАБОТКА КОМПЬЮТЕРНЫХ ИГР И МУЛЬТИМЕДИЙНЫХ ПРИЛОЖЕНИЙ", "vptLogo.png", "Процесс создания компьютерных программ, предназначенных для обучения и развлечения пользователей. В процессе прохождения практики в мастерской обучающиеся получат знания по анализу задачи, разработке игровых объектов и анимаций, построению игровых уровней и интерфейса пользователя, отладке и тестированию проекта.");
            AddToList("МАСТЕРСКАЯ: РАЗРАБОТКА ВИРТУАЛЬНОЙ И ДОПОЛНЕННОЙ РЕАЛЬНОСТИ", "vptLogo.png", "Разработка виртуальной и дополненной реальности – данные технологии представляют собой сложные технологические разработки, состоящие применяются во многих сферах, таких как развлечения, медицина, строительство и торговля.");
        }

        void AddToList(string nameWorkroomL, string sourceImageL, string descriptionL)
        {
            Workroom wr = new Workroom();
            wr.NameWorkroom = nameWorkroomL;
            wr.SourceImage = sourceImageL;
            wr.Description = descriptionL;

            ListRoom.Add(wr);
        }

        void StartTimer()
        {
            timer = new DispatcherTimer();  // если надо, то в скобках указываем приоритет, например DispatcherPriority.Render
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 10);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            counter++;
            if (ListRoom.Count == counter) counter = 0;

            nameWorkroom.Text = ListRoom[counter].NameWorkroom;
            screenWorkroom.Source = new BitmapImage(new Uri("pack://application:,,,/Image/" + ListRoom[counter].SourceImage));
            infoWorkroom.Text = ListRoom[counter].Description;
        }
    }

    public class Workroom
    {
        public string NameWorkroom;
        public string SourceImage;
        public string Description;
    }
}
