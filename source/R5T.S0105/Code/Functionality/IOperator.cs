using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using R5T.L0062.T000;
using R5T.L0063.T000;
using R5T.T0132;
using R5T.T0172;
using R5T.T0181;


namespace R5T.S0105
{
    [FunctionalityMarker]
    public partial interface IOperator : IFunctionalityMarker
    {
        public void Write_ResultsToOutput(
            ITextFilePath outputFilePath,
            IDictionary<IAssemblyFilePath, N001.AssemblySignatureStringsGenerationResult> results,
            bool showSuccesses = true)
        {
            // Now write out results.
            var resultsToOutput = results
                .Where(pair => pair.Value.Exceptions.Any() || pair.Value.Failures.Any() || (pair.Value.Successes.Any() && showSuccesses))
                .Select(pair => pair.Value)
                ;

            var lines = Instances.EnumerableOperator.From("Signature string pairs (round-tripped) in .NET pack assemblies.")
                .AppendIf(!resultsToOutput.Any() || showSuccesses, "=> All signature string pairs matched.\n")
                .AppendIf(resultsToOutput.Any(), resultsToOutput
                    .SelectMany(result =>
                    {
                        var output = Instances.EnumerableOperator.From($"{result.AssemblyFilePath}:")
                            .AppendIf(result.Exceptions.Any(), Instances.EnumerableOperator.From($"{result.Exceptions.Count} - Exceptions (identity strings):")
                                .Append(result.Exceptions
                                    .Select(x => $"\t{x.Value}")
                                )
                            )
                            .AppendIf(!result.Exceptions.Any(), "<No exceptions>")
                            .AppendIf(result.Failures.Any(), Instances.EnumerableOperator.From($"{result.Failures.Count} - Failures (signature string/identity string):")
                                .Append(result.Failures
                                    .Select(x => $"\t{x.SignatureString}\n\t{x.IdentityString}\n")
                                )
                            )
                            .AppendIf(!result.Failures.Any(), "<No failures>")
                            .AppendIf(result.Successes.Any() && showSuccesses, Instances.EnumerableOperator.From($"{result.Successes.Count} - Successes (signature string/identity strings):")
                                .Append(result.Successes
                                    .Select(x => $"\t{x.SignatureString}\n\t{x.IdentityString}")
                                )
                            )
                            .AppendIf(!result.Successes.Any() && showSuccesses, "<No successes>")
                            ;

                        return output;
                    }))
                ;

            Instances.FileOperator.Write_Lines_Synchronous(
                outputFilePath.Value,
                lines);
        }

        public bool Check_SignatureStringViaIdentityString(
            ISignatureString signatureString,
            IIdentityString identityString,
            out IIdentityString identityStringFromStructuredSignatureString)
        {
            var signature = Instances.SignatureStringOperator.Get_Signature(signatureString);

            identityStringFromStructuredSignatureString = Instances.SignatureOperator.Get_IdentityString(signature);

            // Check the two identity strings for equality.
            var output = identityString.Equals(identityStringFromStructuredSignatureString);
            return output;
        }

        public SignatureStringIdentityPair Generate_StringPair(MemberInfo memberInfo)
        {
            var identityString = Instances.MemberInfoOperator.Get_IdentityString(memberInfo);

            var signatureString = Instances.MemberInfoOperator.Get_SignatureString(memberInfo);

            var pair = new SignatureStringIdentityPair
            {
                IdentityString = identityString,
                SignatureString = signatureString,
            };

            return pair;
        }

        public void Write_ResultsToOutput(
            ITextFilePath outputFilePath,
            IDictionary<IAssemblyFilePath, AssemblySignatureStringsGenerationResult> results,
            bool showSuccesses = true)
        {
            // Now write out results.
            var resultsToOutput = results
                .Where(pair => pair.Value.Exceptions.Any() || pair.Value.Failures.Any() || (pair.Value.Successes.Any() && showSuccesses))
                .Select(pair => pair.Value)
                ;

            var lines = Instances.EnumerableOperator.From("Signature string and identity string pairs in .NET pack assemblies.")
                .Append(resultsToOutput
                    .SelectMany(result =>
                    {
                        var output = Instances.EnumerableOperator.From($"{result.AssemblyFilePath}:")
                            .AppendIf(result.Exceptions.Any(), Instances.EnumerableOperator.From($"{result.Exceptions.Count} - Exceptions (identity strings):")
                                .Append(result.Exceptions
                                    .Select(x => $"\t{x.Value}")
                                )
                            )
                            .AppendIf(!result.Exceptions.Any(), "<No exceptions>")
                            .AppendIf(result.Failures.Any(), Instances.EnumerableOperator.From($"{result.Failures.Count} - Failures (signature/identity string/structured identity string):")
                                .Append(result.Failures
                                    .Select(x => $"\t{x.SignatureString}\n\t{x.IdentityString}\n\t{x.StructuredIdentityString}\n")
                                )
                            )
                            .AppendIf(!result.Failures.Any(), "<No failures>")
                            .AppendIf(result.Successes.Any() && showSuccesses, Instances.EnumerableOperator.From($"{result.Successes.Count} - Successes (signature/identity strings):")
                                .Append(result.Successes
                                    .Select(x => $"\t{x.SignatureString}\n\t{x.IdentityString}")
                                )
                            )
                            .AppendIf(!result.Successes.Any() && showSuccesses, "<No successes>")
                            ;

                        return output;
                    }))
                ;

            Instances.FileOperator.Write_Lines_Synchronous(
                outputFilePath.Value,
                lines);
        }
    }
}
