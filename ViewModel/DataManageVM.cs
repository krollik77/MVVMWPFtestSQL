using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MVVMWPF.Model;
using MVVMWPF.View;

namespace MVVMWPF.ViewModel
{
    class DataManageVM : INotifyPropertyChanged
    {
        //все отделы
        private List<Department> allDepartments = DataWorker.GetAllDepartments();
        public List<Department> AllDepartments
        { get { return allDepartments; }
          set { allDepartments = value;
                NotifyPropertyChanged("AllDepartments");}
        }
        //все позиции
        private List<Position> allPositions = DataWorker.GetAllPositions();
        public List<Position> AllPositions
        {
            get
            {
                return allPositions;
            }
            private set
            {
                allPositions = value;
                NotifyPropertyChanged("AllPositions");
            }
        }
        //все сотрудники
        private List<User> allUsers = DataWorker.GetAllUsers();
        public List<User> AllUsers
        {
            get
            {
                return allUsers;
            }
            private set
            {
                allUsers = value;
                NotifyPropertyChanged("AllUsers");
            }
        }

        //свойства для отдела
        public static string DepartmentName { get; set; }
        //свойства для позиций
        public static string PositionName { get; set; }
        public static decimal PositionSalary { get; set; }
        public static int PositionMaxNumber { get; set; }
        public static Department PositionDepartment { get; set; }

        //свойства для сотрудника
        public static string UserName { get; set; }
        public static string UserSurName { get; set; }
        public static string UserPhone { get; set; }
        public static Position UserPosition { get; set; }

        //свойства для выделенных элементов
        public TabItem SelectedTabItem { get; set; }
        public static User SelectedUser { get; set; }
        public static Position SelectedPosition { get; set; }
        public static Department SelectedDepartment { get; set; }

        #region COMMANDS TO ADD

