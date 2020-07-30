using TechTalk.SpecFlow;
using System;
using NUnit.Framework;
 
namespace DotnetcoreTutorial.src.steps
{
 
  [Binding]
  public class ExampleSteps
  {
      [Given(@"precondition")]
      public void GivenPrecondition(){
          Console.WriteLine("Given Some Condition");
      }
 
      [When(@"action")]
      public void WhenAction(){
          Console.WriteLine("When Some conditions");
      }
 
     [Then(@"testable outcome")]
      public void ThenTestableOutcome(){
          Console.WriteLine("Then some outcome");
          Assert.IsTrue(true,"expected true but fund false");
      }
  }
}