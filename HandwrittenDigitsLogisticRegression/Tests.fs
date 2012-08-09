module Tests

open System
open NUnit.Framework

open LogisticRegression
open Examples

type AssertFloat =
    static member AreEqual(expected : float, actual, epsilon) =
        Assert.LessOrEqual(Math.Abs(actual - expected), epsilon)

[<TestFixture>]
type HelpersTests() =
    let eps = 0.0000001

    [<Test>]
    member this.ReshapeTest() =
        Assert.AreEqual([| [|1..3|]; [|4..6|]; [|7..9|] |], reshape 3 3 [|1..9|])
        Assert.AreEqual([| [|1..5|]; [|6..10|] |], reshape 5 2 [|1..10|])

    [<Test>]
    member this.HypothesisTest() = 
        AssertFloat.AreEqual(0.9999546, h [|4.; 0.5; 3.; 1.|] [|2.; 1.; 2.|], eps)