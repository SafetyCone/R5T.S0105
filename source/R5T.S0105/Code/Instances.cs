using System;


namespace R5T.S0105
{
    public class Instances :
        L0055.Instances
    {
        public static L0053.IActionOperator ActionOperator => L0053.ActionOperator.Instance;
        public static L0053.IAssemblyOperator AssemblyOperator => L0053.AssemblyOperator.Instance;
        public static Z0000.ICharacters Characters => Z0000.Characters.Instance;
        public static L0053.IDictionaryOperator DictionaryOperator => L0053.DictionaryOperator.Instance;
        public static T0215.Z000.IDotnetPackNames DotnetPackNames => T0215.Z000.DotnetPackNames.Instance;
        public static L0053.IEnumerableOperator EnumerableOperator => L0053.EnumerableOperator.Instance;
        public static T0226.IExampleMemberOperator ExampleMemberOperator => T0226.ExampleMemberOperator.Instance;
        public static T0226.IExampleMembers ExampleMembers => T0226.ExampleMembers.Instance;
        public static T0226.IExampleMemberSets ExampleMemberSets => T0226.ExampleMemberSets.Instance;
        public static L0062.F001.IIdentityStringOperator IdentityStringOperator => L0062.F001.IdentityStringOperator.Instance;
        public static IMemberInfoOperator MemberInfoOperator => S0105.MemberInfoOperator.Instance;
        public static IOperator Operator => S0105.Operator.Instance;
        public static IOperations Operations => S0105.Operations.Instance;
        public static L0057.IReflectionOperator ReflectionOperator => L0057.ReflectionOperator.Instance;
        public static L0065.ISignatureOperator SignatureOperator => L0065.SignatureOperator.Instance;
        public static L0065.ISignatureStringOperator SignatureStringOperator => L0065.SignatureStringOperator.Instance;
        public static ISignatureStringOperator_Old SignatureStringOperator_Old => S0105.SignatureStringOperator_Old.Instance;
        public static L0063.Z001.Platform.Raw.ISignatureStrings SignatureStrings_Raw => L0063.Z001.Platform.Raw.SignatureStrings.Instance;
        public static L0053.IStringOperator StringOperator => L0053.StringOperator.Instance;
        public static Z0057.ITargetFrameworkMonikers TargetFrameworkMonikers => Z0057.TargetFrameworkMonikers.Instance;
        public static L0062.F001.ITokenSeparators TokenSeparators_IdentityStrings => L0062.F001.TokenSeparators.Instance;
        public static L0063.Z000.ITokenSeparators TokenSeparators_SignatureStrings => L0063.Z000.TokenSeparators.Instance;
        public static L0062.F001.ITypeNameAffixes TypeNameAffixes_IdentityStrings => L0062.F001.TypeNameAffixes.Instance;
        public static L0063.F001.ITypeNameAffixes TypeNameAffixes_SignatureStrings => L0063.F001.TypeNameAffixes.Instance;
    }
}