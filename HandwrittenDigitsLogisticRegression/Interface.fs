namespace Interface

open LogisticRegression

open System
open System.IO

type RecognitionProvider(paramsFile : string) =
    let theta =
        (fun () -> let fs = new FileStream(paramsFile, FileMode.Open)
                   let formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
                   let theta = formatter.Deserialize(fs) :?> float array
                   fs.Close()
                   theta) ()
    
    member this.Theta = theta.Clone()
    member this.IsTwo(input : float array) = h theta input >= 0.5