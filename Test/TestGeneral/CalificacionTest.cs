using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestGeneral
{
    public class CalificacionTest
    {
        [Theory]
        [InlineData(73, 75)]
        [InlineData(67, 67)]
        [InlineData(38, 40)]
        [InlineData(33, 33)]
        public void CalificacionFinalTest(int entrada, int valEsp)
        {
           
            //act
            int califFinal1 = Calificaciones.CalculaCalificacionFinal(entrada);
            //assert
            Assert.Equal(valEsp, califFinal1);

        }

    }
}
