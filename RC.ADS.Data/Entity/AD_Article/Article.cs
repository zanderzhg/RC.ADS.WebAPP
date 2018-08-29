using RC.ADS.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Article
{
    public class Article
    {
        public Article() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public string ArticleName { get; set; }
        public string ArticleContent { get; set; }
        public string ArticleIco { get; set; }
        public string ArticleImage { get; set; }
        public int ArticleIndex { get; set; }
        public ArticleTypeEnum ArticleType{ get; set; }
    }
}
