﻿using PropertyChanged;
using System;

namespace Surrender_20.Model
{
    [AddINotifyPropertyChangedInterface]
    public class Newsfeed
    {
        public Uri UrlToNewsfeed { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; } //Overlaps with Content?
        public string ThumbnailURL { get; set; }
        public string Image { get; set; }
    }
}
