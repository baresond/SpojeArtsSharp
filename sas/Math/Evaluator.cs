using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sas.Math
{
    public class Evaluator
    {
        private readonly ExpressionSyntax _root;

        public Evaluator(ExpressionSyntax root)
        {
            _root = root;
        }

        public int Evaluate() 
        {
            return EvaluateException(_root);
        }

        public int EvaluateException(ExpressionSyntax root)
        {
            if (root is NumberExpressionSyntax n)
                return (int)n.NumberToken.Value;

            if (root is BinaryExpressionSyntax b)
            {
                var left = EvaluateException(b.Left);
                var right = EvaluateException(b.Right);

                switch (b.OperatorToken.Kind)
                {
                    case SyntaxKind.PlusToken:
                        return left + right;
                    case SyntaxKind.MinusToken:
                        return left - right;
                    case SyntaxKind.StarToken:
                        return left * right;
                    case SyntaxKind.SlashToken:
                        return left / right;
                    default:
                        throw new Exception($"Unexpected binary operator <{b.OperatorToken.Kind}>");
                }
            }

            if (root is ParenthesizedExpressionSyntax p)
                return EvaluateException(p.Expression);

            throw new Exception($"Unexpected node <{root.Kind}>");
        }
    }
}
