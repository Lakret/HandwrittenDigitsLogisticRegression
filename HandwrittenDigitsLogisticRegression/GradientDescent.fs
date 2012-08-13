module GradientDescent

open System

let rec gradientDescent epsilon learningRate h (x : float array array) (y : _ array)
        (theta : _ array) prevMove =
    let delta j =
        Array.sum [| for i in 0..(x.Length - 1) -> 
                         (h theta x.[i] - y.[i]) * (if j <> 0 then x.[i].[j - 1] else 1.) |]
    let n = theta.Length
    let newTheta = [| for j in 0..(n - 1) -> theta.[j] - (learningRate / (float n)) * (delta j) |]
    let move = Array.sum <| Array.map2 (fun (x : float) y -> Math.Sqrt <| (x - y) ** 2.) newTheta theta
    printfn "%f" move
    if prevMove - move < epsilon then newTheta 
    else gradientDescent epsilon learningRate h x y newTheta move