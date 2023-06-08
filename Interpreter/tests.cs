using NUnit.Framework;

namespace InterpreterPattern.Tests
{
    [TestFixture]
    public class InterpreterTests
    {
        [Test]
        public void Interpret_ExpressionWithVariables_ReturnsCorrectResult()
        {
            // Arrange
            var context = new Context();
            context.SetVariable("A", true);
            context.SetVariable("B", false);
            context.SetVariable("C", true);

            // Create the expression: (A && !B) || C
            var expression1 = new AndExpression(new VariableExpression("A"), new NotExpression(new VariableExpression("B")));
            var expression2 = new OrExpression(expression1, new VariableExpression("C"));

            // Act
            bool result = expression2.Interpret(context);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Interpret_ExpressionWithUndefinedVariables_ReturnsFalse()
        {
            // Arrange
            var context = new Context();
            context.SetVariable("A", true);

            // Create the expression: A && B
            var expression = new AndExpression(new VariableExpression("A"), new VariableExpression("B"));

            // Act
            bool result = expression.Interpret(context);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Interpret_ExpressionWithNotOperator_ReturnsCorrectResult()
        {
            // Arrange
            var context = new Context();
            context.SetVariable("A", true);
            context.SetVariable("B", false);

            // Create the expression: !A
            var expression = new NotExpression(new VariableExpression("A"));

            // Act
            bool result = expression.Interpret(context);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
