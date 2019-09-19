module LibraryTests
open Testing
open Library
open System.Collections.Generic

let inline konst k = fun _ -> k

let EqualTests = 
    testList "Equal Tests" [
      testCase "IReadOnlyDictionary.union provides same end result as Dict.unionWith picking the first source value for dupes" 
        (fun () -> let m1 = Dictionary(dict [1, "2"; 2,"4"; 4,"8"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [1, "4"; 2,"8"; 4,"16"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.union m2 |> Seq.toList
                   let r2 = m1 |> IReadOnlyDictionary.unionWith konst m2 |> Seq.toList
                   equal r1 r2)
      testCase "IReadOnlyDictionary.intersect provides same end result as IReadOnlyDictionary.intersectWith picking the first source value for dupes" 
        (fun () -> let m1 = Dictionary(dict [1, "2"; 2,"4"; 4,"8"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [1, "4"; 2,"8"; 4,"16"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.intersect m2 |> Seq.toList
                   let r2 = m1 |> IReadOnlyDictionary.intersectWith konst m2 |> Seq.toList
                   equal r1 r2)
      testCase "IReadOnlyDictionary.union returns same results independent of the order (commutative)" 
        (fun () -> let m1 = Dictionary(dict [1, "1"; 2,"2"; 3,"3"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [3, "3"; 4,"4"; 5,"6"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.union m2 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   let r2 = m2 |> IReadOnlyDictionary.union m1 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   equal r1 r2)
      testCase "IReadOnlyDictionary.union returns same results independent of the groupping (associative)" 
        (fun () -> let m1 = Dictionary(dict [1, "1"; 2,"2"; 3,"3"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [3, "3"; 4,"4"; 5,"6"]) :> IReadOnlyDictionary<int, string>
                   let m3 = Dictionary(dict [6, "6"; 6,"6"; 8,"8"]) :> IReadOnlyDictionary<int, string>
                   let r1 = IReadOnlyDictionary.union (IReadOnlyDictionary.union m1 m2) m3 |> Seq.toList
                   let r2 = IReadOnlyDictionary.union m1 (IReadOnlyDictionary.union m2 m3) |> Seq.toList
                   equal r1 r2)
      testCase "IReadOnlyDictionary.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = Dictionary(dict [1, "1"; 2,"2"; 3,"3"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [3, "3"; 4,"4"; 5,"5"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.intersect m2 |> Seq.toList
                   let r2 = m2 |> IReadOnlyDictionary.intersect m1 |> Seq.toList
                   equal r1 r2)
      testCase "Dict.intersect provides same end result as Dict.intersectWith picking the first source value for dupes" 
        (fun () -> let m1 = dict [1, "2"; 2,"4"; 4,"8"]
                   let m2 = dict [1, "4"; 2,"8"; 4,"16"]
                   let r1 = m1 |> Dict.intersect m2 |> Seq.toList
                   let r2 = m1 |> Dict.intersectWith konst m2 |> Seq.toList
                   equal r1 r2)
      testCase "Dict.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = dict [1, "1"; 2,"2"; 3,"3"]
                   let m2 = dict [3, "3"; 4,"4"; 5,"5"]
                   let r1 = m1 |> Dict.intersect m2 |> Seq.toList
                   let r2 = m2 |> Dict.intersect m1 |> Seq.toList
                   equal r1 r2)
      testCase "Map.intersect provides same end result as Map.intersectWith picking the first source value for dupes" 
        (fun () -> let m1 = [1, "2"; 2,"4"; 4,"8"] |> Map.ofList
                   let m2 = [1, "4"; 2,"8"; 4,"16"] |> Map.ofList
                   let r1 = m1 |> Map.intersect m2
                   let r2 = m1 |> Map.intersectWith konst m2
                   equal r1 r2)
      testCase "Dict.union provides same end result as Dict.unionWith picking the first source value for dupes" 
        (fun () -> let m1 = dict [1, "2"; 2,"4"; 4,"8"]
                   let m2 = dict [1, "4"; 2,"8"; 4,"16"]
                   let r1 = m1 |> Dict.union m2 |> Seq.toList
                   let r2 = m1 |> Dict.unionWith konst m2 |> Seq.toList
                   equal r1 r2)
      testCase "Dict.union returns same results independent of the grouping (associative)" 
        (fun () -> let m1 = dict [1, "1"; 2,"2"; 3,"3"]
                   let m2 = dict [3, "3"; 4,"4"; 5,"6"]
                   let m3 = dict [6, "6"; 6,"6"; 8,"8"]
                   let r1 = Dict.union (Dict.union m1 m2) m3 |> Seq.toList
                   let r2 = Dict.union m1 (Dict.union m2 m3) |> Seq.toList
                   equal r1 r2)
      testCase "Dict.union returns same results independent of the order (commutative)" 
        (fun () -> let m1 = dict [1, "1"; 2,"2"; 3,"3"]
                   let m2 = dict [3, "3"; 4,"4"; 5,"6"]
                   let r1 = m1 |> Dict.union m2 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   let r2 = m2 |> Dict.union m1 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   equal r1 r2)
      testCase "Map.union returns same results independent of the order (associative)" 
        (fun () -> let m1 = [1, "1"; 2,"2"; 3,"3"] |> Map.ofList
                   let m2 = [3, "3"; 4,"4"; 5,"6"] |> Map.ofList
                   let r1 = m1 |> Map.union m2
                   let r2 = m2 |> Map.union m1
                   equal r1 r2)
      testCase "Map.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = [1, "1"; 2,"2"; 3,"3"] |> Map.ofList
                   let m2 = [3, "3"; 4,"4"; 5,"5"] |> Map.ofList
                   let r1 = m1 |> Map.intersect m2
                   let r2 = m2 |> Map.intersect m1
                   equal r1 r2)
      testCase "Map.intersect returns any dictionary when intersected with the empty map (identity)" 
        (fun () -> let m1 = [1, "2"; 2,"4"; 4,"8"] |> Map.ofList 
                   let m2 = Map.empty
                   let r1 = m1 |> Map.intersect m2
                   equal Map.empty r1)]


let fsEqTests = 
    testList "fsEq Tests" [
      testCase "IReadOnlyDictionary.union provides same end result as Dict.unionWith picking the first source value for dupes" 
        (fun () -> let m1 = Dictionary(dict [1, "2"; 2,"4"; 4,"8"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [1, "4"; 2,"8"; 4,"16"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.union m2 |> Seq.toList
                   let r2 = m1 |> IReadOnlyDictionary.unionWith konst m2 |> Seq.toList
                   fsEq r1 r2)
      testCase "IReadOnlyDictionary.intersect provides same end result as IReadOnlyDictionary.intersectWith picking the first source value for dupes" 
        (fun () -> let m1 = Dictionary(dict [1, "2"; 2,"4"; 4,"8"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [1, "4"; 2,"8"; 4,"16"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.intersect m2 |> Seq.toList
                   let r2 = m1 |> IReadOnlyDictionary.intersectWith konst m2 |> Seq.toList
                   fsEq r1 r2)
      testCase "IReadOnlyDictionary.union returns same results independent of the order (commutative)" 
        (fun () -> let m1 = Dictionary(dict [1, "1"; 2,"2"; 3,"3"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [3, "3"; 4,"4"; 5,"6"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.union m2 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   let r2 = m2 |> IReadOnlyDictionary.union m1 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   fsEq r1 r2)
      testCase "IReadOnlyDictionary.union returns same results independent of the groupping (associative)" 
        (fun () -> let m1 = Dictionary(dict [1, "1"; 2,"2"; 3,"3"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [3, "3"; 4,"4"; 5,"6"]) :> IReadOnlyDictionary<int, string>
                   let m3 = Dictionary(dict [6, "6"; 6,"6"; 8,"8"]) :> IReadOnlyDictionary<int, string>
                   let r1 = IReadOnlyDictionary.union (IReadOnlyDictionary.union m1 m2) m3 |> Seq.toList
                   let r2 = IReadOnlyDictionary.union m1 (IReadOnlyDictionary.union m2 m3) |> Seq.toList
                   fsEq r1 r2)
      testCase "IReadOnlyDictionary.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = Dictionary(dict [1, "1"; 2,"2"; 3,"3"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [3, "3"; 4,"4"; 5,"5"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.intersect m2 |> Seq.toList
                   let r2 = m2 |> IReadOnlyDictionary.intersect m1 |> Seq.toList
                   fsEq r1 r2)
      testCase "Dict.intersect provides same end result as Dict.intersectWith picking the first source value for dupes" 
        (fun () -> let m1 = dict [1, "2"; 2,"4"; 4,"8"]
                   let m2 = dict [1, "4"; 2,"8"; 4,"16"]
                   let r1 = m1 |> Dict.intersect m2 |> Seq.toList
                   let r2 = m1 |> Dict.intersectWith konst m2 |> Seq.toList
                   fsEq r1 r2)
      testCase "Dict.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = dict [1, "1"; 2,"2"; 3,"3"]
                   let m2 = dict [3, "3"; 4,"4"; 5,"5"]
                   let r1 = m1 |> Dict.intersect m2 |> Seq.toList
                   let r2 = m2 |> Dict.intersect m1 |> Seq.toList
                   fsEq r1 r2)
      testCase "Map.intersect provides same end result as Map.intersectWith picking the first source value for dupes" 
        (fun () -> let m1 = [1, "2"; 2,"4"; 4,"8"] |> Map.ofList
                   let m2 = [1, "4"; 2,"8"; 4,"16"] |> Map.ofList
                   let r1 = m1 |> Map.intersect m2
                   let r2 = m1 |> Map.intersectWith konst m2
                   fsEq r1 r2)
      testCase "Dict.union provides same end result as Dict.unionWith picking the first source value for dupes" 
        (fun () -> let m1 = dict [1, "2"; 2,"4"; 4,"8"]
                   let m2 = dict [1, "4"; 2,"8"; 4,"16"]
                   let r1 = m1 |> Dict.union m2 |> Seq.toList
                   let r2 = m1 |> Dict.unionWith konst m2 |> Seq.toList
                   fsEq r1 r2)
      testCase "Dict.union returns same results independent of the grouping (associative)" 
        (fun () -> let m1 = dict [1, "1"; 2,"2"; 3,"3"]
                   let m2 = dict [3, "3"; 4,"4"; 5,"6"]
                   let m3 = dict [6, "6"; 6,"6"; 8,"8"]
                   let r1 = Dict.union (Dict.union m1 m2) m3 |> Seq.toList
                   let r2 = Dict.union m1 (Dict.union m2 m3) |> Seq.toList
                   fsEq r1 r2)
      testCase "Dict.union returns same results independent of the order (commutative)" 
        (fun () -> let m1 = dict [1, "1"; 2,"2"; 3,"3"]
                   let m2 = dict [3, "3"; 4,"4"; 5,"6"]
                   let r1 = m1 |> Dict.union m2 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   let r2 = m2 |> Dict.union m1 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   fsEq r1 r2)
      testCase "Map.union returns same results independent of the order (associative)" 
        (fun () -> let m1 = [1, "1"; 2,"2"; 3,"3"] |> Map.ofList
                   let m2 = [3, "3"; 4,"4"; 5,"6"] |> Map.ofList
                   let r1 = m1 |> Map.union m2
                   let r2 = m2 |> Map.union m1
                   fsEq r1 r2)
      testCase "Map.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = [1, "1"; 2,"2"; 3,"3"] |> Map.ofList
                   let m2 = [3, "3"; 4,"4"; 5,"5"] |> Map.ofList
                   let r1 = m1 |> Map.intersect m2
                   let r2 = m2 |> Map.intersect m1
                   fsEq r1 r2)
      testCase "Map.intersect returns any dictionary when intersected with the empty map (identity)" 
        (fun () -> let m1 = [1, "2"; 2,"4"; 4,"8"] |> Map.ofList 
                   let m2 = Map.empty
                   let r1 = m1 |> Map.intersect m2
                   fsEq Map.empty r1)]



let deepEqualTests = 
    testList "deepEqual Tests" [
      testCase "IReadOnlyDictionary.union provides same end result as Dict.unionWith picking the first source value for dupes" 
        (fun () -> let m1 = Dictionary(dict [1, "2"; 2,"4"; 4,"8"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [1, "4"; 2,"8"; 4,"16"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.union m2 |> Seq.toList
                   let r2 = m1 |> IReadOnlyDictionary.unionWith konst m2 |> Seq.toList
                   deepEqual r1 r2)
      testCase "IReadOnlyDictionary.intersect provides same end result as IReadOnlyDictionary.intersectWith picking the first source value for dupes" 
        (fun () -> let m1 = Dictionary(dict [1, "2"; 2,"4"; 4,"8"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [1, "4"; 2,"8"; 4,"16"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.intersect m2 |> Seq.toList
                   let r2 = m1 |> IReadOnlyDictionary.intersectWith konst m2 |> Seq.toList
                   deepEqual r1 r2)
      testCase "IReadOnlyDictionary.union returns same results independent of the order (commutative)" 
        (fun () -> let m1 = Dictionary(dict [1, "1"; 2,"2"; 3,"3"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [3, "3"; 4,"4"; 5,"6"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.union m2 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   let r2 = m2 |> IReadOnlyDictionary.union m1 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   deepEqual r1 r2)
      testCase "IReadOnlyDictionary.union returns same results independent of the groupping (associative)" 
        (fun () -> let m1 = Dictionary(dict [1, "1"; 2,"2"; 3,"3"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [3, "3"; 4,"4"; 5,"6"]) :> IReadOnlyDictionary<int, string>
                   let m3 = Dictionary(dict [6, "6"; 6,"6"; 8,"8"]) :> IReadOnlyDictionary<int, string>
                   let r1 = IReadOnlyDictionary.union (IReadOnlyDictionary.union m1 m2) m3 |> Seq.toList
                   let r2 = IReadOnlyDictionary.union m1 (IReadOnlyDictionary.union m2 m3) |> Seq.toList
                   deepEqual r1 r2)
      testCase "IReadOnlyDictionary.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = Dictionary(dict [1, "1"; 2,"2"; 3,"3"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [3, "3"; 4,"4"; 5,"5"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.intersect m2 |> Seq.toList
                   let r2 = m2 |> IReadOnlyDictionary.intersect m1 |> Seq.toList
                   deepEqual r1 r2)
      testCase "Dict.intersect provides same end result as Dict.intersectWith picking the first source value for dupes" 
        (fun () -> let m1 = dict [1, "2"; 2,"4"; 4,"8"]
                   let m2 = dict [1, "4"; 2,"8"; 4,"16"]
                   let r1 = m1 |> Dict.intersect m2 |> Seq.toList
                   let r2 = m1 |> Dict.intersectWith konst m2 |> Seq.toList
                   deepEqual r1 r2)
      testCase "Dict.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = dict [1, "1"; 2,"2"; 3,"3"]
                   let m2 = dict [3, "3"; 4,"4"; 5,"5"]
                   let r1 = m1 |> Dict.intersect m2 |> Seq.toList
                   let r2 = m2 |> Dict.intersect m1 |> Seq.toList
                   deepEqual r1 r2)
      testCase "Map.intersect provides same end result as Map.intersectWith picking the first source value for dupes" 
        (fun () -> let m1 = [1, "2"; 2,"4"; 4,"8"] |> Map.ofList
                   let m2 = [1, "4"; 2,"8"; 4,"16"] |> Map.ofList
                   let r1 = m1 |> Map.intersect m2
                   let r2 = m1 |> Map.intersectWith konst m2
                   deepEqual r1 r2)
      testCase "Dict.union provides same end result as Dict.unionWith picking the first source value for dupes" 
        (fun () -> let m1 = dict [1, "2"; 2,"4"; 4,"8"]
                   let m2 = dict [1, "4"; 2,"8"; 4,"16"]
                   let r1 = m1 |> Dict.union m2 |> Seq.toList
                   let r2 = m1 |> Dict.unionWith konst m2 |> Seq.toList
                   deepEqual r1 r2)
      testCase "Dict.union returns same results independent of the grouping (associative)" 
        (fun () -> let m1 = dict [1, "1"; 2,"2"; 3,"3"]
                   let m2 = dict [3, "3"; 4,"4"; 5,"6"]
                   let m3 = dict [6, "6"; 6,"6"; 8,"8"]
                   let r1 = Dict.union (Dict.union m1 m2) m3 |> Seq.toList
                   let r2 = Dict.union m1 (Dict.union m2 m3) |> Seq.toList
                   deepEqual r1 r2)
      testCase "Dict.union returns same results independent of the order (commutative)" 
        (fun () -> let m1 = dict [1, "1"; 2,"2"; 3,"3"]
                   let m2 = dict [3, "3"; 4,"4"; 5,"6"]
                   let r1 = m1 |> Dict.union m2 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   let r2 = m2 |> Dict.union m1 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   deepEqual r1 r2)
      testCase "Map.union returns same results independent of the order (associative)" 
        (fun () -> let m1 = [1, "1"; 2,"2"; 3,"3"] |> Map.ofList
                   let m2 = [3, "3"; 4,"4"; 5,"6"] |> Map.ofList
                   let r1 = m1 |> Map.union m2
                   let r2 = m2 |> Map.union m1
                   deepEqual r1 r2)
      testCase "Map.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = [1, "1"; 2,"2"; 3,"3"] |> Map.ofList
                   let m2 = [3, "3"; 4,"4"; 5,"5"] |> Map.ofList
                   let r1 = m1 |> Map.intersect m2
                   let r2 = m2 |> Map.intersect m1
                   deepEqual r1 r2)
      testCase "Map.intersect returns any dictionary when intersected with the empty map (identity)" 
        (fun () -> let m1 = [1, "2"; 2,"4"; 4,"8"] |> Map.ofList 
                   let m2 = Map.empty
                   let r1 = m1 |> Map.intersect m2
                   deepEqual Map.empty r1)]


let equalObjTests = 
    testList "equalObj Tests" [
      testCase "IReadOnlyDictionary.union provides same end result as Dict.unionWith picking the first source value for dupes" 
        (fun () -> let m1 = Dictionary(dict [1, "2"; 2,"4"; 4,"8"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [1, "4"; 2,"8"; 4,"16"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.union m2 |> Seq.toList
                   let r2 = m1 |> IReadOnlyDictionary.unionWith konst m2 |> Seq.toList
                   equalObj r1 r2)
      testCase "IReadOnlyDictionary.intersect provides same end result as IReadOnlyDictionary.intersectWith picking the first source value for dupes" 
        (fun () -> let m1 = Dictionary(dict [1, "2"; 2,"4"; 4,"8"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [1, "4"; 2,"8"; 4,"16"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.intersect m2 |> Seq.toList
                   let r2 = m1 |> IReadOnlyDictionary.intersectWith konst m2 |> Seq.toList
                   equalObj r1 r2)
      testCase "IReadOnlyDictionary.union returns same results independent of the order (commutative)" 
        (fun () -> let m1 = Dictionary(dict [1, "1"; 2,"2"; 3,"3"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [3, "3"; 4,"4"; 5,"6"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.union m2 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   let r2 = m2 |> IReadOnlyDictionary.union m1 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   equalObj r1 r2)
      testCase "IReadOnlyDictionary.union returns same results independent of the groupping (associative)" 
        (fun () -> let m1 = Dictionary(dict [1, "1"; 2,"2"; 3,"3"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [3, "3"; 4,"4"; 5,"6"]) :> IReadOnlyDictionary<int, string>
                   let m3 = Dictionary(dict [6, "6"; 6,"6"; 8,"8"]) :> IReadOnlyDictionary<int, string>
                   let r1 = IReadOnlyDictionary.union (IReadOnlyDictionary.union m1 m2) m3 |> Seq.toList
                   let r2 = IReadOnlyDictionary.union m1 (IReadOnlyDictionary.union m2 m3) |> Seq.toList
                   equalObj r1 r2)
      testCase "IReadOnlyDictionary.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = Dictionary(dict [1, "1"; 2,"2"; 3,"3"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [3, "3"; 4,"4"; 5,"5"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.intersect m2 |> Seq.toList
                   let r2 = m2 |> IReadOnlyDictionary.intersect m1 |> Seq.toList
                   equalObj r1 r2)
      testCase "Dict.intersect provides same end result as Dict.intersectWith picking the first source value for dupes" 
        (fun () -> let m1 = dict [1, "2"; 2,"4"; 4,"8"]
                   let m2 = dict [1, "4"; 2,"8"; 4,"16"]
                   let r1 = m1 |> Dict.intersect m2 |> Seq.toList
                   let r2 = m1 |> Dict.intersectWith konst m2 |> Seq.toList
                   equalObj r1 r2)
      testCase "Dict.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = dict [1, "1"; 2,"2"; 3,"3"]
                   let m2 = dict [3, "3"; 4,"4"; 5,"5"]
                   let r1 = m1 |> Dict.intersect m2 |> Seq.toList
                   let r2 = m2 |> Dict.intersect m1 |> Seq.toList
                   equalObj r1 r2)
      testCase "Map.intersect provides same end result as Map.intersectWith picking the first source value for dupes" 
        (fun () -> let m1 = [1, "2"; 2,"4"; 4,"8"] |> Map.ofList
                   let m2 = [1, "4"; 2,"8"; 4,"16"] |> Map.ofList
                   let r1 = m1 |> Map.intersect m2
                   let r2 = m1 |> Map.intersectWith konst m2
                   equalObj r1 r2)
      testCase "Dict.union provides same end result as Dict.unionWith picking the first source value for dupes" 
        (fun () -> let m1 = dict [1, "2"; 2,"4"; 4,"8"]
                   let m2 = dict [1, "4"; 2,"8"; 4,"16"]
                   let r1 = m1 |> Dict.union m2 |> Seq.toList
                   let r2 = m1 |> Dict.unionWith konst m2 |> Seq.toList
                   equalObj r1 r2)
      testCase "Dict.union returns same results independent of the grouping (associative)" 
        (fun () -> let m1 = dict [1, "1"; 2,"2"; 3,"3"]
                   let m2 = dict [3, "3"; 4,"4"; 5,"6"]
                   let m3 = dict [6, "6"; 6,"6"; 8,"8"]
                   let r1 = Dict.union (Dict.union m1 m2) m3 |> Seq.toList
                   let r2 = Dict.union m1 (Dict.union m2 m3) |> Seq.toList
                   equalObj r1 r2)
      testCase "Dict.union returns same results independent of the order (commutative)" 
        (fun () -> let m1 = dict [1, "1"; 2,"2"; 3,"3"]
                   let m2 = dict [3, "3"; 4,"4"; 5,"6"]
                   let r1 = m1 |> Dict.union m2 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   let r2 = m2 |> Dict.union m1 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   equalObj r1 r2)
      testCase "Map.union returns same results independent of the order (associative)" 
        (fun () -> let m1 = [1, "1"; 2,"2"; 3,"3"] |> Map.ofList
                   let m2 = [3, "3"; 4,"4"; 5,"6"] |> Map.ofList
                   let r1 = m1 |> Map.union m2
                   let r2 = m2 |> Map.union m1
                   equalObj r1 r2)
      testCase "Map.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = [1, "1"; 2,"2"; 3,"3"] |> Map.ofList
                   let m2 = [3, "3"; 4,"4"; 5,"5"] |> Map.ofList
                   let r1 = m1 |> Map.intersect m2
                   let r2 = m2 |> Map.intersect m1
                   equalObj r1 r2)
      testCase "Map.intersect returns any dictionary when intersected with the empty map (identity)" 
        (fun () -> let m1 = [1, "2"; 2,"4"; 4,"8"] |> Map.ofList 
                   let m2 = Map.empty
                   let r1 = m1 |> Map.intersect m2
                   equalObj Map.empty r1)]

let nEqualTests = 
    testList "nEqual Tests" [
      testCase "IReadOnlyDictionary.union provides same end result as Dict.unionWith picking the first source value for dupes" 
        (fun () -> let m1 = Dictionary(dict [1, "2"; 2,"4"; 4,"8"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [1, "4"; 2,"8"; 4,"16"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.union m2 |> Seq.toList
                   let r2 = m1 |> IReadOnlyDictionary.unionWith konst m2 |> Seq.toList
                   nEqual r1 r2)
      testCase "IReadOnlyDictionary.intersect provides same end result as IReadOnlyDictionary.intersectWith picking the first source value for dupes" 
        (fun () -> let m1 = Dictionary(dict [1, "2"; 2,"4"; 4,"8"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [1, "4"; 2,"8"; 4,"16"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.intersect m2 |> Seq.toList
                   let r2 = m1 |> IReadOnlyDictionary.intersectWith konst m2 |> Seq.toList
                   nEqual r1 r2)
      testCase "IReadOnlyDictionary.union returns same results independent of the order (commutative)" 
        (fun () -> let m1 = Dictionary(dict [1, "1"; 2,"2"; 3,"3"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [3, "3"; 4,"4"; 5,"6"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.union m2 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   let r2 = m2 |> IReadOnlyDictionary.union m1 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   nEqual r1 r2)
      testCase "IReadOnlyDictionary.union returns same results independent of the groupping (associative)" 
        (fun () -> let m1 = Dictionary(dict [1, "1"; 2,"2"; 3,"3"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [3, "3"; 4,"4"; 5,"6"]) :> IReadOnlyDictionary<int, string>
                   let m3 = Dictionary(dict [6, "6"; 6,"6"; 8,"8"]) :> IReadOnlyDictionary<int, string>
                   let r1 = IReadOnlyDictionary.union (IReadOnlyDictionary.union m1 m2) m3 |> Seq.toList
                   let r2 = IReadOnlyDictionary.union m1 (IReadOnlyDictionary.union m2 m3) |> Seq.toList
                   nEqual r1 r2)
      testCase "IReadOnlyDictionary.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = Dictionary(dict [1, "1"; 2,"2"; 3,"3"]) :> IReadOnlyDictionary<int, string>
                   let m2 = Dictionary(dict [3, "3"; 4,"4"; 5,"5"]) :> IReadOnlyDictionary<int, string>
                   let r1 = m1 |> IReadOnlyDictionary.intersect m2 |> Seq.toList
                   let r2 = m2 |> IReadOnlyDictionary.intersect m1 |> Seq.toList
                   nEqual r1 r2)
      testCase "Dict.intersect provides same end result as Dict.intersectWith picking the first source value for dupes" 
        (fun () -> let m1 = dict [1, "2"; 2,"4"; 4,"8"]
                   let m2 = dict [1, "4"; 2,"8"; 4,"16"]
                   let r1 = m1 |> Dict.intersect m2 |> Seq.toList
                   let r2 = m1 |> Dict.intersectWith konst m2 |> Seq.toList
                   nEqual r1 r2)
      testCase "Dict.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = dict [1, "1"; 2,"2"; 3,"3"]
                   let m2 = dict [3, "3"; 4,"4"; 5,"5"]
                   let r1 = m1 |> Dict.intersect m2 |> Seq.toList
                   let r2 = m2 |> Dict.intersect m1 |> Seq.toList
                   nEqual r1 r2)
      testCase "Map.intersect provides same end result as Map.intersectWith picking the first source value for dupes" 
        (fun () -> let m1 = [1, "2"; 2,"4"; 4,"8"] |> Map.ofList
                   let m2 = [1, "4"; 2,"8"; 4,"16"] |> Map.ofList
                   let r1 = m1 |> Map.intersect m2
                   let r2 = m1 |> Map.intersectWith konst m2
                   nEqual r1 r2)
      testCase "Dict.union provides same end result as Dict.unionWith picking the first source value for dupes" 
        (fun () -> let m1 = dict [1, "2"; 2,"4"; 4,"8"]
                   let m2 = dict [1, "4"; 2,"8"; 4,"16"]
                   let r1 = m1 |> Dict.union m2 |> Seq.toList
                   let r2 = m1 |> Dict.unionWith konst m2 |> Seq.toList
                   nEqual r1 r2)
      testCase "Dict.union returns same results independent of the grouping (associative)" 
        (fun () -> let m1 = dict [1, "1"; 2,"2"; 3,"3"]
                   let m2 = dict [3, "3"; 4,"4"; 5,"6"]
                   let m3 = dict [6, "6"; 6,"6"; 8,"8"]
                   let r1 = Dict.union (Dict.union m1 m2) m3 |> Seq.toList
                   let r2 = Dict.union m1 (Dict.union m2 m3) |> Seq.toList
                   nEqual r1 r2)
      testCase "Dict.union returns same results independent of the order (commutative)" 
        (fun () -> let m1 = dict [1, "1"; 2,"2"; 3,"3"]
                   let m2 = dict [3, "3"; 4,"4"; 5,"6"]
                   let r1 = m1 |> Dict.union m2 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   let r2 = m2 |> Dict.union m1 |> Seq.toList |> List.sortBy (fun x -> x.Key)
                   nEqual r1 r2)
      testCase "Map.union returns same results independent of the order (associative)" 
        (fun () -> let m1 = [1, "1"; 2,"2"; 3,"3"] |> Map.ofList
                   let m2 = [3, "3"; 4,"4"; 5,"6"] |> Map.ofList
                   let r1 = m1 |> Map.union m2
                   let r2 = m2 |> Map.union m1
                   nEqual r1 r2)
      testCase "Map.intersect returns same results independent of the order (commutative)" 
        (fun () -> let m1 = [1, "1"; 2,"2"; 3,"3"] |> Map.ofList
                   let m2 = [3, "3"; 4,"4"; 5,"5"] |> Map.ofList
                   let r1 = m1 |> Map.intersect m2
                   let r2 = m2 |> Map.intersect m1
                   nEqual r1 r2)
      testCase "Map.intersect returns any dictionary when intersected with the empty map (identity)" 
        (fun () -> let m1 = [1, "2"; 2,"4"; 4,"8"] |> Map.ofList 
                   let m2 = Map.empty
                   let r1 = m1 |> Map.intersect m2
                   nEqual (Map.toSeq Map.empty) (Map.toSeq r1))

        ]
