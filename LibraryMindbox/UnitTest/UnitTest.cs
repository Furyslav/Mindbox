using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        

       [TestMethod]
        public void Testing()
        {

            //площадь круга по его радиусу 
            // если подать float и сравнивать с double(decimal) будут разности точности т.к. double точнее
            Assert.AreEqual(Math.Floor(Math.PI * 3d* 3d), Math.Floor(LibraryMindbox.CountSquare.CircleRadius(3f)));
            Assert.AreEqual(Math.PI * 3f * 3f, LibraryMindbox.CountSquare.CircleRadius(3d));
            Assert.AreEqual(Math.PI * 3d * 3d, LibraryMindbox.CountSquare.CircleRadius(3d));

            //площадь прямоугольного треугольника
            Assert.AreEqual(24, LibraryMindbox.CountSquare.StraightRectangle(8f, 6f));
            Assert.AreEqual(24f, LibraryMindbox.CountSquare.StraightRectangle(8f, 6f));
            Assert.AreEqual(24, LibraryMindbox.CountSquare.StraightRectangle(8d, 6d));
            Assert.AreEqual(24f, LibraryMindbox.CountSquare.StraightRectangle(8d, 6d));
            Assert.AreEqual(24d, LibraryMindbox.CountSquare.StraightRectangle(8d, 6d));

            //throws

            string error = "";
            try
            {
                LibraryMindbox.CountSquare.StraightRectangle(0, 6f);
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            Assert.AreNotEqual("", error);

            error = "";
            try
            {
                LibraryMindbox.CountSquare.StraightRectangle(4, 6f);
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            Assert.AreEqual("", error);

            error = "";
            try
            {
                LibraryMindbox.CountSquare.StraightRectangle(1f, 0f);
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            Assert.AreNotEqual("", error);

            error = "";
            try
            {
                LibraryMindbox.CountSquare.CircleRadius(1f);
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            Assert.AreEqual("", error);

            error = "";
            try
            {
                LibraryMindbox.CountSquare.CircleRadius(0);
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            Assert.AreNotEqual("", error);

            error = "";
            try
            {
                LibraryMindbox.CountFormula.Evaluate("");
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            Assert.AreNotEqual("", error);

            error = "";
            try
            {
                LibraryMindbox.CountFormula.Evaluate("2+");
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            Assert.AreNotEqual("", error);

            error = "";
            try
            {
                LibraryMindbox.CountFormula.Evaluate("2+4/0");
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            Assert.AreNotEqual("", error);

            try
            {
                LibraryMindbox.CountFormula.Evaluate("+2");
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            Assert.AreNotEqual("", error);


            //formula
            Assert.AreEqual(3 + 2 * 4 + 7, LibraryMindbox.CountFormula.Evaluate("3+2*4+7"));
            Assert.AreEqual(3 + 2 * 4 + Math.Pow(2,4), LibraryMindbox.CountFormula.Evaluate("3+2*4+2^4"));
            Assert.AreEqual(2 + 2 * 2, LibraryMindbox.CountFormula.Evaluate("2 + 2 * 2"));
            Assert.AreEqual(2*2 + 2 * 2, LibraryMindbox.CountFormula.Evaluate("2*2 + 2 * 2"));
            Assert.AreEqual(2 * 2 + 2, LibraryMindbox.CountFormula.Evaluate("2 * 2 + 2"));
            Assert.AreEqual(2 * (2 + 2), LibraryMindbox.CountFormula.Evaluate("2 * (2 + 2)"));
            Assert.AreEqual(2 * Math.Pow((2 + 2),2), LibraryMindbox.CountFormula.Evaluate("2 * (2 + 2)^2"));
            Assert.AreEqual(2 * Math.Pow((2 + 2), 2)-5, LibraryMindbox.CountFormula.Evaluate("2 * (2 + 2)^2-5"));
            Assert.AreEqual(3 + (double)2/4 *4 + 7, LibraryMindbox.CountFormula.Evaluate("3 + (2/4)*4 + 7"));


        }
    }
}
