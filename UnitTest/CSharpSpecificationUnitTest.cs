//-----------------------------------------------------------------------
// <copyright file="CSharpSpecificationUnitTest.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// C#の仕様に関する挙動確認用テストクラスです。
    /// </summary>
    [TestClass]
    public class CSharpSpecificationUnitTest
    {
        /// <summary>
        /// <see cref="VariableExpansion_FormatTest(string, string)"/>のテストデータを取得します。
        /// </summary>
        /// <returns>テストケースを返します。</returns>
        public static IEnumerable<object[]> GetDataForVariableExpansion_FormatTest()
        {
            yield return new object[] { "円周率：3.14", $"円周率：{Math.PI:F2}" };
            yield return new object[] { "円周率：3.142", $"円周率：{Math.PI:F3}" };
            yield return new object[] { "円周率：3.1415926536", $"円周率：{Math.PI:F10}" };
        }

        /// <summary>
        /// 変数展開にて一般的な変数展開とエスケープの挙動確認です。
        /// </summary>
        [TestMethod]
        public void VariableExpansion_NormalAndEscape()
        {
            var testParam = "hoge";
            Assert.AreEqual("変数の内容は「hoge」です。{変数展開の挙動確認}", $"変数の内容は「{testParam}」です。{{変数展開の挙動確認}}");
        }

        /// <summary>
        /// 変数展開のフォーマットに関するテストを行います。
        /// </summary>
        /// <param name="expected"> 期待値です。 </param>
        /// <param name="actual"> 実際の値です。 </param>
        [DataTestMethod]
        [DynamicData(nameof(GetDataForVariableExpansion_FormatTest), DynamicDataSourceType.Method)]
        public void VariableExpansion_FormatTest(string expected, string actual)
        {
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// フロートのパース確認です。
        /// </summary>
        [TestMethod]
        public void ParseFloat()
        {
            Assert.AreEqual(-0.00341018f, float.Parse("-3.4101800e-003"));
        }
    }
}
