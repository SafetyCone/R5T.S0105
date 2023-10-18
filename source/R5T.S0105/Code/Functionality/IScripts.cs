using System;
using System.Collections.Generic;
using System.Linq;

using R5T.L0053.Extensions;
using R5T.L0062.T000;
using R5T.L0063.T000;
using R5T.T0132;
using R5T.T0172;


namespace R5T.S0105
{
    [FunctionalityMarker]
    public partial interface IScripts : IFunctionalityMarker
    {
        /// <summary>
        /// For all members of a .NET pack, generate signature structures
        /// (validated via member info-to-signature structure-to-identity string,
        /// validated with comparison to identity strings generated from member infos,
        /// validated with identity string comparison with identity strings in the documentation files of the .NET pack's assemblies),
        /// round-trip the signature structures to signature strings,
        /// then check equality.
        /// </summary>
        public void RoundTrip_Signatures_ToSignatureStrings()
        {
            /// Inputs.
            var dotnetPackName = Instances.DotnetPackNames.Microsoft_NETCore_App_Ref;
            var targetFramework = Instances.TargetFrameworkMonikers.NET_6;
            var outputFilePath = Instances.FilePaths.OutputTextFilePath;
            var showSuccesses = false;


            /// Run.
            var (humanOutputTextFilePath, logFilePath) = Instances.TextOutputOperator.In_TextOutputContext(
                nameof(RoundTrip_Signatures_ToSignatureStrings),
                textOutput =>
                {
                    // For results, per assembly, we want:
                    // 1) Identity strings of exceptions
                    // 2) Pairs of identity strings and signature strings,
                    // 3) for successes and failures.
                    var results = new Dictionary<IAssemblyFilePath, N001.AssemblySignatureStringsGenerationResult>();

                    Instances.DotnetPackPathOperator.Foreach_MemberInfo(
                        dotnetPackName,
                        targetFramework,
                        textOutput,
                        (memberInfo, assembly, assemblyFilePath, documentationFilePath) =>
                        {
                            //// For debugging - select based on analysis of member info.
                            //if (assemblyFilePath.Value != @"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.23\ref\net6.0\System.Reflection.Metadata.dll")
                            //{
                            //    return;
                            //}

                            // Generate identity string for the member for use as a locator reference for the member.
                            var identityString = Instances.MemberInfoOperator.Get_IdentityString(memberInfo);

                            Console.WriteLine(identityString);

                            //// For debugging.
                            //if (identityString.Value != "M:System.Reflection.PortableExecutable.ManagedPEBuilder.CreateSections")
                            //{
                            //    return;
                            //}

                            var result = Instances.DictionaryOperator.Acquire_Value(
                                results,
                                assemblyFilePath,
                                () => new N001.AssemblySignatureStringsGenerationResult(assemblyFilePath));

                            try
                            {
                                var signature = Instances.SignatureOperator.Get_Signature(memberInfo);

                                var signatureString = Instances.SignatureOperator.Get_SignatureString(signature);

                                //// For debugging.
                                //if (signatureString.Value != "M:System.Collections.Concurrent.BlockingCollection<`T>.System#Collections#Generic#IEnumerable{T}#GetEnumerator()~System.Collections.Generic.IEnumerator<`T>")
                                //{
                                //    return;
                                //}

                                var roundTrippedSignature = Instances.SignatureStringOperator.Get_Signature(signatureString);

                                var pair = new SignatureStringIdentityPair
                                {
                                    IdentityString = identityString,
                                    SignatureString = signatureString,
                                };

                                var areEqual = Instances.SignatureOperator.Are_Equal_ByValue(
                                    signature,
                                    roundTrippedSignature);

                                if (areEqual)
                                {
                                    result.Successes.Add(pair);
                                }
                                else
                                {
                                    result.Failures.Add(pair);
                                }
                            }
                            catch(Exception)
                            {
                                result.Exceptions.Add(identityString);
                            }
                        });

                    Instances.Operator.Write_ResultsToOutput(
                        outputFilePath,
                        results,
                        showSuccesses);
                });

            Instances.NotepadPlusPlusOperator.Open(
                outputFilePath);
        }

