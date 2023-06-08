using System;
using System.Collections.Generic;

namespace InterpreterPattern
{
    // Контекст (Context)
    public class Context
    {
        private Dictionary<string, bool> variables;

        public Context()
        {
            variables = new Dictionary<string, bool>();
        }

        public void SetVariable(string name, bool value)
        {
            variables[name] = value;
        }

        public bool GetVariable(string name)
        {
            if (variables.ContainsKey(name))
            {
                return variables[name];
            }

            return false;
        }
    }

    // Абстрактний вираз (AbstractExpression)
    public abstract class Expression
    {
        public abstract bool Interpret(Context context);
    }

    // Термінальний вираз (TerminalExpression)
    public class VariableExpression : Expression
    {
        private string variableName;

        public VariableExpression(string variableName)
        {
            this.variableName = variableName;
        }

        public override bool Interpret(Context context)
        {
            return context.GetVariable(variableName);
        }
    }

    // Негативний вираз (NegativeExpression)
    public class NotExpression : Expression
    {
        private Expression expression;

        public NotExpression(Expression expression)
        {
            this.expression = expression;
        }

        public override bool Interpret(Context context)
        {
            return !expression.Interpret(context);
        }
    }

    // Логічний вираз "І" (AndExpression)
    public class AndExpression : Expression
    {
        private Expression leftExpression;
        private Expression rightExpression;

        public AndExpression(Expression leftExpression, Expression rightExpression)
        {
            this.leftExpression = leftExpression;
            this.rightExpression = rightExpression;
        }

        public override bool Interpret(Context context)
        {
            return leftExpression.Interpret(context) && rightExpression.Interpret(context);
        }
    }

    // Логічний вираз "АБО" (OrExpression)
    public class OrExpression : Expression
    {
        private Expression leftExpression;
        private Expression rightExpression;

        public OrExpression(Expression leftExpression, Expression rightExpression)
        {
            this.leftExpression = leftExpression;
            this.rightExpression = rightExpression;
        }

        public override bool Interpret(Context context)
        {
            return leftExpression.Interpret(context) || rightExpression.Interpret(context);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Створення контексту
            var context = new Context();

            // Встановлення значень змінних
            context.SetVariable("A", true);
            context.SetVariable("B", false);
            context.SetVariable("C", true);

            // Створення виразів
            var expression1 = new AndExpression(new VariableExpression("A"), new NotExpression(new VariableExpression("B")));
            var expression2 = new OrExpression(expression1, new VariableExpression("C"));

            // Інтерпретація виразу
            bool result = expression2.Interpret(context);

            Console.WriteLine($"Expression result: {result}");
        }
    }
}
