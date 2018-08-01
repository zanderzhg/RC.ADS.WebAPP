using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Models.Articles
{
    public class ArticlesDTO
    {
        
        public string Id { get; set; }
        public string ArticleName { get; set; }
        public string ArticleContent { get; set; }
        public string ArticleIco { get; set; }
        public string ArticleImage { get; set; }
        public int ArticleIndex { get; set; }
        public string ArticleTypeId   { get; set; }
        public string ArticleTypeName { get; set; }
    }
}