        public void RoundTrip_SignatureString()
        {
            /// Inputs.
            var outputFilePath = Instances.FilePaths.OutputTextFilePath;
            var signatureString =
                //Instances.SignatureStrings_Raw.T_002
                //Instances.SignatureStrings_Raw.T_003
                //Instances.SignatureStrings_Raw.M_001
                //Instances.SignatureStrings_Raw.E_001
                //Instances.SignatureStrings_Raw.E_002
                //Instances.SignatureStrings_Raw.F_001
                //Instances.SignatureStrings_Raw.F_002
                //Instances.SignatureStrings_Raw.P_001
                //Instances.SignatureStrings_Raw.P_002
                //Instances.SignatureStrings_Raw.T_004 // An obsolete type.
                //Instances.SignatureStrings_Raw.M_002 // An obsolete method.
                //Instances.SignatureStrings_Raw.P_003 // An obsolete property.
                //Instances.SignatureStrings_Raw.E_003 // An obsolete event.
                Instances.SignatureStrings_Raw.F_003 // An obsolete field.
                ;


            /// Run.
            var signature = Instances.SignatureStringOperator.Get_Signature(signatureString);

            var roundTrippedSignatureString = Instances.SignatureOperator.Get_SignatureString(signature);

            var areEqual = signatureString.Equals(roundTrippedSignatureString.Value);

            var lines = Instances.EnumerableOperator.Empty<string>()
                .AppendIf(areEqual, "Success:")
                .AppendIf(!areEqual, "FAIL:")
                .Append($"(Round-tripped/Original):\n\t{roundTrippedSignatureString}\n\t{signatureString}")
                ;

            Instances.FileOperator.Write_Lines_Synchronous(
                outputFilePath.Value,
                lines);

            Instances.NotepadPlusPlusOperator.Open(outputFilePath);
        }

        public void Parse_SignatureString()
        {
            /// Inputs.
            var signatureString =
                //Instances.SignatureStrings_Raw.N_002
                Instances.SignatureStrings_Raw.T_003
                ;


            /// Run.
            var signature = Instances.SignatureStringOperator.Get_Signature(signatureString);

            Console.WriteLine(signature);
        }

        /// <summary>
        /// For the set of all example members, generate a signature string, round-trip the signature string to a structured signature and back, 
        /// </summary>
        public void GenerateAndRoundTrip_SignatureStrings()
        {
            /// Inputs.
            var outputFilePath = Instances.FilePaths.OutputTextFilePath;
            bool showRoundTripSuccesses = false;
            var members =
                Instances.ExampleMemberSets.All
                ;


            /// Run.
            var results = new List<(IIdentityString IdentityString, ISignatureString SignatureString, ISignatureString RoundTrippedSignatureString)>();

            foreach (var member in members)
            {
                var identityString = Instances.IdentityStringOperator.Get_IdentityString(member);
                var signatureString = Instances.SignatureStringOperator_Old.Get_SignatureString(member);
                var signature = Instances.SignatureStringOperator.Get_Signature(signatureString);
                var roundTrippedSignatureString = Instances.SignatureOperator.Get_SignatureString(signature);

                results.Add((identityString, signatureString, roundTrippedSignatureString));
            }

            var resultsToShow = results
                .Where(x =>
                {
                    // Show only those members whose signature strings are not equal, or show everything if we want to also show round trip successes.
                    if(showRoundTripSuccesses)
                    {
                        return true;
                    }

                    var areEqual = x.SignatureString.Equals(x.RoundTrippedSignatureString);

                    var output = !areEqual;
                    return output;
                })
                .Now();

            var lines = Instances.EnumerableOperator.Empty<string>()
                .AppendIf(!resultsToShow.Any(), "Success (no mismatches)")
                .AppendIf(resultsToShow.Any(), Instances.EnumerableOperator.From("Identity, Signature Strings (Round-tripped/Original)")
                    .Append(
                        resultsToShow.SelectMany(result =>
                        {
                            var areEqual = result.SignatureString.Equals(result.RoundTrippedSignatureString);

                            var lines = Instances.EnumerableOperator.Empty<string>()
                                .AppendIf(areEqual, "Success:")
                                .AppendIf(!areEqual, "FAIL:")
                                .Append($"\t{result.RoundTrippedSignatureString}\n\t{result.SignatureString}")
                                ;

                            return lines;
                        })
                    )
                )
                ;

            Instances.FileOperator.Write_Lines_Synchronous(
                outputFilePath.Value,
                lines);

            Instances.NotepadPlusPlusOperator.Open(outputFilePath);
        }

        public void Generate_SignatureString()
        {
            /// Inputs.
            var outputFilePath = Instances.FilePaths.OutputTextFilePath;
            var member =
                //Instances.ExampleMembers.Class_001
                //Instances.ExampleMembers.NestedType_001
                //Instances.ExampleMembers.NestedClass_002_Open
                //Instances.ExampleMembers.NestedType_003_Open
                //Instances.ExampleMembers.Method_006_1
                //Instances.ExampleMembers.Event_001
                //Instances.ExampleMembers.Event_002
                //Instances.ExampleMembers.Field_001
                //Instances.ExampleMembers.Field_002
                //Instances.ExampleMembers.Property_001
                //Instances.ExampleMembers.Property_002
                //Instances.ExampleMembers.Class_005 // An obsolete class.
                //Instances.ExampleMembers.Method_020 // An obsolete method.
                //Instances.ExampleMembers.Property_003 // An obsolete property.
                //Instances.ExampleMembers.Event_003 // An obsolete event.
                Instances.ExampleMembers.Field_003 // An obsolete event.
                ;


            /// Run.
            var identityString = Instances.IdentityStringOperator.Get_IdentityString(member);
            var signatureString = Instances.SignatureStringOperator_Old.Get_SignatureString(member);

            Instances.FileOperator.Write_Lines_Synchronous(
                outputFilePath.Value,
                Instances.EnumerableOperator.From(
                    identityString.Value,
                    signatureString.Value));

            Instances.NotepadPlusPlusOperator.Open(outputFilePath);
        }

