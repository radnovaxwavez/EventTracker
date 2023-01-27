using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace EventTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            MyEvents = GetEvents();
            this.BindingContext = this; //This is setting the binding context of the .cs file to itself so that the .xaml file can read everything in here, REMEMBER THIS PLS
        }

        public ObservableCollection<Event> MyEvents { get; set; }

        private ObservableCollection<Event> GetEvents()
        {
            return new ObservableCollection<Event>
            {
                new Event { Title = "Xamarin Forms Masterclass", Image = "banner.png", Venue = "Register Online", Duration = "07:30 UTC - 09:30 UTC", Date = new DateTime(2020, 6, 8), Description = "This masterclass was design to help you take your Xamarin Forms Development to the next level. Register here: https://bit.ly/2XbkoTG"},
                new Event { Title = "Training: WDC Solution", Image = "onlinetraining.jpg", Venue = "Zoom Meeting", Duration = "07:30 UTC - 09:30 UTC", Date = new DateTime(2020, 6, 9), Description = "Want to maximize your European vacation? Move through Europe with ease & discover how to travel around Europe by train with as little as possible."},
                new Event { Title = "World Dogs Championship", Image = "dogs.jpg", Venue = "Virtual Challenge", Duration = "07:30 UTC - 09:30 UTC", Date = new DateTime(2020, 6, 10), Description = "A dog earns a championship with wins at a specified number of conformation shows, where a judge evaluates a dog's breed type and how closely the dog approaches the ideal represented in its breed's standard."},
                new Event { Title = "Book Review Conference", Image = "bookclub.jpg", Venue = "Online", Duration = "07:30 UTC - 09:30 UTC", Date = new DateTime(2020, 6, 11), Description = "And whether you are a publishing insider or simply a book nerd, you should be able to find something to suit you in this list of events in 2020."},
                new Event { Title = "Tea Ceremony", Image = "tea.jpg", Venue = "Virtual Meetup", Duration = "07:30 UTC - 09:30 UTC", Date = new DateTime(2020, 6, 12), Description = "The tea ceremony sees the simple task of preparing a drink for a guest elevated to an art form, an intricate series of movements performed in strict order."}
            };
        }

        private async Task OpenAnimation(View view, uint length = 250) //pre-defined length
        {
            view.RotationX = -90; //Initial rotation set to -90 degs
            view.IsVisible = true;
            view.Opacity = 0; //Initial opacity is 0
            _ = view.FadeTo(1, length); //Will fade to 1 
            await view.RotateXTo(0, length); //Will be set back to 0 on animation start
        }

        private async Task CloseAnimation(View view, uint length = 250) //Closing animation, essentially reverting above effects
        {
            _ = view.FadeTo(0, length); 
            await view.RotateXTo(-90, length);
            view.IsVisible = false; //Setting the view visibility variable back to false
        }

        private async void MainExpander_Tapped(object sender, EventArgs e)
        {
            var expander = sender as Expander; //Variable sender is now of type 'Expander'
            var imgView = expander.FindByName<Grid>("ImageView");
            var detailsView = expander.FindByName<Grid>("DetailsView");

            if (expander.IsExpanded)
            {
                await OpenAnimation(imgView);
                await OpenAnimation(detailsView);
            }
            else
            {
                await CloseAnimation(detailsView);
                await CloseAnimation(imgView);
            }

        }
    }
    public class Event //Really need to figure out the exact way these datasheets work, was such a pain in the ass to figure out before
    {
        public string Title { get; set; }
        public string Venue { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
    }
}
