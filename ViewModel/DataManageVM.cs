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

        #region COMMANDS TO OPEN WINDOWS

        //команды для открытия окна Депортамента
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
