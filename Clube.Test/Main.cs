using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Clube.Test
{
    [TestFixture]
    public class Main
    {
        [Test]
        public void TesSairDoPrograma()
        {
            Assert.IsTrue(Module1.SairDoPrograma("sim"));
        }
    }
}
