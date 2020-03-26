using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace Classword.ViewModel
{
    class MainViewModel
    {
        public MainViewModel()
        {
            this.Name = "What are we going to do tonight, Brain?";

            string filePath = Directory.GetCurrentDirectory();
            filePath = Directory.GetParent(filePath).Parent.Parent.Parent.FullName;
            filePath += "\\tasks.csv";

            string[] input;

            try
            {
                input = File.ReadAllLines(filePath);
                this.Names = new ObservableCollection<string>(input);
            }
            catch (IOException e)
            {
                this.Names = new ObservableCollection<string>();
                this.Name = "There was an error loading the tasks file";
                this.Names.Add("Go outside and touch the ground.");
                
                string bugReportPath = Directory.GetCurrentDirectory();
                bugReportPath = Directory.GetParent(bugReportPath).Parent.Parent.Parent.FullName;
                bugReportPath += "\\bugreport.log";

                List<string> bugreport = new List<string>();
                bugreport.Add("---Start of Bugreport---");
                bugreport.Add("\nError:");
                bugreport.Add(e.ToString());
                bugreport.Add("\nMessage:");
                bugreport.Add(e.Message);
                bugreport.Add("\n---End of Bugreport---");
                bugreport.Add("");

                File.AppendAllLines(bugReportPath, bugreport);
            }

        }

        public string Name { get; set; }

        public ObservableCollection<string> Names { get; set; }


        private RelayCommand _commandAddTask;
        public ICommand AddElementCommand
        {
            get
            {
                if (_commandAddTask == null)
                {
                    _commandAddTask = new RelayCommand( () => AddElement() );
                }
                return _commandAddTask;
            }
        }

        public void AddElement()
        {
            this.Names.Add(this.Name);
        }


        private RelayCommand _commandSaveTasks;
        public ICommand SaveTasksCommand
        {
            get
            {
                if (_commandSaveTasks == null)
                {
                    _commandSaveTasks = new RelayCommand(() => SaveTasks());
                }
                return _commandSaveTasks;
            }
        }
        
        public void SaveTasks()
        {
            string filePath = Directory.GetCurrentDirectory();
            filePath = Directory.GetParent(filePath).Parent.Parent.Parent.FullName;
            filePath += "\\tasks.csv";

            File.WriteAllLines(filePath, Names);

        }

    }
}
