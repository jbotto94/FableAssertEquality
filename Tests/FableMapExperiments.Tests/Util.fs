module TestUtil

#if FABLE_COMPILER
    type TestKind =
    | TestList of string * TestKind seq
    | TestCase of (string * obj)

    open Fable.Core
    open Fable.Core.Testing
    open Fable.Core.JsInterop
    open Fable.Import
    open Fetch

    let testList (name: string) (tests: TestKind seq) = TestList( name, tests )
    let testCase (msg: string) (test: unit->unit) = TestCase( msg, box test )

    let equal expected actual: unit = Assert.AreEqual(actual, expected)
    let notEqual expected actual: unit = Assert.NotEqual(actual, expected)

    let private deepE expected actual: unit = import "DeepEq" "./DeepEqual.js"

    let deepEqual expected actual: unit = deepE expected actual


    
    //Code required to run the tests
    let [<Global>] describe (name: string) (f: unit->unit) = jsNative
    let [<Global>] it (msg: string) (f: unit->unit) = jsNative


    let rec flattenTest (test:TestKind) : unit =
        match test with
        | TestList(name, tests) ->
            describe name (fun () ->
              for t in tests do
                flattenTest t)
        | TestCase (name, test) ->
            it name (unbox test)

#else 

    open Expecto

    let testCase (msg: string) test : Test = testCase msg test
    let testList (name: string) test : Test = testList name test

    let equal expected actual: unit = Expect.equal actual expected ""
    let notEqual expected actual: unit = Expect.notEqual actual expected ""

    let deepEqual expected actual: unit = Expect.sequenceEqual actual expected ""

#endif
