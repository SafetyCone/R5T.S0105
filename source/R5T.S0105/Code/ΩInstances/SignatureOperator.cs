using System;


namespace R5T.S0105
{
    public class SignatureOperator_Old : ISignatureOperator_Old
    {
        #region Infrastructure

        public static ISignatureOperator_Old Instance { get; } = new SignatureOperator_Old();


        private SignatureOperator_Old()
        {
        }

        #endregion
    }
}


namespace R5T.S0105
{
    public class SignatureOperator_New : ISignatureOperator_New
    {
        #region Infrastructure

        public static ISignatureOperator_New Instance { get; } = new SignatureOperator_New();


        private SignatureOperator_New()
        {
        }

        #endregion
    }
}
