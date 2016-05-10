using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Determinant
{
    //该类用来计算行列式的值
    class Caculate
    {
        public double determinante(double[,] mat, int n)
        {
            int length = n;
            double sum = 0;
            int cnt = 0;
            double[,] aux = new double[length - 1, length - 1];
        
            if(length == 2)//如果是二阶行列式就直接计算
            {
                sum = mat[0, 0]*mat[1, 1] - mat[0, 1]*mat[1, 0];
                return sum;
            }

            //n阶行列式的值d等于其中任一行（列）元素与其代数余子式的乘积的和
            for (int d1 = 0; d1 < length; d1++)
            {
                for(int d2 = 0; d2 < length - 1; d2++)
                {            
                    for(int d3 = 0; d3 < length; d3++)
                    {               
                        if(d3==d1)
                        {
                            continue;
                        }
                        aux[d2, cnt] = mat[d2+1, d3];//取第0行个元素的余子式
                        cnt++;
                    } 
                    cnt  = 0;
                }

                //余子式!=代数余子式
                if(d1%2==0)
                {
                    sum += mat[0, d1] * determinante(aux, length - 1);
                }
                else
                {
                    sum -= mat[0, d1] * determinante(aux, length - 1);
                }
            }

            return sum;
        }
    }
}
