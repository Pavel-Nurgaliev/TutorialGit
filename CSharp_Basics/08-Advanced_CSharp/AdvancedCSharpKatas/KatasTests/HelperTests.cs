using System;
using System.Collections.Generic;
using System.Text;

namespace KatasTests
{
    internal class HelperTests
    {
        [TestCaseSource(typeof(HelperTestData), nameof(HelperTestData.IntArrayForMaxTesting))]
        public object TestArrayForMax_InputDataIntArray_MaxResult(int[] inputData)
        {
            return GenericsImplementations.Helper.Max<int>(inputData);
        }
        [TestCaseSource(typeof(HelperTestData), nameof(HelperTestData.StringArrayForMaxTesting))]
        public object TestArrayForMax_InputDataStringArray_MaxResult(string[] inputData)
        {
            return GenericsImplementations.Helper.Max<string>(inputData);
        }
        [TestCaseSource(typeof(HelperTestData), nameof(HelperTestData.SVArrayForMaxTesting))]
        public object TestArrayForMax_InputDataSVArray_MaxResult(SemanticVersion[] inputData)
        {
            return GenericsImplementations.Helper.Max<SemanticVersion>(inputData).Major;
        }
    }
}