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
            this.Name = "TextBlock ili choto togo";
            this.Names = new ObservableCollection<string>();
            Names.Add(Name);
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
            string folder = Directory.GetCurrentDirectory();
            folder = Directory.GetParent(folder).Parent.Parent.Parent.FullName;
            string filename = "\\tasks.csv";

            File.WriteAllLines(folder+filename, Names);

        }

    }
}
