using System;
using System.Collections.Generic;
using System.Reflection;

using R5T.T0131;
using R5T.T0172;
using R5T.T0172.Extensions;


namespace R5T.S0105
{
    [ValuesMarker]
    public partial interface IOperations : IValuesMarker
    {
        public Action<MemberInfo> Verify_SignatureStringViaIdentityString(IDictionary<IAssemblyFilePath, AssemblySignatureStringsGenerationResult> results)
        {
            void Internal(MemberInfo memberInfo)
            {
                var assembly = Instances.MemberInfoOperator.Get_Assembly(memberInfo);
                var assemblyFilePath = Instances.AssemblyOperator.Get_AssemblyFilePath(assembly)
                    .ToAssemblyFilePath();

                var result = Instances.DictionaryOperator.Acquire_Value(
                    results,
                    assemblyFilePath,
                    () => new AssemblySignatureStringsGenerationResult(assemblyFilePath));

                var identityString = Instances.MemberInfoOperator.Get_IdentityString(memberInfo);

                try
                {
                    var signatureString = Instances.MemberInfoOperator.Get_SignatureString(memberInfo);

                    var pair = new SignatureStringIdentityPair
                    {
                        IdentityString = identityString,
                        SignatureString = signatureString,
                    };

                    var areEqual = Instances.Operator.Check_SignatureStringViaIdentityString(
                        pair.SignatureString,
                        pair.IdentityString,
                        out var identityStringFromStructuredSignatureString);

                    if (areEqual)
                    {
                        result.Successes.Add(pair);
                    }
                    else
                    {
                        var failurePair = new SignatureStringIdentityFailurePair
                        {
                            IdentityString = pair.IdentityString,
                            SignatureString = pair.SignatureString,
                            StructuredIdentityString = identityStringFromStructuredSignatureString,
                        };

                        result.Failures.Add(failurePair);
                    }
                }
                catch (Exception)
                {
                    result.Exceptions.Add(identityString);
                }
            }

            return Internal;
        }
    }
}
