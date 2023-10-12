using System;
using System.Collections.Generic;

using R5T.L0062.T000;
using R5T.T0172;


namespace R5T.S0105.N001
{
    /// <summary>
    /// For use in validating signature string generation - from member info to signature, then round-trip to/from signature string, then checking equality of the signature instances.
    /// </summary>
    public class AssemblySignatureStringsGenerationResult
    {
        public IAssemblyFilePath AssemblyFilePath { get; }
        public List<IIdentityString> Exceptions { get; } = new();
        public List<SignatureStringIdentityPair> Successes { get; } = new();
        public List<SignatureStringIdentityPair> Failures { get; } = new();


        public AssemblySignatureStringsGenerationResult(IAssemblyFilePath assemblyFilePath)
        {
            this.AssemblyFilePath = assemblyFilePath;
        }
    }
}
