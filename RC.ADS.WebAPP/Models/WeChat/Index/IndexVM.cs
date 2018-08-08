using RC.ADS.Data.Entity.AD_Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Models.WeChat
{
    public class IndexVM
    {
        /// <summary>
        /// 轮播图
        /// </summary>
        public List<Article> Slideshows { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public Article About { get; set; }
        /// <summary>
        /// 成果
        /// </summary>
        public Article Achievement { get; set; }
        /// <summary>
        /// 公告
        /// </summary>
        public Article Notice { get; set; }

    }
}
