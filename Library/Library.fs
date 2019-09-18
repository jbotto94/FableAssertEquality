namespace Library

open System
open System.Collections.ObjectModel
open System.Collections.Generic

module Map =
    let mapValues2 f (x: Map<'Key, 'T1>) (y: Map<'Key, 'T2>) = Map <| seq {
        let f = OptimizedClosures.FSharpFunc<_,_,_>.Adapt f
        for KeyValue(k, vx) in x do
            match Map.tryFind k y with
            | Some vy -> yield (k, f.Invoke (vx, vy))
            | None    -> () }

    let unionWith combiner (source1: Map<'Key, 'Value>) (source2: Map<'Key, 'Value>) =
        Map.fold (fun m k v' -> Map.add k (match Map.tryFind k m with Some v -> combiner v v' | None -> v') m) source1 source2

    let union (source: Map<'Key, 'T>) (altSource: Map<'Key, 'T>) = unionWith (fun x _ -> x) source altSource

    let intersectWith combiner (source1:Map<'Key, 'T>) (source2:Map<'Key, 'T>) =
        mapValues2 combiner source1 source2

    let intersect (source1:Map<'Key, 'T>) (source2:Map<'Key, 'T>) = 
        intersectWith (fun a _ -> a) source1 source2

module Dict =
    let tryGetValue k (dct: IDictionary<'Key, 'Value>) =
        match dct.TryGetValue k with
        | true, v -> Some v
        | _       -> None

    let map2 f (x: IDictionary<'Key, 'T1>) (y: IDictionary<'Key, 'T2>) =
        let dct = Dictionary<'Key, 'U> ()
        let f = OptimizedClosures.FSharpFunc<_,_,_>.Adapt f
        for KeyValue(k, vx) in x do
            match tryGetValue k y with
            | Some vy -> dct.Add (k, f.Invoke (vx, vy))
            | None    -> ()
        dct :> IDictionary<'Key, 'U>

    let unionWith combiner (source1: IDictionary<'Key, 'Value>) (source2: IDictionary<'Key, 'Value>) =
        let d = Dictionary<'Key,'Value> ()
        let f = OptimizedClosures.FSharpFunc<_,_,_>.Adapt combiner
        for KeyValue(k, v ) in source1 do d.[k] <- v
        for KeyValue(k, v') in source2 do d.[k] <- match d.TryGetValue k with true, v -> f.Invoke (v, v') | _ -> v'
        d :> IDictionary<'Key,'Value>

    let union (source: IDictionary<'Key, 'T>) (altSource: IDictionary<'Key, 'T>) : IDictionary<'Key, 'Value> = unionWith (fun x _ -> x) source altSource

    let intersectWith combiner (source1:IDictionary<'Key, 'Value>) (source2:IDictionary<'Key, 'Value>) : IDictionary<'Key, 'Value> =
        map2 (fun x y -> combiner x y) source1 source2

    let intersect (source1:IDictionary<'Key, 'T>) (source2:IDictionary<'Key, 'T>) = 
        intersectWith (fun a _ -> a) source1 source2


module IReadOnlyDictionary =
    let tryGetValue k (dct: IReadOnlyDictionary<'Key, 'Value>) =
        match dct.TryGetValue k with
        | true, v -> Some v
        | _       -> None

    let unionWith combiner (source1: IReadOnlyDictionary<'Key, 'Value>) (source2: IReadOnlyDictionary<'Key, 'Value>) =
        let d = Dictionary<'Key,'Value> ()
        let f = OptimizedClosures.FSharpFunc<_,_,_>.Adapt combiner
        for KeyValue(k, v ) in source1 do d.[k] <- v
        for KeyValue(k, v') in source2 do d.[k] <- match d.TryGetValue k with true, v -> f.Invoke (v, v') | _ -> v'
        d :> IReadOnlyDictionary<'Key,'Value>

    let union (source: IReadOnlyDictionary<'Key, 'T>) (altSource: IReadOnlyDictionary<'Key, 'T>) : IReadOnlyDictionary<'Key, 'T> = 
        unionWith (fun x _ -> x) source altSource

    let map2 f (x: IReadOnlyDictionary<'Key, 'T1>) (y: IReadOnlyDictionary<'Key, 'T2>) =
        let dct = Dictionary<'Key, 'U> ()
        let f = OptimizedClosures.FSharpFunc<_,_,_>.Adapt f
        for KeyValue(k, vx) in x do
            match tryGetValue k y with
            | Some vy -> dct.Add (k, f.Invoke (vx, vy))
            | None    -> ()
        dct :> IReadOnlyDictionary<'Key, 'U>

    let intersectWith combiner (source1:IReadOnlyDictionary<'Key, 'T>) (source2:IReadOnlyDictionary<'Key, 'T>) =
         map2 (fun x y -> combiner x y) source1 source2

    let intersect (source1:IReadOnlyDictionary<'Key, 'T>) (source2:IReadOnlyDictionary<'Key, 'T>) = 
        intersectWith (fun a _ -> a) source1 source2