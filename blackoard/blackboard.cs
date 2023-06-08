using System;
using System.Collections.Generic;

namespace BlackboardPattern
{
    // Компонент (Component)
    public interface IComponent
    {
        void Update();
    }

    // Компонент A (Component A)
    public class ComponentA : IComponent
    {
        private Blackboard blackboard;

        public ComponentA(Blackboard blackboard)
        {
            this.blackboard = blackboard;
        }

        public void Update()
        {
            // Отримати необхідні дані з дошки
            var data = blackboard.GetData("dataA");

            // Виконати обробку даних
            var result = $"Processed data A: {data}";

            // Записати результат на дошку
            blackboard.SetData("resultA", result);
        }
    }

    // Компонент B (Component B)
    public class ComponentB : IComponent
    {
        private Blackboard blackboard;

        public ComponentB(Blackboard blackboard)
        {
            this.blackboard = blackboard;
        }

        public void Update()
        {
            // Отримати необхідні дані з дошки
            var data = blackboard.GetData("dataB");

            // Виконати обробку даних
            var result = $"Processed data B: {data}";

            // Записати результат на дошку
            blackboard.SetData("resultB", result);
        }
    }

    // Дошка (Blackboard)
    public class Blackboard
    {
        private Dictionary<string, object> data = new Dictionary<string, object>();

        public void SetData(string key, object value)
        {
            data[key] = value;
        }

        public object GetData(string key)
        {
            if (data.ContainsKey(key))
            {
                return data[key];
            }

            return null;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Створення дошки
            var blackboard = new Blackboard();

            // Створення компонентів
            var componentA = new ComponentA(blackboard);
            var componentB = new ComponentB(blackboard);

            // Запис даних на дошку
            blackboard.SetData("dataA", "Data A");
            blackboard.SetData("dataB", "Data B");

		    // Оновлення компонентів
            componentA.Update();
            componentB.Update();

            // Отримання результатів з дошки
            var resultA = blackboard.GetData("resultA");
            var resultB = blackboard.GetData("resultB");

            // Виведення результатів
            Console.WriteLine($"Result A: {resultA}");
            Console.WriteLine($"Result B: {resultB}");
        }
    }
}

