using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4_Kalkulacka
{
    class Core
    {
        public double Compute(List<string> chain)
        {
            Queue<string> queue = new Queue<string>();
            Stack<string> stack = new Stack<string>();
            Stack<double> numstack = new Stack<double>();
            // Implementation of a Shunting-Yard algorithm
            foreach (string token in chain)
            {
                // Put a token into queue if its a number
                if (IsNumeric(token))
                {
                    queue.Enqueue(token);
                }
                // If its left bracket, push it into the stack
                else if (token == "(")
                {
                    stack.Push(token);
                }
                // If its right bracket, pop all operators added after last left bracket to ensure correct calculation order
                else if (token == ")")
                {
                    while (stack.Peek() != "(")
                    {
                        queue.Enqueue(stack.Pop());
                    }
                    // Discard both brackets because they are no longer needed for post-fix calculations
                    stack.Pop();
                }
                // If its a operator, pop all other operators from the stack that has greater calculation priority and push
                // current operator into the stack
                else
                {
                    if (stack.Count > 0)
                    {
                        while (GetOperatorValue(stack.Peek()) > GetOperatorValue(token))
                        {
                            queue.Enqueue(stack.Pop());
                            if (stack.Count == 0)
                            {
                                break;
                            }
                        }
                    }
                    stack.Push(token);
                }
                // Pop the remaining operators and put them into queue
            }
            while (stack.Count > 0)
            {
                queue.Enqueue(stack.Pop());

            }

           foreach(string token in queue)
            {
                Console.Write(token + ",");
            } 

            // Implementation of the post-fix expresion calculations:
            // If a token is a number put it into a new numberstack, else its an operator in which case pop 2 numbers from a numberstack
            // and evaluate them according to the current operator. Push then the result into numberstack. If its the last number there
            // return it as the result
            while (queue.Count > 0)
            {
                string token = queue.Dequeue();
                if (IsNumeric(token))
                {
                    numstack.Push(Double.Parse(token));
                    
                }
                else
                {
                    double rightnum = numstack.Pop();
                    double leftnum = numstack.Pop();
                    numstack.Push(Evaluate(leftnum, rightnum, token));
                   
                    if (numstack.Count == 1 && queue.Count == 0)
                    {
                        
                        return numstack.Pop();
                        
                    }
                }
            }
            // This code will never be executed but must be here to not throw an error
            return -1;

        }
        // Checks whether a token in a string form is a number
        private bool IsNumeric(string token)
        {
            try
            {
                Double.Parse(token);
                return true;
            }
            catch (Exception e)
            {

            }
            return false;

        }
        // Returns a operator value according to their priority in calculations. Returns 0 for brackets to prevent them from being in
        // the output queue in the Shunting-Yard algorithm
        private int GetOperatorValue(string token)
        {
            if (token == "(" || token == ")")
            {
                return 0;
            }
            else if (token == "^")
            {
                return 3;
            }
            else if (token == "*" || token == "/")
            {
                return 2;
            }
            return 1;
        }
        // Evaluates two numbers with given operator and returns the result
        private double Evaluate(double leftnum, double rightnum, string _operator)
        {
            if (_operator == "^")
            {
                return Math.Pow(leftnum, rightnum);
            }
            else if (_operator == "*")
            {
                return leftnum * rightnum;
            }
            else if (_operator == "/")
            {
                return leftnum / rightnum;
            }
            else if (_operator == "-")
            {
                return leftnum - rightnum;
            }
            return leftnum + rightnum;
        }
    }
}
