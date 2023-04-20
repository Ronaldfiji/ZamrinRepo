using CoffeeNext.Models;
using CoffeeNext.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace CoffeeNext.ViewModels
{
    
    public class ProjectsViewModel : BaseViewModel
    {
        public Projects projectsModel { get; } = new Projects();
        public ObservableRangeCollection<Projects> Projects { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public  Command  LoadItemsCommand { get; }
        //public IProjectService projectService => DependencyService.Get<IProjectService>();
        IProjectService _projectService;

        public AsyncCommand<object> SelectedCommand { get; }
        public Command LoadMoreCommand { get; }

        public AsyncCommand<Projects> DeleteCommand { get; }
        public Command LoadDataTestCommand { get; }
        public ProjectsViewModel()
        {
            Title = "Internet Coffee";
            Projects = new ObservableRangeCollection<Projects>();
            RefreshCommand = new AsyncCommand(Refresh);
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);
            _projectService = DependencyService.Get<IProjectService>();
            SelectedCommand = new AsyncCommand<object>(Selected);
            LoadMoreCommand = new Command(LoadMore);
            DeleteCommand = new AsyncCommand<Projects>(Remove);

            LoadDataTestCommand = new Command(async() => await LoadDataTest());


            //Task.Run(async () => await Refresh());
        }


        
        async Task Refresh()
        {
            try
            {
                IsBusy = true;

                //await Task.Delay(1000);

                Projects.Clear();

                var projects = await _projectService.GetProjectsAsync("api/ProjectsManger/GetAllProjects");

                Projects.AddRange(projects);
               
                IsBusy = false;

                foreach( var project in Projects)
                {
                    Debug.WriteLine("Projetss...................."+ project.ProjectName);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public  async  Task LoadDataTest()
        {

            try
            {
                IsBusy = true;

                await Task.Delay(1000);

                Projects.Clear();

                var projects = await _projectService.GetProjectsAsync("api/ProjectsManger/GetAllProjects");

                Projects.AddRange(projects);                

                IsBusy = false;

                foreach (var project in Projects)
                {
                    Debug.WriteLine("Projetss...................." + project.ProjectName);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }
      
        private async void ExecuteLoadItemsCommand(object obj)
        {
            IsBusy = true;

            try
            {
                Projects.Clear();

                /*var task1 = await projectService.GetProjectsAsync("/ProjectsManger/GetAllProjects");
                var task2 = await projectService.GetProjectsAsync("/ProjectsManger/GetAllProjects");
                var task3 = await projectService.GetProjectsAsync("/ProjectsManger/GetAllProjects");
                var task4 = await projectService.GetProjectsAsync("/ProjectsManger/GetAllProjects");

                foreach (var person in task2)
                {
                    Debug.WriteLine("The person is : " + person);
                    Projects.Add(person);
                }*/
                var proj = await _projectService.GetProjectsAsync("api/ProjectsManger/GetAllProjects");
                Projects.AddRange(proj);
                

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }


        public void OnAppearing()
        {
            IsBusy = true;
           

        }


        Projects previouslySelected;
        Projects selectedProject;
        public Projects SelectedProject
        {
            get => selectedProject;
            set => SetProperty(ref selectedProject, value);
        }

        async Task Selected(object args)
        {
            var project = args as Projects;
            if (project == null)
                return;

            SelectedProject = null;


            //await AppShell.Current.GoToAsync(nameof(AddMyCoffeePage));
            await Application.Current.MainPage.DisplayAlert("Selected", project.projectName, "OK");

        }

        void LoadMore()
        {
            if (Projects.Count >= 8)
                return;

           // var image = "coffeebag.png";
            Projects.Add(new Projects { ProjectName = "Rosket Project", Description = "New projeect Missile M100", Code = "10100" });
            /*Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Potent Potable", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Potent Potable", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Kenya Kiambu Handege", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Kenya Kiambu Handege", Image = image });*/

        }

        async Task Remove(Projects projects)
        {            
            await _projectService.DeleteProject($"api/ProjectsManger/{projects.Id}");
            await Refresh();
        }

    }
    }
