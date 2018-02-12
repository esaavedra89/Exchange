

namespace Exchange.Infrastructure
{
    using ViewModels;
     public class InstanceLocator
    {
        //propiedad tipo MainViewModel
        //public MainViewModel Main { get; set; }
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            //instanciamos propiedad Main dentro del constructor
            //Main = new MainViewModel();
            Main = new MainViewModel();

        }
    }
}
