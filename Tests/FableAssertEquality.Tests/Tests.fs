module Tests

open Testing
open LibraryTests

open Fable.Core
open Fable.Core.JsInterop

let AllTests = testList "All Tests" [LibraryTests.EqualTests; LibraryTests.fsEqTests; LibraryTests.deepEqualTests; LibraryTests.equalObjTests]
flattenTest AllTests
