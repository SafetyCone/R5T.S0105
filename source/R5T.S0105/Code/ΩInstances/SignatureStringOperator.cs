using System;


namespace R5T.S0105
{
    public class SignatureStringOperator_Old : ISignatureStringOperator_Old
    {
        #region Infrastructure

        public static ISignatureStringOperator_Old Instance { get; } = new SignatureStringOperator_Old();


        private SignatureStringOperator_Old()
        {
        }

        #endregion
    }
}
