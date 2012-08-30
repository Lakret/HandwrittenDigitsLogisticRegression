module Examples

open System
open System.Collections.Generic
open System.IO
open MSDN.FSharp.Charting
open System.Drawing
open System.Windows.Forms
open System.Windows.Forms.DataVisualization.Charting
open MathNet.Numerics.Statistics

open LogisticRegression
open GradientDescent
open Assessment

let (X, Y) = 
    File.ReadAllLines @"C:\Users\Slutsky\Desktop\semeion.data.txt"
    |> Array.map (fun line -> line.Split([|' '|], StringSplitOptions.RemoveEmptyEntries) 
                              |> Array.map float)
    |> Array.map (fun line -> line.[..255], (line.[256..] |> Array.findIndex ((=) 1.0)))
    |> Array.unzip

let reshape n m (line : _ array) = [| for j in 1..m -> line.[(j - 1) * n .. j * n - 1] |]

let visualizeInstanse (input : _ []) =
    let applyStyle chart =
        chart
        |> FSharpChart.WithArea.AxisX(Enabled = AxisEnabled.False)
        |> FSharpChart.WithArea.AxisY(Enabled = AxisEnabled.False)
        |> FSharpChart.WithSeries.Marker(Color = Color.Black, Size = 10, Style = MarkerStyle.Circle)

    let image = reshape 16 16 input
    [| for x in 1..16 do
           for y in 1..16 do
               if image.[y - 1].[x - 1] > 0.5 then yield (x, 16 - y) |]
    |> FSharpChart.Point
    |> applyStyle

visualizeInstanse <| X.[50]
printfn "Ground truth: %i" <| Y.[50]

FSharpChart.Line [| for h in 0.01 .. 0.01 .. 1. -> h, cost h 1.|]
FSharpChart.Line [| for h in 0. .. 0.01 .. 0.99 -> h, cost h 0.|]

FSharpChart.Line [| for z in -10. .. 0.1 .. 10. -> z, sigmoid z|]

let randGen = new Random()
let n = X.[0].Length
let trainedTheta, stepsCurve = gradientDescent 0.00005 0.1 100 h X (Array.map (fun y -> if y = 2 then 1. else 0.) Y) [| for j in 0..n -> randGen.NextDouble() - 0.5 |]
FSharpChart.Line (Array.ofList <| List.mapi (fun idx v -> float idx + 1., v) stepsCurve)

visualizeInstanse X.[50]
h trainedTheta X.[50] //0.97067883
visualizeInstanse X.[70]
h trainedTheta X.[70] //0.001859589623

printfn "The following information is based on test-set performance, therefore it is NOT representative (overly optimistic) and should NOT be trusted:"
assess h X Y 2 trainedTheta

let fs = new FileStream(@"C:\Users\Slutsky\Desktop\trainedParams", FileMode.Create)
let formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
formatter.Serialize(fs, trainedTheta)
fs.Close()

trainedTheta