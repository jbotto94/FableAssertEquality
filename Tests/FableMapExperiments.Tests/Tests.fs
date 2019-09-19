module Tests

open Library
open TestUtil
open System.Collections.Generic


let AllTests =  TestUtil.testList "AllTests" [
      testCase "Map.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = [1, "1"; 2,"2"; 3,"3"] |> Map.ofList
                   let m2 = [3, "3"; 4,"4"; 5,"5"] |> Map.ofList
                   let r1 = m1 |> Map.intersect m2
                   let r2 = m2 |> Map.intersect m1
                   printfn "m1: %A" m1 
                   printfn "m2: %A" m2 
                   printfn "r1: %A" r1 
                   printfn "r2: %A" r2
                   deepEqual r1 r2)]

#if FABLE_COMPILER

open Fable.Core
open Fable.Core.JsInterop

flattenTest AllTests

#else

open Expecto
open Expecto.TestResults

[<EntryPoint>]
    let main args =
        printfn "Result: %i" (runTestsWithArgs defaultConfig args AllTests)
        0


#endif