using TechTalk.SpecFlow;
using System;
using NUnit.Framework;
 
namespace DotnetcoreTutorialSteps
{
 
  [Binding]
  public static class ExampleSteps
  {
      [Given(@"precondition")]
      public static void GivenPrecondition(){
          Console.WriteLine("Given Some Condition");
      }
 
      [When(@"action")]
      public static void WhenAction(){
          Console.WriteLine("When Some conditions");
      }
 
     [Then(@"testable outcome")]
      public static void ThenTestableOutcome(){
          Console.WriteLine("Then some outcome");
          Assert.IsTrue(true,"expected true but found false");
      }
  }
}