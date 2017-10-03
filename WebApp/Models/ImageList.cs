using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ImageList
    {
        public IEnumerable<Image> Imageslist { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}