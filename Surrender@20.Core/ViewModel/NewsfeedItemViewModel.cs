using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Surrender_20.Core.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedItemViewModel
    {
        public string Title { get; set; } = "Weź coś zrób";
    }
}
