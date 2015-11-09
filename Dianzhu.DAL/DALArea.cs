﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;

namespace Dianzhu.DAL
{
    public class DALArea : DALBase<Model.Area>
    {
        
        public DALArea()
        {
           
        }
        //注入依赖,供测试使用;
        public DALArea(string fortest) : base(fortest)
        {
            
        }
        //protected IDAL.IDALBase<Model.Area> IDALBase
        //{
        //    get { return IDALBase; }
        //    set { iDALBase = value; }
        //}
        
        public virtual IList<Model.Area> GetArea(int areaid)
        {
            string sqlstr = "select a from Area a where a.Code like '" + areaid + "__00'";
            IQuery query = Session.CreateQuery(sqlstr);

            return query.Future<Model.Area>().ToList<Model.Area>();
        }
        public Model.Area GetAreaByAreaid(int areaid)
        {
            IQuery query = Session.CreateQuery("select a from Area a where a.Id=" + areaid + "");
            return query.FutureValue<Model.Area>().Value;
        }

        public Model.Area GetAreaByAreaname(string areaname)
        {
            IQuery query = Session.CreateQuery("select a from Area a where a.Name='" + areaname + "'");
            return query.FutureValue<Model.Area>().Value;
        }

        public Model.Area GetAreaByAreanamelike(string areaname)
        {
            IQuery query = Session.CreateQuery("select a from Area a where a.Name like '%" + areaname + "%'");
            return query.FutureValue<Model.Area>().Value;
        }

        public Model.Area GetAreaBySeoName(string seoName)
        {
            //IQuery query = DALBase.Session.CreateQuery("select a from Area a where a.SeoName='" + seoName + "'");
            //return query.FutureValue<Model.Area>().Value;
            string sql = "select top 1 Id,Name,SeoName,Code,AreaOrder,MetaDescription from Area a where a.seoname=:seoname";
            IQuery query = Session.CreateSQLQuery(sql)
                .SetParameter("seoname", seoName);
            var result = query.UniqueResult<object[]>();
            if (result != null)
            {
                return new Model.Area(){
                    Id = int.Parse(result[0].ToString()),
                    Name = result[1].ToString(),
                    SeoName = result[2].ToString(),
                    Code = result[3].ToString(),
                    AreaOrder = int.Parse(result[4].ToString()),
                    MetaDescription = result[5].ToString()
                };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// todo:需要将逻辑部分移至bll层
        /// </summary>
        /// <param name="areacode"></param>
        /// <returns></returns>
        public IList<Model.Area> GetSubArea(string areacode)
        {
            string sql = "";
            //开始2位编号
            string bCode = areacode.Substring(0, 2);
            //中间2位编号
            string mCode = areacode.Substring(2, 2);
            //最后2位编号
            string lCode = areacode.Substring(4, 2);
            //查询编号
            string searchCode;
            if (mCode == "00")
            {
                //查找市级区域单位
                searchCode = bCode + "__00";
                sql = "select a from Area a where a.Code like '" + searchCode
                    + "' and a.Code<>'" + bCode + "0000'";
            }
            else if (lCode == "00")
            {
                //查找市内区、县级区域单位(并排除市和辖区)
                searchCode = bCode + mCode + "__";
                sql = "select a from Area a where a.Code like '" + searchCode
                    + "' AND a.Code<>'" + bCode + mCode
                    + "00'";
            }
            if (string.IsNullOrEmpty(sql)) return null;
            IQuery query = Session.CreateQuery(sql);
            return query.Future<Model.Area>().ToList<Model.Area>();
        }
        /// <summary>
        /// todo:需要移至bll层
        /// </summary>
        /// <param name="areacode"></param>
        /// <returns></returns>
        public string GetSubAreaIds(string areacode)
        {
            string ids = string.Empty;
            Model.Area currentArea = GetAreaByCode(areacode);
            if (currentArea == null)
            {
                return ids;
            }
            ids += currentArea.Id;
            IList<Model.Area> Areas = GetSubArea(areacode);
            if (Areas == null)
            {

                return ids;
            }
            ids += ",";
            foreach (Model.Area a in Areas)
            {
                ids += a.Id + ",";
            }
            ids = ids.TrimEnd(',');
            return ids;
        }

        public IList<Model.Area> GetAreaProvince()
        {
            string strQuery = "select a from Area a where a.Code like '__0000'";
            IQuery query = Session.CreateQuery(strQuery);
            return query.Future<Model.Area>().ToList<Model.Area>();
        }


        public Model.Area GetAreaByCode(string code)
        {
            string sql = "select a from Area a where a.Code='" + code + "'";
            IQuery query = Session.CreateQuery(sql);
            return query.FutureValue<Model.Area>().Value;
        }
        public Model.Area GetOne(int code)
        {
            return  GetOne(code);
        }
    }
}
