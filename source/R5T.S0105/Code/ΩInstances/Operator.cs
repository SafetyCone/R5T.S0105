using System;


namespace R5T.S0105
{
    public class Operator : IOperator
    {
        #region Infrastructure

        public static IOperator Instance { get; } = new Operator();


        private Operator()
        {
        }

        #endregion
    }
}
