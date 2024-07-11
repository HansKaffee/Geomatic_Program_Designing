using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaDiZhuTiZhengSuan
{
    class Algorithm
    {
        //求扁率
        public static double CalBianLv(double blds)
        {
            double result;
            result = 1 / blds;
            return result;
        }


        //计算椭球基本参数
        #region 计算椭球基本参数
        //椭球短半轴b
        public static double Calb(double a, double f)
        {
            double b;
            b = a * (1 - f);
            return b;
        }
        //椭球第一偏心率平方
        public static double Cale2(double a, double b)
        {
            double e2;
            e2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2);
            return e2;
        }
        //椭球第二偏心率平方
        public static double Calep2(double e2)
        {
            double ep2;
            ep2 = e2 / (1 - e2);
            return ep2;
        }
        #endregion

        //白塞尔法大地主题正算
        public static string Caldadi(double B1, double L1, double A1, double S,double e2,double ep2,double a,double b)
        {
            string report1; //大地正算报告文本部分
            //计算起点的归化纬度
            //dd.mmsss转换dd转rad
            B1 = (Math.Floor(B1) + (Math.Floor(B1 * 100) / 100 - Math.Floor(B1)) * 100 / 60 + (B1 - Math.Floor(B1) - (Math.Floor(B1 * 100) / 100 - Math.Floor(B1))) * 10000 / 3600) * Math.PI / 180;
            L1 = (Math.Floor(L1) + (Math.Floor(L1 * 100) / 100 - Math.Floor(L1)) * 100 / 60 + (L1 - Math.Floor(L1) - (Math.Floor(L1 * 100) / 100 - Math.Floor(L1))) * 10000 / 3600) *Math.PI / 180;
            A1 = (Math.Floor(A1) + (Math.Floor(A1 * 100) / 100 - Math.Floor(A1)) * 100 / 60 + (A1 - Math.Floor(A1) - (Math.Floor(A1 * 100) / 100 - Math.Floor(A1))) * 10000 / 3600) *Math.PI / 180;



            double W1;
            double sinu1;
            double cosu1;
            double sinB1 = Math.Sin(B1);

            W1 = Math.Sqrt(1 - e2* Math.Pow(sinB1, 2));
            sinu1 = sinB1 * Math.Sqrt(1 - e2) / W1;
            cosu1 = Math.Cos(B1) / W1;

            //计算辅助函数值
            double sinA0 = cosu1 * Math.Sin(A1);
            double cotsigma1 = (cosu1 * Math.Cos(A1)) / sinu1;
            double sigma1 = Math.Atan(1 / cotsigma1);

            //辅助计算
            double cos2A0;
            double k2;
            cos2A0 = 1 - Math.Pow(sinA0, 2);
            k2 = ep2 * cos2A0;  //6式

            double A;
            double B;
            double C;
            double k = Math.Sqrt(k2);

            A = (1 - (k2 / 4) + (7 * Math.Pow(k2, 2) / 64) - (15 * Math.Pow(k, 6) / 256)) / b;
            B = ((k2 / 4) - (Math.Pow(k2, 2) / 8) + (37 * Math.Pow(k, 6) / 512));
            C = ((Math.Pow(k2, 2) / 128) - (Math.Pow(k, 6) / 128));

            double alpha;
            double beta;
            double gamma;

            double e = Math.Sqrt(e2);
            double e4 = Math.Pow(e2, 2);
            double e6 = Math.Pow(e, 6);


            alpha = ((e2 / 2) + (Math.Pow(e2, 2) / 8) + (Math.Pow(e, 6) / 16)) - (Math.Pow(e2, 2) / 16 + Math.Pow(e, 6) / 16) * cos2A0 + (3 * Math.Pow(e, 6) / 128) * Math.Pow(cos2A0, 2);
            beta = (e4 / 16 + e6 / 16) * cos2A0 - (e6 / 32) * Math.Pow(cos2A0, 2);
            gamma = (e6 / 256) * Math.Pow(cos2A0, 2);

            //计算球面长度
            double sigma = A * S;

            double sigmaTemp1 = sigma;
            double sigmaTemp2 = sigma;

            sigmaTemp2 = A * S + B * Math.Sin(sigma) * Math.Cos(2 * sigma1 + sigma) + C * Math.Sin(2 * sigma) * Math.Cos(4 * sigma1 + 2 * sigma);
            while (sigmaTemp1 - sigmaTemp2 >= 1e-10)    //迭代计算
            {
                double temp = sigmaTemp2;
                sigmaTemp2 = A * S + B * Math.Sin(sigmaTemp2) * Math.Cos(2 * sigma1 + sigmaTemp2) + C * Math.Sin(2 * sigmaTemp2) * Math.Cos(4 * sigma1 + 2 * sigmaTemp2);
                sigmaTemp1 = temp;
            }
            sigma = sigmaTemp2; //计算结束得出最终值    

            //计算经度差改正数
            
            double gai;
            gai = (alpha * sigma + beta * Math.Sin(sigma) * Math.Cos(2 * sigma1 + sigma) + gamma * Math.Sin(2 * sigma) * Math.Cos(4 * sigma1 + 2 * sigma)) * sinA0;



            //计算终点大地坐标及坐标方位角
            double sinu2;
            double B2;
            double lambda;

            sinu2 = sinu1 * Math.Cos(sigma) + cosu1 * Math.Cos(A1) * Math.Sin(sigma);
            B2 = Math.Atan(sinu2 / Math.Sqrt(1 - e2) * Math.Sqrt(1 - Math.Pow(sinu2, 2)));
            lambda = Math.Atan(Math.Sin(A1) * Math.Sin(sigma) / (cosu1 * Math.Cos(sigma)) - sinu1 * Math.Sin(sigma) * Math.Cos(A1));

            double sinA1 = Math.Sin(A1);
            double tanlambda = Math.Tan(lambda);
        

            if (sinA1 > 0 && tanlambda > 0) //计算lambda
            {
                lambda = Math.Abs(lambda);
            }
            else if (sinA1 > 0 && tanlambda < 0)
            {
                lambda = Math.PI - Math.Abs(lambda);

            }
            else if (sinA1 < 0 && tanlambda < 0)
            {
                lambda = -Math.Abs(lambda);
            }
            else if (sinA1 < 0 && tanlambda > 0)
            {
                lambda = Math.Abs(lambda) - Math.PI;
            }

            double L2;
            double A2;
            L2 = L1 + lambda - gai;
            A2 = Math.Atan((cosu1 * sinA1) / cosu1 * Math.Cos(sigma) * Math.Cos(A1) - sinu1 * Math.Sin(sigma));

            double tanA2 = Math.Tan(A2);

            if (sinA1 < 0 && tanA2 > 0) //计算A2
            {
                A2 = Math.Abs(A2);
            }
            else if (sinA1 < 0 && tanA2 < 0)
            {
                A2 = Math.PI - Math.Abs(A2);
            }
            else if (sinA1 > 0 && tanA2 > 0)
            {
                A2 = Math.PI + Math.Abs(A2);
            }
            else if (sinA1 > 0 && tanA2 < 0)
            {
                A2 = Math.PI * 2 - Math.Abs(A2);
            }

            if (A2 < 0)
            {
                A2 = A2 + Math.PI * 2;
            }
            if (A2 > Math.PI * 2)
            {
                A2 = A2 - Math.PI * 2;
            }

            double tempB2;
            double tempL2;
            double tempA2;
            //弧度转DD
            tempB2 = B2 * 180 / Math.PI;
            tempL2 = L2 * 180 / Math.PI;
            tempA2 = A2 * 180 / Math.PI;

            double DegreeB2;
            double DegreeL2;
            double DegreeA2;
            double MinB2;
            double MinL2;
            double MinA2;
            double SecB2;
            double SecL2;
            double SecA2;
            //DD转DD.MMSSSS
            DegreeB2 = Math.Floor(tempB2);
            MinB2 = Math.Floor((tempB2 - DegreeB2) * 60);
            SecB2 = Math.Round(((tempB2 - DegreeB2) * 60 - MinB2) * 60,2);

            DegreeL2 = Math.Floor(tempL2);
            MinL2 = Math.Floor((tempL2 - DegreeL2) * 60);
            SecL2 = Math.Round(((tempL2 - DegreeL2) * 60 - MinL2) * 60,2);

            DegreeA2 = Math.Floor(tempA2);
            MinA2 = Math.Floor((tempA2 - DegreeA2) * 60);
            SecA2 = Math.Round(((tempA2 - DegreeA2) * 60 - MinA2) * 60,2);

            double rB2;
            double rL2;
            double rA2;

            rB2 = DegreeB2 + MinB2 / 100 + SecB2 / 1000;
            rL2 = DegreeL2 + MinL2 / 100 + SecL2 / 1000;
            rA2 = DegreeA2 + MinA2 / 100 + SecA2 / 1000;


            report1 = "7,第三条大地线的W1," + Math.Round(W1,3) +"\n";
            report1 += "8,第三条大地线的sinu1," + Math.Round(sinu1, 3) + "\n";
            report1 += "9,第三条大地线的cosu1," + Math.Round(cosu1, 3) + "\n";
            report1 += "10,第三条大地线sinA0," + Math.Round(sinA0, 6) + "\n";
            report1 += "11,第三条大地线cotσ1," + Math.Round(cotsigma1, 6) + "\n";
            report1 += "12,第三条大地线σ1," + Math.Round(sigma1, 6) + "\n";
            report1 += "13,第三条大地线系数A," + Math.Round(A, 8) + "\n";
            report1 += "14,第三条大地线系数B," + Math.Round(B, 8) + "\n";
            report1 += "15,第三条大地线系数C," + Math.Round(C, 8) + "\n";
            report1 += "16,第三条大地线系数α," + Math.Round(alpha, 8) + "\n";
            report1 += "17,第三条大地线系数β," + Math.Round(beta, 8) + "\n";
            report1 += "18,第三条大地线系数γ," + Math.Round(gamma, 8) + "\n";
            report1 += "19,第三条大地线的球面长度σ," + Math.Round(sigma, 6) + "\n";
            report1 += "20,第三条大地线经差改正数," + Math.Round(gai, 8) + "\n";
            report1 += "21,第三条大地线终点纬度B2," + rB2 + "\n";
            report1 += "22,第三条大地线终点经度L2," + rL2 + "\n";
            report1 += "23,第三条大地线终点坐标方位角A2," + rA2 + "\n";


            return report1;
        }
        

    }

}
