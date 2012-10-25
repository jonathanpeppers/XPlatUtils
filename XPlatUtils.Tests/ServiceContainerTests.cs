using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace XPlatUtils.Tests {
    [TestFixture]
    public class ServiceContainerTests {

        #region Test Types

        class ClassA : InterfaceA { }

        interface InterfaceA { }

        #endregion

        [SetUp]
        public void SetUp ()
        {
            //Wipe out the container before each test
            ServiceContainer.Clear ();
        }

        [Test]
        public void RegisterInstance ()
        {
            var obj = new { ID = 1 };

            ServiceContainer.Register (obj);

            Assert.That (ServiceContainer.Resolve (obj.GetType ()), Is.EqualTo (obj));
        }

        [Test]
        public void RegisterInstanceWithType ()
        {
            var obj = new { ID = 1 };

            ServiceContainer.Register (obj.GetType(), obj);

            Assert.That (ServiceContainer.Resolve (obj.GetType ()), Is.EqualTo (obj));
        }

        [Test]
        public void RegisterInstanceGeneric ()
        {
            var obj = new ClassA ();

            ServiceContainer.Register (obj);

            Assert.That (ServiceContainer.Resolve<ClassA>(), Is.EqualTo (obj));
        }

        [Test]
        public void RegisterInterface ()
        {
            var obj = new ClassA ();

            ServiceContainer.Register<InterfaceA> (obj);

            Assert.That (ServiceContainer.Resolve<InterfaceA> (), Is.EqualTo (obj));
        }

        [Test]
        public void OverwriteRegistration ()
        {
            var obj1 = new ClassA ();
            var obj2 = new ClassA ();

            ServiceContainer.Register (obj1);
            ServiceContainer.Register (obj2);

            Assert.That (ServiceContainer.Resolve<ClassA> (), Is.EqualTo (obj2));
        }

        [Test]
        public void RegisterNewConstraint ()
        {
            ServiceContainer.Register<ClassA> ();

            Assert.That (ServiceContainer.Resolve<ClassA> (), Is.InstanceOf<ClassA> ());
        }

        [Test]
        public void RegisterWithFunc ()
        {
            ClassA obj = null;

            ServiceContainer.Register (typeof (ClassA), () => obj = new ClassA ());

            Assert.That (obj, Is.Null);

            var actual = ServiceContainer.Resolve<ClassA> ();

            Assert.That (obj, Is.Not.Null);
            Assert.That (actual, Is.EqualTo (obj));
        }

        [Test]
        public void RegisterWithFuncGeneric ()
        {
            ClassA obj = null;

            ServiceContainer.Register (() => obj = new ClassA ());

            Assert.That (obj, Is.Null);

            var actual = ServiceContainer.Resolve<ClassA> ();

            Assert.That (obj, Is.Not.Null);
            Assert.That (actual, Is.EqualTo (obj));
        }

        [Test, ExpectedException(typeof(KeyNotFoundException))]
        public void NotFound ()
        {
            ServiceContainer.Resolve<ClassA> ();
        }

        [Test, ExpectedException (typeof (KeyNotFoundException))]
        public void NotFoundWithWrongType ()
        {
            ServiceContainer.Register<ClassA> ();

            ServiceContainer.Resolve<InterfaceA> ();
        }
    }
}