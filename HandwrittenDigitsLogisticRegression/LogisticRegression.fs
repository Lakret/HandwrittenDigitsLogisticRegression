module LogisticRegression

#if INTERACTIVE 
//Math.Net Numerics
#r @"..\packages\MathNet.Numerics.2.1.2\lib\Net40\MathNet.Numerics.dll"
#r @"..\packages\MathNet.Numerics.FSharp.2.1.2\lib\Net40\MathNet.Numerics.FSharp.dll"
//PowerPack
#r @"..\packages\FSPowerPack.Community.2.1.1.1\Lib\Net40\FSharp.PowerPack.dll"
#r @"..\packages\FSPowerPack.Community.2.1.1.1\Lib\Net40\FSharp.PowerPack.Linq.dll"
#r @"..\packages\FSPowerPack.Community.2.1.1.1\Lib\Net40\FSharp.PowerPack.Metadata.dll"
#r @"..\packages\FSPowerPack.Community.2.1.1.1\Lib\Net40\FSharp.PowerPack.Parallel.Seq.dll"
//FSharpChart
#r @"..\packages\MSDN.FSharpChart.dll.0.60\lib\MSDN.FSharpChart.dll"
#load "FSharpChart.fsx"
//HtmlAgility
#r @"..\packages\HtmlAgilityPack.1.4.3\lib\HtmlAgilityPack.dll"
#r @"System.Xml"
#r @"System.Xml.Linq"

#r @"..\packages\NUnit.2.6.0.12054\lib\nunit.framework.dll"
#endif

open System
open System.Collections.Generic
open System.Linq
open System.Text
open System.IO
open System.Xml
open System.Xml.Linq

open MathNet.Numerics
open MathNet.Numerics.FSharp
open MathNet.Numerics.LinearAlgebra.Double
open MathNet.Numerics.LinearAlgebra.IO
open MathNet.Numerics.Distributions
open MSDN.FSharp.Charting  //FSharpChart
open HtmlAgilityPack       //HtmlAgility
open Microsoft.FSharp.Math //PowerPack

//мера ошибки для одного предсказания
let cost h y = -y * Math.Log(h) - (1. - y) * Math.Log(1. - h)

let sigmoid z = 1. / (1. + Math.Exp(-z)) //or simply SpecialFunctions.Logistic

//общая ошибка
let J h theta (dataset : _ array) (groundTruth : _ array) = 
    (-1. / (float dataset.Length)) * (Array.sum <| Array.map2 (fun x y -> cost (h theta x) y) dataset groundTruth)

//гипотеза — предсказывающая функция
let h (theta : float array) x = 
    theta.[0] + (Array.sum <| Array.map2 (*) theta.[1..] x)
    |> sigmoid


// float<logspace> for probs in logspace