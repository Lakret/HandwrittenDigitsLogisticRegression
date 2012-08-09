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

let (X, Y) = 
    File.ReadAllLines @"C:\Users\Lakret\Desktop\semeion.data.txt"
    |> Array.map (fun line -> line.Split([|' '|], StringSplitOptions.RemoveEmptyEntries) 
                              |> Array.map float)
    |> Array.map (fun line -> line.[..255], (line.[256..] |> Array.findIndex ((=) 1.0)))
    |> Array.unzip

let reshape x y (line : _ array) = [| for j in 1..y -> line.[(j - 1) * x .. j * x - 1] |]

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
let randomParams = [| for i in 0..256 -> (randGen.NextDouble() - 0.5) * 3. |]
let meaninglessPredictions = Array.map (h randomParams) X

FSharpChart.Line [| for i in 0..255 -> i, meaninglessPredictions.[i] |]

Statistics.Mean meaninglessPredictions
Statistics.StandardDeviation meaninglessPredictions