        //команды для ввода данных
        private RelayCommand addNewDepartment;
        public RelayCommand AddNewDepartment
        {
            get
            {
                return addNewDepartment ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateDepartment(DepartmentName);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        //команды для ввода данных
        private RelayCommand addNewPosition;
        public RelayCommand AddNewPosition
        {
            get
            {
                return addNewPosition ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (PositionName == null || PositionName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    if (PositionSalary == 0)
                    {
                        SetRedBlockControll(wnd, "SalaryBlock");
                    }
                    if (PositionMaxNumber == 0)
                    {
                        SetRedBlockControll(wnd, "MaxNumberBlock");
                    }
                    if (PositionDepartment == null)
                    {
                        MessageBox.Show("Укажите отдел");
                    }
                    else
                    {
                        resultStr = DataWorker.CreatePosition(PositionName, PositionSalary, PositionMaxNumber, PositionDepartment);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        //команды для ввода данных
        private RelayCommand addNewUser;
        public RelayCommand AddNewUser
        {
            get
            {
                return addNewUser ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (UserName == null || UserName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    if (UserSurName == null || UserSurName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "SurNameBlock");
                    }
                    //if (UserPhone == null || UserPhone.Replace(" ", "").Length == 0)
                    //{
                    //    SetRedBlockControll(wnd, "SurNameBlock");
                    //}
                    if (UserPosition == null)
                    {
                        MessageBox.Show("Укажите позицию");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateUser(UserName, UserSurName, UserPhone, UserPosition);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        #endregion

        #region COMMANDS TO OPEN WINDOWS

        //публичные команды для открытия окна при нажатии кнопки Депортамента //
        private RelayCommand openAddNewDepartmentWnd;
        public RelayCommand OpenAddNewDepartmentWnd
        {
            get
            {
                return openAddNewDepartmentWnd ?? new RelayCommand(obj =>
                {
                    OpenAddDepartmentWindowMethod();
                }
                    );
            }
        }
        //публичные команды для открытия окна при нажатии кнопки Розиции //
        private RelayCommand openAddNewPositionWnd;
        public RelayCommand OpenAddNewPositionWnd
        {
            get
            {
                return openAddNewPositionWnd ?? new RelayCommand(obj =>
                {
                    OpenAddPositionWindowMethod();
                }
                );
            }
        }
        //публичные команды для открытия окна при нажатии кнопки Юзера/Сотрудника
        //
        private RelayCommand openAddNewUserWnd;
        public RelayCommand OpenAddNewUserWnd
        {
            get
            {
                return openAddNewUserWnd ?? new RelayCommand(obj =>
                {
                    OpenAddUserWindowMethod();
                }
                );
            }
        }
        
        

#endregion


        #region METHODS TO OPEN WINDOW
        //методы открытия окон
        private void OpenAddDepartmentWindowMethod()
        {
            Window1 newDepartmentWindow = new Window1();  ///тут ошибка вместо Window1  должен быть AddDepartmentWindow
            SetCenterPositionAndOpen(newDepartmentWindow);
        }
        private void OpenAddPositionWindowMethod()
        {
            AddNewPositionWindow newPositionWindow = new AddNewPositionWindow();
            SetCenterPositionAndOpen(newPositionWindow);
        }
        private void OpenAddUserWindowMethod()
        {
            AddNewUserWindow newUserWindow = new AddNewUserWindow();
            SetCenterPositionAndOpen(newUserWindow);
        }
        ////окна редактирования
        //private void OpenEditDepartmentWindowMethod(Department department)
        //{
        //    EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow(department);
        //    SetCenterPositionAndOpen(editDepartmentWindow);
        //}
        //private void OpenEditPositionWindowMethod(Position position)
        //{
        //    EditPositionWindow editPositionWindow = new EditPositionWindow(position);
        //    SetCenterPositionAndOpen(editPositionWindow);
        //}
        //private void OpenEditUserWindowMethod(User user)
        //{
        //    EditUserWindow editUserWindow = new EditUserWindow(user);
        //    SetCenterPositionAndOpen(editUserWindow);
        //}
        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion

        #region UPDATE VIEWS
// обнуляет все значения 
        private void SetNullValuesToProperties()
        {
            //для пользователя
            UserName = null;
            UserSurName = null;
            UserPhone = null;
            UserPosition = null;
            //для позиции
            PositionName = null;
            PositionSalary = 0;
            PositionMaxNumber = 0;
            PositionDepartment = null;
            //для отдела
            DepartmentName = null;
        }

        private void UpdateAllDataView()
        {
            UpdateAllDepartmentsView();
            UpdateAllPositionsView();
            UpdateAllUsersView();
        }
        // методы которые обнавляют укран после ввода параметров
        private void UpdateAllDepartmentsView()
        {
            AllDepartments = DataWorker.GetAllDepartments();
            MainWindow.AllDepartmentsView.ItemsSource = null;
            MainWindow.AllDepartmentsView.Items.Clear();
            MainWindow.AllDepartmentsView.ItemsSource = AllDepartments;
            MainWindow.AllDepartmentsView.Items.Refresh();
        }
        private void UpdateAllPositionsView()
        {
            AllPositions = DataWorker.GetAllPositions();
            MainWindow.AllPositionsView.ItemsSource = null;
            MainWindow.AllPositionsView.Items.Clear();
            MainWindow.AllPositionsView.ItemsSource = AllPositions;
            MainWindow.AllPositionsView.Items.Refresh();
        }
        private void UpdateAllUsersView()
        {
            AllUsers = DataWorker.GetAllUsers();
            MainWindow.AllUsersView.ItemsSource = null;
            MainWindow.AllUsersView.Items.Clear();
            MainWindow.AllUsersView.ItemsSource = AllUsers;
            MainWindow.AllUsersView.Items.Refresh();
        }
        #endregion

        // этот метот меняет цвет блока на красный
        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        // этот метот запускает MessageView
        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        
           


    }


}
