using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaDiZhuTiZhengSuan
{
    class DataCenter
    {
       public double ChangBanZhou;
       public double BianLvDaoShu;
       public List<Point> Points = new List<Point>();

    }
    class Point
    {
       public string startName;
       public double latitude;
       public double longitude;
       public string endName;
       public double angleA1;
       public double lengthS;

        public Point(string startname,double w,double j,string endname,double angle,double length)
        {
            startName = startname;
            latitude = w;
            longitude = j;
            endName = endname;
            angleA1 = angle;
            lengthS = length;
        }
    }
}