        public void GenerateAndCheck_ExampleMemberSignatureStrings()
        {
            /// Inputs.
            var outputFilePath = Instances.FilePaths.OutputTextFilePath;
            var members =
                //Instances.ExampleMemberSets.All
                new[]
                {
                    //Instances.ExampleMembers.Class_001,
                    Instances.ExampleMembers.NestedClass_001
                };


            /// Run.
            var (humanOutputTextFilePath, logFilePath) = Instances.TextOutputOperator.In_TextOutputContext(
                nameof(GenerateAndCheck_DotnetPackMemberSignatureStrings),
                textOutput =>
                {
                    var results = new Dictionary<IAssemblyFilePath, AssemblySignatureStringsGenerationResult>();

                    Instances.ActionOperator.Foreach_Of(
                        members,
                        Instances.Operations.Verify_SignatureStringViaIdentityString(results));

                    Instances.Operator.Write_ResultsToOutput(
                        outputFilePath,
                        results);
                });

            Instances.NotepadPlusPlusOperator.Open(outputFilePath);
        }

        /// <summary>
        /// For all members (types, methods, properties, fields, events) in a .NET pack (dotnet pack),
        /// generate signature strings, then test signature string generation relative to identity string generation.
        /// Output results to a file.
        /// </summary>
        public void GenerateAndCheck_DotnetPackMemberSignatureStrings()
        {
            /// Inputs.
            var dotnetPackName = Instances.DotnetPackNames.Microsoft_NETCore_App_Ref;
            var targetFramework = Instances.TargetFrameworkMonikers.NET_6;
            var outputFilePath = Instances.FilePaths.OutputTextFilePath;


            /// Run.
            var (humanOutputTextFilePath, logFilePath) = Instances.TextOutputOperator.In_TextOutputContext(
                nameof(GenerateAndCheck_DotnetPackMemberSignatureStrings),
                textOutput =>
                {
                    var results = new Dictionary<IAssemblyFilePath, AssemblySignatureStringsGenerationResult>();

                    Instances.DotnetPackPathOperator.Foreach_MemberInfo(
                        dotnetPackName,
                        targetFramework,
                        textOutput,
                        (memberInfo, assembly, assemblyFilePath, documentationFilePath) =>
                        {
                            // For debugging.
                            // Select based on analysis of MemberInfo.
                            if(assemblyFilePath.Value != @"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.22\ref\net6.0\System.Runtime.dll")
                            {
                                return;
                            }

                            // Generate the identity string.
                            // Note: this should always work, as it was tested in R5T.S0102.
                            var identityString = Instances.MemberInfoOperator.Get_IdentityString(memberInfo);

                            Console.WriteLine(identityString);

                            // For debugging.
                            if (identityString.Value != "M:System.AggregateException.#ctor(System.Collections.Generic.IEnumerable{System.Exception})")
                            {
                                return;
                            }

                            var result = Instances.DictionaryOperator.Acquire_Value(
                                results,
                                assemblyFilePath,
                                () => new AssemblySignatureStringsGenerationResult(assemblyFilePath));


                            // Try working with signature strings.
                            try
                            {
                                // Generate the signature string.
                                var signatureString = Instances.MemberInfoOperator.Get_SignatureString(memberInfo);

                                var pair = new SignatureStringIdentityPair
                                {
                                    IdentityString = identityString,
                                    SignatureString = signatureString,
                                };

                                // Structure the signature string.
                                var signature = Instances.SignatureStringOperator.Get_Signature(signatureString);

                                // Then generate an identity string from the structure.
                                IIdentityString identityStringFromStructuredSignatureString = Instances.SignatureOperator.Get_IdentityString(signature);

                                // Check the two identity strings for equality.
                                var areEqual = identityString.Equals(identityStringFromStructuredSignatureString);
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

                                // Add information to the identity string to get the signature string.
                                // (This is a good motivator for identitfying problematic identity strings.)
                            }
                            // If there are exceptions, accumulate the identity strings of members that cause exceptions.
                            catch (Exception)
                            {
                                result.Exceptions.Add(identityString);
                            }
                        });

                    Instances.Operator.Write_ResultsToOutput(
                        outputFilePath,
                        results);
                });

            Instances.NotepadPlusPlusOperator.Open(
                outputFilePath);
        }
    }
}
