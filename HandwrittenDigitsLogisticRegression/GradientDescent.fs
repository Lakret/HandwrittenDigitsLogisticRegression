module GradientDescent

open System
open System.Linq

let gradientDescent epsilon learningRate maxIter h (x : float array array) (y : _ array) initialTheta =
    let rec loop (theta : _ array) prevMove currIter changes =
        let delta j = Array.sum [| for i in 0..(x.Length - 1) -> (h theta x.[i] - y.[i]) * (if j <> 0 then x.[i].[j - 1] else 1.) |]
        let n = theta.Length
        let newTheta = [| for j in 0..(n - 1) -> theta.[j] - (learningRate / (float n)) * (delta j) |]
        let move = Array.sum <| Array.map2 (fun x y -> Math.Sqrt <| (x - y) ** 2.) newTheta theta
        printfn "%i: %f" currIter move
        if (not <| (prevMove = 0.)) && (prevMove - move < epsilon || currIter >= maxIter) then 
            newTheta, List.rev changes
        else 
            loop newTheta move (currIter + 1) (move :: changes)
    loop initialTheta 0. 1 []