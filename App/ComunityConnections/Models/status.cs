using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ComunityConnections.Models
{
    public partial class status : ObservableObject
    {
        [ObservableProperty]
        public string statusName;
    }
}
