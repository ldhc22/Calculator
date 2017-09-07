using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoreSoft.MathExpressions;

namespace CalculatorUnitTesting
{
    [TestClass]
    public class ArtimeticTest
    {
        MathEvaluator eval;

        [TestInitialize]
        public void Setup()
        {
            eval = new MathEvaluator();
        }

        [TestMethod]
        [TestCategory("Aritmetic")]
        public void EvaluateNegative()
        {
            double expected = 2d + -1d;
            double result = eval.Evaluate("2 + -1");
            Assert.AreEqual(expected, result);

            expected = -2d + 1d;
            result = eval.Evaluate("-2 + 1");
            Assert.AreEqual(expected, result);

            expected = (2d + -1d) * (-1d + 2d);
            result = eval.Evaluate("(2 + -1) * (-1 + 2)");
            Assert.AreEqual(expected, result);

            // this failed due to a bug in parsing whereby the minus sign was erroneously mistaken for a negative sign.  
            // which left the -4 on the calculationStack at the end of evaluation. 
            expected = (-4 - 3) * 5;
            result = eval.Evaluate("(-4-3) *5");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Expresion")]
        public void EvaluateLog10()
        {
            double result = eval.Evaluate("log10(10)");
            Assert.AreEqual(1d, result);
        }

        [TestMethod]
        [TestCategory("Aritmetic")]
        public void EvaluateSimple()
        {
            double expected = (2d + 1d) * (1d + 2d);
            double result = eval.Evaluate("(2 + 1) * (1 + 2)");

            Assert.AreEqual(expected, result);

            expected = 2d + 1d * 1d + 2d;
            result = eval.Evaluate("2 + 1 * 1 + 2");

            Assert.AreEqual(expected, result);

            expected = 1d / 2d;
            result = eval.Evaluate("1/2");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Aritmetic")]
        public void EvaluateComplex()
        {
            double expected = ((1d + 2d) + 3d) * 2d - 8d / 4d;
            double result = eval.Evaluate("((1 + 2) + 3) * 2 - 8 / 4");

            Assert.AreEqual(expected, result);

            expected = 3d + 4d / 5d - 8d;
            result = eval.Evaluate("3+4/5-8");

            Assert.AreEqual(expected, result);

            expected = Math.Pow(1, 2) + 5 * 1 + 14;
            result = eval.Evaluate("1 ^ 2 + 5 * 1 + 14");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Expresion")]
        public void EvaluateFunctionMinWithinParenthesis()
        {
            // this should work... but doesnt.
            double expected = (3 * Math.Min(45, 50));
            double result = eval.Evaluate("(3 * Min(45,50))");

            Assert.AreEqual(expected, result);
        }  

        [TestMethod]
        [TestCategory("Expresion")]
        public void EvaluateFunctionMax()
        {
            double expected = Math.Max(45, 50);
            double result = eval.Evaluate("max(45, 50)");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Expresion")]
        public void EvaluateFunctionMaxNested()
        {
            double expected = Math.Max(3, Math.Max(45, 50));
            double result = eval.Evaluate("max(3, max(45,50))");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Expresion")]
        public void EvaluateFunctionHasTooManyArguments()
        {
            try
            {
                eval.Evaluate("max(1,2,3,4)");
                Assert.Fail("No Parse Exception thrown");
            }
            catch (LoreSoft.MathExpressions.ParseException pe)
            {

            }
        }
    }
}
