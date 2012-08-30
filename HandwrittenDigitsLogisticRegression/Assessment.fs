module Assessment

let assess h X Y answer trainedTheta =
    //tp = true positives, fp = false positives, fn = false negatives
    let right, wrong, tp, fp, fn =
        Array.fold2 
            (fun (right, wrong, tp, fp, fn) x y ->
                 let prediction = h trainedTheta x
                 if prediction >= 0.5 && y = answer then 
                    (right + 1, wrong, tp + 1, fp, fn)
                 elif prediction >= 0.5 && y <> answer then
                    (right, wrong + 1, tp, fp + 1, fn)
                 elif prediction <= 0.5 && y = answer then
                    (right, wrong + 1, tp, fp, fn + 1)
                 else (right + 1, wrong, tp, fp, fn))
            (0, 0, 0, 0, 0) X Y
    let precision = (float tp) / (float <| tp + fp)
    let recall = (float tp) / (float <| tp + fn)
    printfn "%i right (%f%%) and %i wrong answers were given. Precision = %f, recall = %f" right (float right / (float <| right + wrong)) wrong precision recall