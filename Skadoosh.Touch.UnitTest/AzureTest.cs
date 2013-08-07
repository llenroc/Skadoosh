using System;
using NUnit.Framework;
using Skadoosh.Common.ViewModels;
using Microsoft.WindowsAzure.MobileServices;

namespace Skadoosh.Touch.UnitTest
{
	[TestFixture]
	public class AzureTest
	{
		[Test]
		public void Pass ()
		{
           
         
			var skadoosh = new SkadooshVM ();
            skadoosh.UnitTestTableInitialize();
			Assert.True (true);
		}

		[Test]
		public void Fail ()
		{
			Assert.False (true);
		}

		[Test]
		[Ignore ("another time")]
		public void Ignore ()
		{
			Assert.True (false);
		}
	}
}
